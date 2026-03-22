// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;

namespace Content.Server._Capibara.Botany;

public sealed partial class PlantGenomeSystem
{
    /// <summary>
    /// Number of consecutive cycles a plant has been in ideal temperature.
    /// Tracked per-entity via a dictionary since we don't want to add fields to PlantHolderComponent.
    /// </summary>
    private readonly Dictionary<EntityUid, int> _idealTempCycles = new();
    private readonly Dictionary<EntityUid, int> _pestFreeCycles = new();

    /// <summary>
    /// Checks environmental conditions and applies/removes epigenetic modifiers.
    /// Called each growth cycle after instability processing.
    /// </summary>
    public void ProcessEpigenetics(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        if (holder.Seed == null || holder.Dead)
            return;

        // Track ideal temperature cycles
        if (!holder.ImproperHeat)
        {
            _idealTempCycles.TryGetValue(uid, out var count);
            _idealTempCycles[uid] = count + 1;

            if (count + 1 >= 10 && !HasEpigenetic(genome, "acclimatized"))
            {
                genome.Epigenetics.Add(new EpigeneticModifier
                {
                    EffectId = "acclimatized",
                    StatModifiers = new Dictionary<string, float> { { "Instability", -5f } },
                    RemainingCycles = 999, // Lasts until temp changes
                    Source = "environment:temperature",
                });
            }
        }
        else
        {
            _idealTempCycles[uid] = 0;
            RemoveEpigenetic(genome, "acclimatized");
        }

        // Track pest-free cycles
        if (holder.PestLevel <= 0.5f)
        {
            _pestFreeCycles.TryGetValue(uid, out var count);
            _pestFreeCycles[uid] = count + 1;

            if (count + 1 >= 15 && !HasEpigenetic(genome, "thriving"))
            {
                genome.Epigenetics.Add(new EpigeneticModifier
                {
                    EffectId = "thriving",
                    StatModifiers = new Dictionary<string, float> { { "Yield", 1f } },
                    RemainingCycles = 999,
                    Source = "environment:pest-free",
                });
            }
        }
        else
        {
            _pestFreeCycles[uid] = 0;
            RemoveEpigenetic(genome, "thriving");
        }

        // Check for stabilizer reagent in soil (handled by reagent effects, but we add the epigenetic here)
        // This would be triggered by reagent processing — for now, the stabilizer epigenetic
        // is added via chemical interaction in soil solution processing.

        Dirty(uid, genome);
    }

    private static bool HasEpigenetic(PlantGenomeComponent genome, string effectId)
    {
        foreach (var epi in genome.Epigenetics)
        {
            if (epi.EffectId == effectId)
                return true;
        }
        return false;
    }

    private static void RemoveEpigenetic(PlantGenomeComponent genome, string effectId)
    {
        for (var i = genome.Epigenetics.Count - 1; i >= 0; i--)
        {
            if (genome.Epigenetics[i].EffectId == effectId)
                genome.Epigenetics.RemoveAt(i);
        }
    }
}
