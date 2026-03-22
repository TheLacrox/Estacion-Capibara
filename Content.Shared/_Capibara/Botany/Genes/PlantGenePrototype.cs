// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared._Capibara.Botany.Genes;

/// <summary>
/// Defines a plant gene that can be inserted into a plant's genome via machines.
/// Genes modify plant stats, add chemicals, and add behaviors to harvested produce.
/// </summary>
[Prototype("plantGene")]
public sealed partial class PlantGenePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public LocId Name = string.Empty;

    [DataField(required: true)]
    public LocId Description = string.Empty;

    [DataField(required: true)]
    public PlantGeneRarity Rarity = PlantGeneRarity.Common;

    /// <summary>
    /// How much instability this gene adds to the plant.
    /// </summary>
    [DataField]
    public float InstabilityCost;

    /// <summary>
    /// Flat additions to SeedData stat fields. Key is field name (e.g. "Potency", "Yield").
    /// </summary>
    [DataField]
    public Dictionary<string, float> StatModifiers = new();

    /// <summary>
    /// Multiplicative modifiers to SeedData stat fields. Applied after additive. 1.0 = no change.
    /// </summary>
    [DataField]
    public Dictionary<string, float> StatMultipliers = new();

    /// <summary>
    /// Additional chemicals this gene adds to the plant's produce.
    /// </summary>
    [DataField]
    public Dictionary<string, SeedChemData> ChemicalAdds = new();

    /// <summary>
    /// Boolean trait toggles this gene sets (e.g. "Seedless" = true, "Ligneous" = true).
    /// Applied after stat modifiers during RecalculateStats().
    /// </summary>
    [DataField]
    public Dictionary<string, bool> BooleanTraits = new();

    /// <summary>
    /// Harvest type upgrade level. 0 = no change, 1 = at least Repeat, 2 = at least SelfHarvest.
    /// The gene upgrades the harvest type if the current level is lower.
    /// </summary>
    [DataField]
    public int HarvestUpgrade;

    /// <summary>
    /// Gene IDs that cannot coexist with this gene in the same plant.
    /// </summary>
    [DataField]
    public List<ProtoId<PlantGenePrototype>> Incompatible = new();

    /// <summary>
    /// If set, this gene requires this technology to be researched before it can be inserted.
    /// </summary>
    [DataField]
    public ProtoId<EntityPrototype>? RequiredResearch;

    /// <summary>
    /// The type of instability failure this gene causes (e.g. "explosive", "electrical", "toxic").
    /// Used to determine themed instability consequences.
    /// </summary>
    [DataField]
    public string? FailureMode;

    /// <summary>
    /// Color tint applied to the plant sprite when this gene is active.
    /// </summary>
    [DataField]
    public Color? VisualTint;

    /// <summary>
    /// RSI path for a visual effect overlay on the plant (e.g. "Effects/electricity.rsi").
    /// Rendered as an extra unshaded sprite layer on top of the plant.
    /// </summary>
    [DataField]
    public ResPath? EffectRsi;

    /// <summary>
    /// RSI state for the visual effect overlay.
    /// </summary>
    [DataField]
    public string? EffectState;
}

/// <summary>
/// Chemical data for gene-added chemicals (simplified version of SeedChemQuantity).
/// </summary>
[DataDefinition, Serializable, NetSerializable]
public sealed partial class SeedChemData
{
    [DataField]
    public int Min;

    [DataField]
    public int Max;

    [DataField]
    public int PotencyDivisor = 20;
}
