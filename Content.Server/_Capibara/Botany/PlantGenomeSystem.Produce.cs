// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Events;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared.EntityEffects;
using Content.Shared.EntityEffects.Effects;
using Robust.Shared.Random;

namespace Content.Server._Capibara.Botany;

public sealed partial class PlantGenomeSystem
{
    [Dependency] private readonly ProduceMobSystem _produceMob = default!;

    private void InitializeProduce()
    {
        SubscribeLocalEvent<ProduceEntitySpawnedEvent>(OnProduceSpawned);
    }

    /// <summary>
    /// When produce is spawned, check if the source plant has a genome and apply gene effects.
    /// </summary>
    private void OnProduceSpawned(ref ProduceEntitySpawnedEvent args)
    {
        if (!TryComp<PlantGenomeComponent>(args.PlantHolder, out var genome) || !genome.Initialized)
            return;

        if (!TryComp<PlantHolderComponent>(args.PlantHolder, out var holder) || holder.Seed == null)
            return;

        // Apply visual effects from genes to produce
        ApplyGeneVisualsToProduct(args.Produce, genome);

        // Store active gene IDs on produce for downstream systems
        StoreGenesOnProduce(args.Produce, genome);

        // Copy full genome to produce so seed extractor can transfer it to new seeds
        CopyGenomeToProduce(args.Produce, args.PlantHolder, genome);

        // Apply behavioral effects from each gene to produce
        if (ApplyGeneBehaviorsToProduct(args.Produce, genome, holder))
            return; // Produce was consumed (e.g. turned into sentient mob)

        // Check for gene combo synergies
        foreach (var combo in _protoManager.EnumeratePrototypes<GeneComboPrototype>())
        {
            if (!MatchesCombo(genome, combo, holder.Seed.Potency))
                continue;

            // If combo replaces normal produce, delete the original and spawn combo items instead
            if (combo.ReplacesNormalProduce)
            {
                var coords = Transform(args.Produce).Coordinates;
                QueueDel(args.Produce);
                foreach (var proto in combo.BonusProducePrototypes)
                {
                    Spawn(proto, coords);
                }
                return; // Original produce replaced
            }

            // Spawn bonus produce alongside normal
            var bonusCoords = Transform(args.Produce).Coordinates;
            foreach (var proto in combo.BonusProducePrototypes)
            {
                Spawn(proto, bonusCoords);
            }
        }
    }

    /// <summary>
    /// Applies behavioral effects from genes to produce entities.
    /// Maps specific gene IDs to actual game mechanics on the produce.
    /// Returns true if the produce entity was consumed (e.g. turned into a mob).
    /// </summary>
    private bool ApplyGeneBehaviorsToProduct(EntityUid produce, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null)
                continue;

            switch (slot.Gene.Value.Id)
            {
                case "GeneSlippery":
                case "GeneHonking":
                    // Make produce slippery (same as old Slipify mutation)
                    new Slipify().Effect(new EntityEffectBaseArgs(produce, EntityManager));
                    break;

                case "GeneSentient":
                    // Make produce into a sentient mob
                    var aggressive = _random.Prob(0.4f);
                    _produceMob.MakeProduceSentient(produce, aggressive);
                    return true; // Produce entity was consumed

                case "GeneBioluminescent":
                    // Make produce glow — uses the appearance system to signal the client
                    // The glow is handled via the gene visual tint (green tint already applied)
                    break;

                case "GeneMetallic":
                    // Metallic produce has high throw damage — handled by GeneProduceEffectsSystem
                    // which adds knockdown on throw hit
                    break;
            }
        }

        return false;
    }

    /// <summary>
    /// Stores active gene IDs on the produce component for downstream use.
    /// </summary>
    private void StoreGenesOnProduce(EntityUid produce, PlantGenomeComponent genome)
    {
        var geneIds = new List<string>();
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene != null)
                geneIds.Add(slot.Gene.Value.Id);
        }

        if (geneIds.Count == 0)
            return;

        var comp = EnsureComp<GeneModifiedProduceComponent>(produce);
        comp.ActiveGeneIds = geneIds;
        Dirty(produce, comp);
    }

    /// <summary>
    /// Copies gene visual effects (tint + overlays) from the plant genome onto a produce entity.
    /// </summary>
    private void ApplyGeneVisualsToProduct(EntityUid produce, PlantGenomeComponent genome)
    {
        float r = 0, g = 0, b = 0;
        var tintCount = 0;
        var overlayStrings = new List<string>();

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var geneProto))
                continue;

            if (geneProto.VisualTint is { } tint)
            {
                r += tint.R;
                g += tint.G;
                b += tint.B;
                tintCount++;
            }

            if (geneProto.EffectRsi != null && geneProto.EffectState != null)
            {
                var effectColor = geneProto.VisualTint ?? Color.White;
                overlayStrings.Add($"{geneProto.EffectRsi}|{geneProto.EffectState}|{effectColor.ToHex()}");
            }
        }

        if (tintCount == 0 && overlayStrings.Count == 0)
            return;

        var comp = EnsureComp<GeneModifiedProduceComponent>(produce);

        if (tintCount > 0)
        {
            r /= tintCount;
            g /= tintCount;
            b /= tintCount;

            const float blendToWhite = 0.4f;
            r = r + (1f - r) * blendToWhite;
            g = g + (1f - g) * blendToWhite;
            b = b + (1f - b) * blendToWhite;

            comp.Tint = new Color(r, g, b);
        }

        comp.EffectOverlays = overlayStrings;
        Dirty(produce, comp);
    }

    /// <summary>
    /// Checks if a genome contains a specific gene by prototype ID.
    /// </summary>
    private static bool HasGene(PlantGenomeComponent genome, string geneId)
    {
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene?.Id == geneId)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if a genome matches a gene combo's requirements.
    /// </summary>
    private static bool MatchesCombo(PlantGenomeComponent genome, GeneComboPrototype combo, float potency)
    {
        if (potency < combo.MinPotency)
            return false;

        foreach (var requiredGene in combo.RequiredGenes)
        {
            var found = false;
            foreach (var slot in genome.GeneSlots)
            {
                if (slot.Gene == requiredGene)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Copies the plant holder's genome onto the produce entity so the seed extractor
    /// can transfer it to new seed packets.
    /// </summary>
    private void CopyGenomeToProduce(EntityUid produce, EntityUid plantHolder, PlantGenomeComponent sourceGenome)
    {
        var target = EnsureComp<PlantGenomeComponent>(produce);
        target.CoreSpeciesId = sourceGenome.CoreSpeciesId;
        target.MaxSlots = sourceGenome.MaxSlots;
        target.Instability = sourceGenome.Instability;
        target.Initialized = true;

        target.GeneSlots.Clear();
        foreach (var slot in sourceGenome.GeneSlots)
        {
            target.GeneSlots.Add(new PlantGeneSlot
            {
                Gene = slot.Gene,
                Locked = slot.Locked,
            });
        }

        target.Epigenetics.Clear();
        target.BaseStatSnapshot = new Dictionary<string, float>(sourceGenome.BaseStatSnapshot);
        target.BaseBoolSnapshot = new Dictionary<string, bool>(sourceGenome.BaseBoolSnapshot);
        target.BaseChemSnapshot = new Dictionary<string, (int, int, int)>(sourceGenome.BaseChemSnapshot);
        target.BaseHarvestType = sourceGenome.BaseHarvestType;

        Dirty(produce, target);
    }
}
