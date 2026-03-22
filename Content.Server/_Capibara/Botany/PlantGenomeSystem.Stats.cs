// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;

namespace Content.Server._Capibara.Botany;

public sealed partial class PlantGenomeSystem
{
    private void InitializeStats()
    {
        // Additional stat-related subscriptions can go here.
    }

    /// <summary>
    /// Captures a snapshot of the original SeedData values before any gene modifications.
    /// This snapshot is used to restore base values during RecalculateStats().
    /// </summary>
    public void CaptureBaseStats(PlantGenomeComponent genome, SeedData seed)
    {
        genome.BaseStatSnapshot.Clear();
        genome.BaseStatSnapshot["Potency"] = seed.Potency;
        genome.BaseStatSnapshot["Yield"] = seed.Yield;
        genome.BaseStatSnapshot["Lifespan"] = seed.Lifespan;
        genome.BaseStatSnapshot["Maturation"] = seed.Maturation;
        genome.BaseStatSnapshot["Production"] = seed.Production;
        genome.BaseStatSnapshot["Endurance"] = seed.Endurance;
        genome.BaseStatSnapshot["NutrientConsumption"] = seed.NutrientConsumption;
        genome.BaseStatSnapshot["WaterConsumption"] = seed.WaterConsumption;
        genome.BaseStatSnapshot["IdealHeat"] = seed.IdealHeat;
        genome.BaseStatSnapshot["HeatTolerance"] = seed.HeatTolerance;
        genome.BaseStatSnapshot["IdealLight"] = seed.IdealLight;
        genome.BaseStatSnapshot["LightTolerance"] = seed.LightTolerance;
        genome.BaseStatSnapshot["ToxinsTolerance"] = seed.ToxinsTolerance;
        genome.BaseStatSnapshot["LowPressureTolerance"] = seed.LowPressureTolerance;
        genome.BaseStatSnapshot["HighPressureTolerance"] = seed.HighPressureTolerance;
        genome.BaseStatSnapshot["PestTolerance"] = seed.PestTolerance;
        genome.BaseStatSnapshot["WeedTolerance"] = seed.WeedTolerance;

        // Capture base chemicals
        genome.BaseChemSnapshot.Clear();
        foreach (var (chemId, chemData) in seed.Chemicals)
        {
            genome.BaseChemSnapshot[chemId] = (chemData.Min, chemData.Max, chemData.PotencyDivisor);
        }

        // Capture boolean traits
        genome.BaseBoolSnapshot.Clear();
        genome.BaseBoolSnapshot["Seedless"] = seed.Seedless;
        genome.BaseBoolSnapshot["Ligneous"] = seed.Ligneous;
        genome.BaseBoolSnapshot["CanScream"] = seed.CanScream;
        genome.BaseBoolSnapshot["TurnIntoKudzu"] = seed.TurnIntoKudzu;

        // Capture harvest type
        genome.BaseHarvestType = (int) seed.HarvestRepeat;
    }

    /// <summary>
    /// Recalculates all SeedData stats from the base snapshot + gene modifiers + epigenetics.
    /// This is the core of the write-back bridge: genome data is written into SeedData
    /// so all existing code that reads Seed.Potency etc. gets genome-adjusted values.
    /// </summary>
    public void RecalculateStats(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        if (holder.Seed == null || !genome.Initialized)
            return;

        // Ensure we have a unique seed to modify
        if (!holder.Seed.Unique)
        {
            holder.Seed = holder.Seed.Clone();
        }

        var seed = holder.Seed;

        // Step 1: Restore base values from snapshot
        if (genome.BaseStatSnapshot.TryGetValue("Potency", out var val)) seed.Potency = val;
        if (genome.BaseStatSnapshot.TryGetValue("Yield", out val)) seed.Yield = (int) val;
        if (genome.BaseStatSnapshot.TryGetValue("Lifespan", out val)) seed.Lifespan = val;
        if (genome.BaseStatSnapshot.TryGetValue("Maturation", out val)) seed.Maturation = val;
        if (genome.BaseStatSnapshot.TryGetValue("Production", out val)) seed.Production = val;
        if (genome.BaseStatSnapshot.TryGetValue("Endurance", out val)) seed.Endurance = val;
        if (genome.BaseStatSnapshot.TryGetValue("NutrientConsumption", out val)) seed.NutrientConsumption = val;
        if (genome.BaseStatSnapshot.TryGetValue("WaterConsumption", out val)) seed.WaterConsumption = val;
        if (genome.BaseStatSnapshot.TryGetValue("IdealHeat", out val)) seed.IdealHeat = val;
        if (genome.BaseStatSnapshot.TryGetValue("HeatTolerance", out val)) seed.HeatTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("IdealLight", out val)) seed.IdealLight = val;
        if (genome.BaseStatSnapshot.TryGetValue("LightTolerance", out val)) seed.LightTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("ToxinsTolerance", out val)) seed.ToxinsTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("LowPressureTolerance", out val)) seed.LowPressureTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("HighPressureTolerance", out val)) seed.HighPressureTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("PestTolerance", out val)) seed.PestTolerance = val;
        if (genome.BaseStatSnapshot.TryGetValue("WeedTolerance", out val)) seed.WeedTolerance = val;

        // Step 1b: Restore boolean traits from snapshot
        if (genome.BaseBoolSnapshot.TryGetValue("Seedless", out var bVal)) seed.Seedless = bVal;
        if (genome.BaseBoolSnapshot.TryGetValue("Ligneous", out bVal)) seed.Ligneous = bVal;
        if (genome.BaseBoolSnapshot.TryGetValue("CanScream", out bVal)) seed.CanScream = bVal;
        if (genome.BaseBoolSnapshot.TryGetValue("TurnIntoKudzu", out bVal)) seed.TurnIntoKudzu = bVal;

        // Step 1c: Restore harvest type from snapshot
        seed.HarvestRepeat = (HarvestType) genome.BaseHarvestType;

        // Step 2: Apply gene stat modifiers (additive first)
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var genProto))
                continue;

            foreach (var (statName, modifier) in genProto.StatModifiers)
            {
                ApplyStatAdditive(seed, statName, modifier);
            }
        }

        // Step 3: Apply gene stat multipliers
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var genProto))
                continue;

            foreach (var (statName, multiplier) in genProto.StatMultipliers)
            {
                ApplyStatMultiplier(seed, statName, multiplier);
            }
        }

        // Step 4: Apply epigenetic modifiers (additive)
        foreach (var epi in genome.Epigenetics)
        {
            foreach (var (statName, modifier) in epi.StatModifiers)
            {
                ApplyStatAdditive(seed, statName, modifier);
            }
        }

        // Step 5: Apply gene boolean traits and harvest upgrades
        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var genProto))
                continue;

            foreach (var (traitName, traitValue) in genProto.BooleanTraits)
            {
                ApplyBooleanTrait(seed, traitName, traitValue);
            }

            // Harvest upgrades only go up, never down
            if (genProto.HarvestUpgrade > 0 && (int) seed.HarvestRepeat < genProto.HarvestUpgrade)
            {
                seed.HarvestRepeat = (HarvestType) genProto.HarvestUpgrade;
            }
        }

        // Step 6: Restore base chemicals and apply gene chemical additions
        seed.Chemicals.Clear();
        foreach (var (chemId, snap) in genome.BaseChemSnapshot)
        {
            seed.Chemicals[chemId] = new SeedChemQuantity
            {
                Min = snap.Min,
                Max = snap.Max,
                PotencyDivisor = snap.PotDiv,
                Inherent = true,
            };
        }

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var genProto))
                continue;

            foreach (var (chemId, chemData) in genProto.ChemicalAdds)
            {
                if (!seed.Chemicals.ContainsKey(chemId))
                {
                    seed.Chemicals[chemId] = new SeedChemQuantity
                    {
                        Min = chemData.Min,
                        Max = chemData.Max,
                        PotencyDivisor = chemData.PotencyDivisor,
                        Inherent = false,
                    };
                }
            }
        }

        // Step 7: Recalculate instability
        genome.Instability = CalculateInstability(genome);

        Dirty(uid, genome);
    }

    /// <summary>
    /// Public method to recalculate instability only (without touching SeedData stats).
    /// Used by DNA Manipulator when genes are added/removed on seed packets (not in trays).
    /// </summary>
    public void RecalculateInstability(EntityUid uid, PlantGenomeComponent genome)
    {
        genome.Instability = CalculateInstability(genome);
        Dirty(uid, genome);
    }

    /// <summary>
    /// Calculates total instability from gene costs and slot penalties.
    /// </summary>
    private float CalculateInstability(PlantGenomeComponent genome)
    {
        var total = 0f;

        for (var i = 0; i < genome.GeneSlots.Count; i++)
        {
            var slot = genome.GeneSlots[i];
            if (slot.Gene == null)
                continue;

            if (_protoManager.TryIndex(slot.Gene.Value, out var genProto))
                total += genProto.InstabilityCost;

            // Slot penalties: slots 5-6 add +5 each, slots 7-8 add +10 each
            if (i >= 4 && i < 6)
                total += 5f;
            else if (i >= 6)
                total += 10f;
        }

        // Subtract stabilization from epigenetics
        foreach (var epi in genome.Epigenetics)
        {
            if (epi.StatModifiers.TryGetValue("Instability", out var instMod))
                total += instMod; // Negative values reduce instability
        }

        return Math.Max(0f, total);
    }

    private static void ApplyStatAdditive(SeedData seed, string statName, float value)
    {
        switch (statName)
        {
            case "Potency": seed.Potency += value; break;
            case "Yield": seed.Yield += (int) value; break;
            case "Lifespan": seed.Lifespan += value; break;
            case "Maturation": seed.Maturation += value; break;
            case "Production": seed.Production += value; break;
            case "Endurance": seed.Endurance += value; break;
            case "NutrientConsumption": seed.NutrientConsumption += value; break;
            case "WaterConsumption": seed.WaterConsumption += value; break;
            case "IdealHeat": seed.IdealHeat += value; break;
            case "HeatTolerance": seed.HeatTolerance += value; break;
            case "IdealLight": seed.IdealLight += value; break;
            case "LightTolerance": seed.LightTolerance += value; break;
            case "ToxinsTolerance": seed.ToxinsTolerance += value; break;
            case "LowPressureTolerance": seed.LowPressureTolerance += value; break;
            case "HighPressureTolerance": seed.HighPressureTolerance += value; break;
            case "PestTolerance": seed.PestTolerance += value; break;
            case "WeedTolerance": seed.WeedTolerance += value; break;
        }
    }

    private static void ApplyStatMultiplier(SeedData seed, string statName, float value)
    {
        switch (statName)
        {
            case "Potency": seed.Potency *= value; break;
            case "Yield": seed.Yield = (int) (seed.Yield * value); break;
            case "Lifespan": seed.Lifespan *= value; break;
            case "Maturation": seed.Maturation *= value; break;
            case "Production": seed.Production *= value; break;
            case "Endurance": seed.Endurance *= value; break;
            case "NutrientConsumption": seed.NutrientConsumption *= value; break;
            case "WaterConsumption": seed.WaterConsumption *= value; break;
            case "IdealHeat": seed.IdealHeat *= value; break;
            case "HeatTolerance": seed.HeatTolerance *= value; break;
            case "IdealLight": seed.IdealLight *= value; break;
            case "LightTolerance": seed.LightTolerance *= value; break;
            case "ToxinsTolerance": seed.ToxinsTolerance *= value; break;
            case "LowPressureTolerance": seed.LowPressureTolerance *= value; break;
            case "HighPressureTolerance": seed.HighPressureTolerance *= value; break;
            case "PestTolerance": seed.PestTolerance *= value; break;
            case "WeedTolerance": seed.WeedTolerance *= value; break;
        }
    }

    private static void ApplyBooleanTrait(SeedData seed, string traitName, bool value)
    {
        switch (traitName)
        {
            case "Seedless": seed.Seedless = value; break;
            case "Ligneous": seed.Ligneous = value; break;
            case "CanScream": seed.CanScream = value; break;
            case "TurnIntoKudzu": seed.TurnIntoKudzu = value; break;
        }
    }
}
