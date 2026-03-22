// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Server.Botany.Systems;
using Content.Server.Power.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared._Capibara.Botany.Ui;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Interaction;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;

namespace Content.Server._Capibara.Botany.Machines;

/// <summary>
/// Server system for the Seed Analyzer machine.
/// Displays the full genome readout of an inserted seed or produce.
/// </summary>
public sealed class SeedAnalyzerSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly BotanySystem _botanySystem = default!;
    [Dependency] private readonly ItemSlotsSystem _itemSlots = default!;

    public override void Initialize()
    {
        base.Initialize();

        Subs.BuiEvents<SeedAnalyzerComponent>(SeedAnalyzerUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnUiOpened);
            subs.Event<SeedAnalyzerEjectMessage>(OnEject);
        });

        SubscribeLocalEvent<SeedAnalyzerComponent, EntInsertedIntoContainerMessage>(OnItemInserted);
        SubscribeLocalEvent<SeedAnalyzerComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
        SubscribeLocalEvent<SeedAnalyzerComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, SeedAnalyzerComponent comp, InteractUsingEvent args)
    {
        if (args.Handled)
            return;

        if (!this.IsPowered(uid, EntityManager))
            return;

        // Check if the used item is a seed packet or produce
        if (!HasComp<SeedComponent>(args.Used) && !HasComp<ProduceComponent>(args.Used))
            return;

        // Try to insert into the analyzer's item slot
        if (!TryComp<SeedAnalyzerComponent>(uid, out var analyzerComp))
            return;

        if (_itemSlots.TryInsertFromHand(uid, analyzerComp.SeedSlot, args.User))
        {
            args.Handled = true;
            UpdateUiState(uid);
        }
    }

    private void OnItemInserted(EntityUid uid, SeedAnalyzerComponent comp, EntInsertedIntoContainerMessage args)
    {
        UpdateUiState(uid);
    }

    private void OnItemRemoved(EntityUid uid, SeedAnalyzerComponent comp, EntRemovedFromContainerMessage args)
    {
        UpdateUiState(uid);
    }

    private void OnUiOpened(EntityUid uid, SeedAnalyzerComponent comp, BoundUIOpenedEvent args)
    {
        UpdateUiState(uid);
    }

    private void OnEject(EntityUid uid, SeedAnalyzerComponent comp, SeedAnalyzerEjectMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.SeedSlot, args.Actor);
        UpdateUiState(uid);
    }

    private void UpdateUiState(EntityUid uid)
    {
        if (!TryComp<SeedAnalyzerComponent>(uid, out var comp))
            return;

        var state = new SeedAnalyzerBuiState();

        // Try to get the seed from whatever is in the slot
        if (comp.SeedSlot.Item is { } item)
        {
            SeedData? seed = null;

            if (TryComp<SeedComponent>(item, out var seedComp))
                _botanySystem.TryGetSeed(seedComp, out seed);
            else if (TryComp<ProduceComponent>(item, out var produce))
                _botanySystem.TryGetSeed(produce, out seed);

            if (seed != null)
            {
                state.HasSeed = true;
                state.SpeciesName = Loc.GetString(seed.DisplayName);
                state.Potency = seed.Potency;
                state.Yield = seed.Yield;
                state.Lifespan = seed.Lifespan;
                state.Maturation = seed.Maturation;
                state.Production = seed.Production;
                state.Endurance = seed.Endurance;
                state.IdealHeat = seed.IdealHeat;
                state.IdealLight = seed.IdealLight;
                state.WaterConsumption = seed.WaterConsumption;
                state.NutrientConsumption = seed.NutrientConsumption;

                // Check if there's genome data on this entity or the plant holder it came from
                if (TryComp<PlantGenomeComponent>(item, out var genome) && genome.Initialized)
                {
                    PopulateGenomeState(state, genome);
                }
                else
                {
                    // No genome — show default empty slots
                    state.MaxSlots = 6;
                    state.Instability = 0f;
                    for (var i = 0; i < 6; i++)
                    {
                        state.GeneSlots.Add(new GeneSlotData(null, null, null, null, i >= 6));
                    }
                }
            }
        }

        _uiSystem.SetUiState(uid, SeedAnalyzerUiKey.Key, state);
    }

    private void PopulateGenomeState(SeedAnalyzerBuiState state, PlantGenomeComponent genome)
    {
        state.Instability = genome.Instability;
        state.MaxSlots = genome.MaxSlots;

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene != null && _protoManager.TryIndex(slot.Gene.Value, out var genProto))
            {
                state.GeneSlots.Add(new GeneSlotData(
                    slot.Gene,
                    Loc.GetString(genProto.Name),
                    Loc.GetString(genProto.Description),
                    genProto.Rarity,
                    slot.Locked));
            }
            else
            {
                state.GeneSlots.Add(new GeneSlotData(null, null, null, null, slot.Locked));
            }
        }

        foreach (var epi in genome.Epigenetics)
        {
            state.EpigeneticNames.Add(Loc.GetString($"capibara-epigenetic-{epi.EffectId}"));
        }
    }
}
