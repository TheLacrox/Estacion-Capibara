// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Events;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared.EntityEffects;
using Content.Shared.EntityEffects.Effects;
using Content.Shared.Popups;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._Capibara.Botany;

/// <summary>
/// Core system for the plant genome. Initializes genomes on seed planting,
/// manages gene slots, and recalculates stats via the write-back bridge.
/// </summary>
public sealed partial class PlantGenomeSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<SeedPlantedInTrayEvent>(OnSeedPlanted);
        SubscribeLocalEvent<PlantGrowthCycleEvent>(OnGrowthCycle);
        SubscribeLocalEvent<SeedPacketSpawnedEvent>(OnSeedPacketSpawned);
        SubscribeLocalEvent<PlantGenomeComponent, ComponentRemove>(OnGenomeRemoved);

        InitializeStats();
        InitializeProduce();
        InitializeBreeding();
    }

    private void OnSeedPlanted(ref SeedPlantedInTrayEvent args)
    {
        var uid = args.PlantHolder;

        if (!TryComp<PlantHolderComponent>(uid, out var holder) || holder.Seed == null)
            return;

        var comp = EnsureComp<PlantGenomeComponent>(uid);

        // Check if the seed packet entity has genome data (from DNA manipulator or clipping)
        // The seed entity is queued for deletion but still accessible
        if (TryComp<PlantGenomeComponent>(args.SeedEntity, out var seedGenome) && seedGenome.Initialized)
        {
            // Transfer genome from seed packet to plant holder
            comp.CoreSpeciesId = seedGenome.CoreSpeciesId;
            comp.MaxSlots = seedGenome.MaxSlots;
            comp.Instability = seedGenome.Instability;
            comp.Initialized = true;

            comp.GeneSlots.Clear();
            foreach (var slot in seedGenome.GeneSlots)
            {
                comp.GeneSlots.Add(new PlantGeneSlot
                {
                    Gene = slot.Gene,
                    Locked = slot.Locked,
                });
            }

            comp.Epigenetics.Clear();
            comp.BaseStatSnapshot = new Dictionary<string, float>(seedGenome.BaseStatSnapshot);
            comp.BaseBoolSnapshot = new Dictionary<string, bool>(seedGenome.BaseBoolSnapshot);
            comp.BaseChemSnapshot = new Dictionary<string, (int, int, int)>(seedGenome.BaseChemSnapshot);
            comp.BaseHarvestType = seedGenome.BaseHarvestType;

            // Recapture base stats from actual seed data and apply gene modifiers
            CaptureBaseStats(comp, holder.Seed);
            RecalculateStats(uid, comp, holder);
            UpdateGeneTint(uid, comp);
            Dirty(uid, comp);
            return;
        }

        // Fresh plant — initialize empty genome
        comp.CoreSpeciesId = holder.Seed.Name;
        comp.Instability = 0f;
        comp.Epigenetics.Clear();

        comp.GeneSlots.Clear();
        for (var i = 0; i < comp.MaxSlots; i++)
        {
            comp.GeneSlots.Add(new PlantGeneSlot
            {
                Gene = null,
                Locked = i >= 6,
            });
        }

        CaptureBaseStats(comp, holder.Seed);
        comp.Initialized = true;

        Dirty(uid, comp);
    }

    private void OnGenomeRemoved(EntityUid uid, PlantGenomeComponent comp, ComponentRemove args)
    {
        // Clean up epigenetic cycle tracking to prevent memory leaks
        _idealTempCycles.Remove(uid);
        _pestFreeCycles.Remove(uid);
    }

    private void OnGrowthCycle(ref PlantGrowthCycleEvent args)
    {
        var uid = args.PlantHolder;

        if (!TryComp<PlantHolderComponent>(uid, out var holderCheck) || holderCheck.Seed == null)
            return;

        // Auto-initialize genome on any plant that doesn't have one yet
        if (!TryComp<PlantGenomeComponent>(uid, out var genome))
        {
            genome = EnsureComp<PlantGenomeComponent>(uid);
        }

        if (!genome.Initialized)
        {
            genome.CoreSpeciesId = holderCheck.Seed.Name;
            genome.Instability = 0f;
            genome.Epigenetics.Clear();
            genome.GeneSlots.Clear();
            for (var i = 0; i < genome.MaxSlots; i++)
                genome.GeneSlots.Add(new PlantGeneSlot { Gene = null, Locked = i >= 6 });
            CaptureBaseStats(genome, holderCheck.Seed);
            genome.Initialized = true;
            Dirty(uid, genome);
        }

        // Decay epigenetic modifiers
        for (var i = genome.Epigenetics.Count - 1; i >= 0; i--)
        {
            var epi = genome.Epigenetics[i];
            epi.RemainingCycles--;
            if (epi.RemainingCycles <= 0)
                genome.Epigenetics.RemoveAt(i);
        }

        // Process gene discovery (using mutation level from event, before base system resets it)
        if (TryComp<PlantHolderComponent>(uid, out var holder))
        {
            ProcessGeneDiscovery(uid, genome, holder, args.MutationLevel);
            ProcessPlantEffects(uid, genome, holder);
            ProcessEpigenetics(uid, genome, holder);
            ProcessInstability(uid, genome, holder);

            // Recalculate stats every cycle — applies gene modifiers + chemicals to SeedData
            RecalculateStats(uid, genome, holder);
        }

        // Update gene tint visuals
        UpdateGeneTint(uid, genome);

        Dirty(uid, genome);
    }

    /// <summary>
    /// When a plant has accumulated mutation level, there's a chance to discover a new gene,
    /// trigger species change, or gain chemical/gas mutations.
    /// This replaces the old MutationSystem for genome plants.
    /// </summary>
    private void ProcessGeneDiscovery(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder, float mutationLevel)
    {
        if (holder.Seed == null || holder.Dead)
            return;

        if (mutationLevel <= 0)
            return;

        var clampedLevel = Math.Min(mutationLevel, 25f);

        // Gene discovery chance scales with mutation level
        var discoveryChance = Math.Min(0.05f + 0.03f * clampedLevel, 0.60f);

        if (_random.Prob(discoveryChance))
        {
            TryDiscoverGene(uid, genome, holder, clampedLevel);
        }

        // Species change chance (requires high mutation level)
        if (clampedLevel >= 10 && holder.Seed.MutationPrototypes.Count > 0)
        {
            var speciesChance = 0.03f * clampedLevel / 25f;
            if (_random.Prob(speciesChance))
            {
                TrySpeciesChange(uid, genome, holder);
            }
        }

        // Chemical mutation (replaces old PlantMutateChemicals, same odds: 0.072 * severity)
        if (_random.Prob(Math.Min(0.072f * clampedLevel, 1.0f)))
        {
            var effect = new PlantMutateChemicals();
            effect.Effect(new EntityEffectBaseArgs(uid, EntityManager));
        }

        // Gas exude mutation (same odds: 0.0145 * severity)
        if (_random.Prob(Math.Min(0.0145f * clampedLevel, 1.0f)))
        {
            var effect = new PlantMutateExudeGasses();
            effect.Effect(new EntityEffectBaseArgs(uid, EntityManager));
        }

        // Gas consume mutation (same odds: 0.0036 * severity)
        if (_random.Prob(Math.Min(0.0036f * clampedLevel, 1.0f)))
        {
            var effect = new PlantMutateConsumeGasses();
            effect.Effect(new EntityEffectBaseArgs(uid, EntityManager));
        }
    }

    /// <summary>
    /// Attempts to discover a new gene and slot it into an empty unlocked slot.
    /// </summary>
    private void TryDiscoverGene(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder, float mutationLevel)
    {
        // Find an empty unlocked slot
        var emptySlot = -1;
        for (var i = 0; i < genome.GeneSlots.Count; i++)
        {
            if (genome.GeneSlots[i].Gene == null && !genome.GeneSlots[i].Locked)
            {
                emptySlot = i;
                break;
            }
        }

        if (emptySlot == -1)
            return; // No empty slots

        // Pick a random gene weighted by rarity (scaled with mutation level)
        var gene = PickRandomGeneByRarity(mutationLevel);
        if (gene == null)
            return;

        // Check not already present
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene?.Id == gene.ID)
                return;
        }

        genome.GeneSlots[emptySlot] = new PlantGeneSlot { Gene = gene.ID, Locked = false };

        // Notify the botanist
        _popup.PopupEntity(Loc.GetString("capibara-genome-gene-discovered",
            ("gene", Loc.GetString(gene.Name))), uid, PopupType.Medium);

        RecalculateInstability(uid, genome);

        // Recalculate stats so the new gene takes effect immediately
        RecalculateStats(uid, genome, holder);
    }

    /// <summary>
    /// Picks a random gene weighted by rarity. Weights scale with mutation level.
    /// </summary>
    private PlantGenePrototype? PickRandomGeneByRarity(float mutationLevel = 1f)
    {
        var candidates = new List<(PlantGenePrototype gene, float weight)>();
        foreach (var gene in _protoManager.EnumeratePrototypes<PlantGenePrototype>())
        {
            var weight = gene.Rarity switch
            {
                PlantGeneRarity.Common => 10f,
                PlantGeneRarity.Uncommon => 4f + mutationLevel * 0.3f,
                PlantGeneRarity.Rare => 1f + mutationLevel * 0.15f,
                PlantGeneRarity.Legendary => mutationLevel >= 15 ? 0.1f : 0f,
                _ => 1f,
            };

            if (weight > 0)
                candidates.Add((gene, weight));
        }

        if (candidates.Count == 0)
            return null;

        var totalWeight = 0f;
        foreach (var (_, w) in candidates)
            totalWeight += w;

        var roll = _random.NextFloat(0, totalWeight);
        var accum = 0f;
        foreach (var (gene, w) in candidates)
        {
            accum += w;
            if (roll <= accum)
                return gene;
        }

        return candidates[^1].gene;
    }

    /// <summary>
    /// Triggers a species change using the plant's MutationPrototypes list.
    /// Preserves existing genes and reapplies them on the new species base.
    /// </summary>
    private void TrySpeciesChange(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        if (holder.Seed == null || holder.Seed.MutationPrototypes.Count == 0)
            return;

        var targetProto = _random.Pick(holder.Seed.MutationPrototypes);
        if (!_protoManager.TryIndex(targetProto, out SeedPrototype? protoSeed))
            return;

        // Perform species change on the seed data
        holder.Seed = holder.Seed.SpeciesChange(protoSeed);

        // Update genome core species
        genome.CoreSpeciesId = holder.Seed.Name;

        // Recapture base stats from new species, then reapply existing genes
        CaptureBaseStats(genome, holder.Seed);
        RecalculateStats(uid, genome, holder);

        Dirty(uid, genome);
    }

    /// <summary>
    /// Calculates a blended tint color from all active gene tints, collects effect overlays,
    /// and sends both to the client via appearance data.
    /// </summary>
    public void UpdateGeneTint(EntityUid uid, PlantGenomeComponent genome)
    {
        // Skip appearance updates for plants with no genes (avoids unnecessary PVS traffic)
        var hasAnyGene = false;
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene != null)
            {
                hasAnyGene = true;
                break;
            }
        }

        if (!hasAnyGene)
            return;

        float r = 0, g = 0, b = 0;
        var tintCount = 0;
        var overlayStrings = new List<string>(); // "rsiPath|state|colorHex" format

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var geneProto))
                continue;

            // Collect tint colors
            if (geneProto.VisualTint is { } tint)
            {
                r += tint.R;
                g += tint.G;
                b += tint.B;
                tintCount++;
            }

            // Collect effect overlays as simple strings (avoids custom class serialization)
            if (geneProto.EffectRsi != null && geneProto.EffectState != null)
            {
                var effectColor = geneProto.VisualTint ?? Color.White;
                overlayStrings.Add($"{geneProto.EffectRsi}|{geneProto.EffectState}|{effectColor.ToHex()}");
            }
        }

        // Calculate blended tint
        if (tintCount > 0)
        {
            r /= tintCount;
            g /= tintCount;
            b /= tintCount;

            const float blendToWhite = 0.4f;
            r = r + (1f - r) * blendToWhite;
            g = g + (1f - g) * blendToWhite;
            b = b + (1f - b) * blendToWhite;

            _appearance.SetData(uid, PlantGenomeVisuals.GeneTint, new Color(r, g, b));
        }

        // Send effect overlays as simple string list
        if (overlayStrings.Count > 0)
            _appearance.SetData(uid, PlantGenomeVisuals.EffectOverlays, overlayStrings);
    }

    private void OnSeedPacketSpawned(ref SeedPacketSpawnedEvent args)
    {
        TransferGenomeToSeed(args.SourcePlantHolder, args.SeedPacket);
    }

    /// <summary>
    /// Copies genome data from a plant holder to a seed packet entity.
    /// Called when seeds are clipped or extracted.
    /// </summary>
    public void TransferGenomeToSeed(EntityUid plantHolder, EntityUid seedPacket)
    {
        if (!TryComp<PlantGenomeComponent>(plantHolder, out var sourceGenome) || !sourceGenome.Initialized)
            return;

        var targetGenome = EnsureComp<PlantGenomeComponent>(seedPacket);
        targetGenome.CoreSpeciesId = sourceGenome.CoreSpeciesId;
        targetGenome.MaxSlots = sourceGenome.MaxSlots;
        targetGenome.Instability = sourceGenome.Instability;
        targetGenome.Initialized = true;

        // Deep copy gene slots
        targetGenome.GeneSlots.Clear();
        foreach (var slot in sourceGenome.GeneSlots)
        {
            targetGenome.GeneSlots.Add(new PlantGeneSlot
            {
                Gene = slot.Gene,
                Locked = slot.Locked,
            });
        }

        // Don't copy epigenetics (they're environmental, not genetic)
        targetGenome.Epigenetics.Clear();

        // Copy base stat snapshot
        targetGenome.BaseStatSnapshot = new Dictionary<string, float>(sourceGenome.BaseStatSnapshot);
        targetGenome.BaseBoolSnapshot = new Dictionary<string, bool>(sourceGenome.BaseBoolSnapshot);
        targetGenome.BaseChemSnapshot = new Dictionary<string, (int, int, int)>(sourceGenome.BaseChemSnapshot);
        targetGenome.BaseHarvestType = sourceGenome.BaseHarvestType;

        Dirty(seedPacket, targetGenome);
    }
}
