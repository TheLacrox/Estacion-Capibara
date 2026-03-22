// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Ui;

[Serializable, NetSerializable]
public enum SeedAnalyzerUiKey : byte
{
    Key
}

/// <summary>
/// Data for a single gene slot displayed in the Seed Analyzer UI.
/// </summary>
[Serializable, NetSerializable]
public sealed class GeneSlotData
{
    public ProtoId<PlantGenePrototype>? GeneId;
    public string? GeneName;
    public string? GeneDescription;
    public PlantGeneRarity? Rarity;
    public bool Locked;

    public GeneSlotData(ProtoId<PlantGenePrototype>? geneId, string? geneName, string? geneDescription, PlantGeneRarity? rarity, bool locked)
    {
        GeneId = geneId;
        GeneName = geneName;
        GeneDescription = geneDescription;
        Rarity = rarity;
        Locked = locked;
    }
}

/// <summary>
/// State sent from server to client for the Seed Analyzer display.
/// </summary>
[Serializable, NetSerializable]
public sealed class SeedAnalyzerBuiState : BoundUserInterfaceState
{
    public bool HasSeed;
    public string? SpeciesName;

    // Base stats
    public float Potency;
    public int Yield;
    public float Lifespan;
    public float Maturation;
    public float Production;
    public float Endurance;

    // Tolerances
    public float IdealHeat;
    public float IdealLight;
    public float WaterConsumption;
    public float NutrientConsumption;

    // Genome info
    public List<GeneSlotData> GeneSlots = new();
    public float Instability;
    public List<string> EpigeneticNames = new();
    public int MaxSlots;
}

/// <summary>
/// Client requests to eject the item from the analyzer.
/// </summary>
[Serializable, NetSerializable]
public sealed class SeedAnalyzerEjectMessage : BoundUserInterfaceMessage
{
}
