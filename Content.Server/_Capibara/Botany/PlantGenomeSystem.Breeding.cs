// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Events;
using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Random;

namespace Content.Server._Capibara.Botany;

public sealed partial class PlantGenomeSystem
{
    private void InitializeBreeding()
    {
        SubscribeLocalEvent<PlantCrossPollinatedEvent>(OnCrossPollinated);
    }

    /// <summary>
    /// When two plants are cross-pollinated, merge gene slots with weighted inheritance.
    /// Rarer genes have lower transfer chance. After Cross() merges raw SeedData,
    /// recapture base stats and reapply gene modifiers.
    /// </summary>
    private void OnCrossPollinated(ref PlantCrossPollinatedEvent args)
    {
        if (!TryComp<PlantGenomeComponent>(args.SourcePlant, out var sourceGenome) || !sourceGenome.Initialized)
            return;

        if (!TryComp<PlantGenomeComponent>(args.TargetPlant, out var targetGenome) || !targetGenome.Initialized)
            return;

        // For each gene in source, try to transfer to target based on rarity
        foreach (var sourceSlot in sourceGenome.GeneSlots)
        {
            if (sourceSlot.Gene == null)
                continue;

            if (!_protoManager.TryIndex(sourceSlot.Gene.Value, out var geneProto))
                continue;

            // Weighted transfer chance based on rarity
            var transferChance = geneProto.Rarity switch
            {
                PlantGeneRarity.Common => 0.70f,
                PlantGeneRarity.Uncommon => 0.50f,
                PlantGeneRarity.Rare => 0.25f,
                PlantGeneRarity.Legendary => 0.10f,
                _ => 0.50f,
            };

            if (!_random.Prob(transferChance))
                continue;

            // Check if target already has this gene
            var alreadyHas = false;
            foreach (var targetSlot in targetGenome.GeneSlots)
            {
                if (targetSlot.Gene == sourceSlot.Gene)
                {
                    alreadyHas = true;
                    break;
                }
            }
            if (alreadyHas)
                continue;

            // Find empty unlocked slot in target
            for (var i = 0; i < targetGenome.GeneSlots.Count; i++)
            {
                var slot = targetGenome.GeneSlots[i];
                if (slot.Gene == null && !slot.Locked)
                {
                    targetGenome.GeneSlots[i] = new PlantGeneSlot
                    {
                        Gene = sourceSlot.Gene,
                        Locked = false,
                    };
                    break;
                }
            }
        }

        // 5% base chance to discover a new random gene through breeding
        if (_random.Prob(0.05f))
        {
            DiscoverRandomGene(targetGenome);
        }

        // After MutationSystem.Cross() merged raw SeedData, recapture the new base stats
        // and reapply gene modifiers on top
        if (TryComp<PlantHolderComponent>(args.TargetPlant, out var holder) && holder.Seed != null)
        {
            CaptureBaseStats(targetGenome, holder.Seed);
            RecalculateStats(args.TargetPlant, targetGenome, holder);
        }
        else
        {
            RecalculateInstability(args.TargetPlant, targetGenome);
        }

        Dirty(args.TargetPlant, targetGenome);
    }

    /// <summary>
    /// Discovers a random Common or Uncommon gene and adds it to an empty slot.
    /// </summary>
    private void DiscoverRandomGene(PlantGenomeComponent genome)
    {
        // Collect all Common/Uncommon genes
        var candidates = new List<PlantGenePrototype>();
        foreach (var gene in _protoManager.EnumeratePrototypes<PlantGenePrototype>())
        {
            if (gene.Rarity is PlantGeneRarity.Common or PlantGeneRarity.Uncommon)
                candidates.Add(gene);
        }

        if (candidates.Count == 0)
            return;

        var chosen = _random.Pick(candidates);

        // Check not already present
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene?.Id == chosen.ID)
                return;
        }

        // Insert into empty slot
        for (var i = 0; i < genome.GeneSlots.Count; i++)
        {
            var slot = genome.GeneSlots[i];
            if (slot.Gene == null && !slot.Locked)
            {
                genome.GeneSlots[i] = new PlantGeneSlot
                {
                    Gene = chosen.ID,
                    Locked = false,
                };
                break;
            }
        }
    }
}
