// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Ui;

[Serializable, NetSerializable]
public enum PlantAnalyzerUiKey : byte
{
    Key
}

[Serializable, NetSerializable]
public sealed class PlantAnalyzerBuiState : BoundUserInterfaceState
{
    public string? SpeciesName;

    // Stats
    public float Potency;
    public int Yield;
    public float Lifespan;
    public float Maturation;
    public float Production;
    public float Endurance;

    // Environment
    public float IdealHeat;
    public float HeatTolerance;
    public float IdealLight;
    public float WaterConsumption;
    public float NutrientConsumption;

    // Current state
    public float Health;
    public float MaxHealth;
    public int Age;
    public float WaterLevel;
    public float NutritionLevel;
    public float PestLevel;
    public float WeedLevel;
    public float Toxins;
    public bool Dead;
    public bool Harvest;
    public bool Seedless;
    public bool Ligneous;
    public string HarvestType = "";

    // Mutation
    public float MutationLevel;
    public float MutationMod;
    public float GeneDiscoveryChance;

    // Genome
    public bool HasGenome;
    public List<GeneSlotData> GeneSlots = new();
    public float Instability;
    public List<string> EpigeneticNames = new();
    public int MaxSlots;
    public int FilledSlots;
    public int EmptySlots;

    // Chemistry
    public List<string> ChemicalNames = new();
}
