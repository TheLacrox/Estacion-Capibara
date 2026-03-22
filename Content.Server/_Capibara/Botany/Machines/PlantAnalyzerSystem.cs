// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared._Capibara.Botany.Ui;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Server.Popups;
using Robust.Server.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Server._Capibara.Botany.Machines;

/// <summary>
/// Handheld tool: use on a hydroponics tray to open a popup UI with full plant + genome info.
/// </summary>
public sealed class PlantAnalyzerSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly PlantGenomeSystem _genomeSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<PlantAnalyzerComponent, AfterInteractEvent>(OnAfterInteract);
    }

    private void OnAfterInteract(EntityUid uid, PlantAnalyzerComponent comp, AfterInteractEvent args)
    {
        if (args.Handled || args.Target == null || !args.CanReach)
            return;

        var target = args.Target.Value;

        if (!TryComp<PlantHolderComponent>(target, out var holder))
            return;

        args.Handled = true;

        if (holder.Seed == null)
        {
            _popup.PopupEntity(Loc.GetString("capibara-plant-analyzer-no-plant"), target, args.User);
            return;
        }

        var state = BuildState(target, holder);
        _uiSystem.OpenUi(uid, PlantAnalyzerUiKey.Key, args.User);
        _uiSystem.SetUiState(uid, PlantAnalyzerUiKey.Key, state);
    }

    private PlantAnalyzerBuiState BuildState(EntityUid plantHolder, PlantHolderComponent holder)
    {
        var seed = holder.Seed!;
        var state = new PlantAnalyzerBuiState
        {
            SpeciesName = Loc.GetString(seed.DisplayName),

            // Stats
            Potency = seed.Potency,
            Yield = seed.Yield,
            Lifespan = seed.Lifespan,
            Maturation = seed.Maturation,
            Production = seed.Production,
            Endurance = seed.Endurance,

            // Environment
            IdealHeat = seed.IdealHeat,
            HeatTolerance = seed.HeatTolerance,
            IdealLight = seed.IdealLight,
            WaterConsumption = seed.WaterConsumption,
            NutrientConsumption = seed.NutrientConsumption,

            // Current state
            Health = holder.Health,
            MaxHealth = seed.Endurance,
            Age = holder.Age,
            WaterLevel = holder.WaterLevel,
            NutritionLevel = holder.NutritionLevel,
            PestLevel = holder.PestLevel,
            WeedLevel = holder.WeedLevel,
            Toxins = holder.Toxins,
            Dead = holder.Dead,
            Harvest = holder.Harvest,
            Seedless = seed.Seedless,
            Ligneous = seed.Ligneous,
            HarvestType = seed.HarvestRepeat.ToString(),

            // Mutation
            MutationLevel = holder.MutationLevel,
            MutationMod = holder.MutationMod,
            GeneDiscoveryChance = holder.MutationLevel > 0 ? 50f : 0f,
        };

        // Chemistry - list what chemicals this plant produces
        foreach (var (chemId, _) in seed.Chemicals)
        {
            state.ChemicalNames.Add(chemId);
        }

        // Genome — initialize on-demand if plant doesn't have one yet
        if (!TryComp<PlantGenomeComponent>(plantHolder, out var genome) || !genome.Initialized)
        {
            genome = EnsureComp<PlantGenomeComponent>(plantHolder);
            if (!genome.Initialized)
            {
                genome.CoreSpeciesId = seed.Name;
                genome.Instability = 0f;
                genome.Epigenetics.Clear();
                genome.GeneSlots.Clear();
                for (var i = 0; i < genome.MaxSlots; i++)
                    genome.GeneSlots.Add(new PlantGeneSlot { Gene = null, Locked = i >= 6 });
                _genomeSystem.CaptureBaseStats(genome, seed);
                genome.Initialized = true;
                Dirty(plantHolder, genome);
            }
        }

        if (genome.Initialized)
        {
            state.HasGenome = true;
            state.Instability = genome.Instability;
            state.MaxSlots = genome.MaxSlots;

            foreach (var slot in genome.GeneSlots)
            {
                if (slot.Gene != null && _protoManager.TryIndex(slot.Gene.Value, out var gp))
                {
                    state.GeneSlots.Add(new GeneSlotData(
                        slot.Gene, Loc.GetString(gp.Name), Loc.GetString(gp.Description),
                        gp.Rarity, slot.Locked));
                }
                else
                {
                    state.GeneSlots.Add(new GeneSlotData(null, null, null, null, slot.Locked));
                }
            }

            // Count slots
            var filled = 0;
            var empty = 0;
            foreach (var slot in genome.GeneSlots)
            {
                if (slot.Locked) continue;
                if (slot.Gene != null) filled++;
                else empty++;
            }
            state.FilledSlots = filled;
            state.EmptySlots = empty;

            foreach (var epi in genome.Epigenetics)
            {
                state.EpigeneticNames.Add(Loc.GetString($"capibara-epigenetic-{epi.EffectId}"));
            }
        }

        return state;
    }
}
