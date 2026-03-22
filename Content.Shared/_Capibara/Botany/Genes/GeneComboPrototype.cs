// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared._Capibara.Botany.Genes;

/// <summary>
/// Defines a multi-gene synergy combo. When a plant has all required genes,
/// produce gains additional behaviors or special items.
/// </summary>
[Prototype("geneCombo")]
public sealed partial class GeneComboPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public LocId Name = string.Empty;

    [DataField(required: true)]
    public LocId Description = string.Empty;

    /// <summary>
    /// All genes that must be present for this combo to activate.
    /// </summary>
    [DataField(required: true)]
    public List<ProtoId<PlantGenePrototype>> RequiredGenes = new();

    /// <summary>
    /// Minimum potency required for this combo.
    /// </summary>
    [DataField]
    public float MinPotency;

    /// <summary>
    /// Additional entity prototypes spawned alongside or instead of normal produce.
    /// </summary>
    [DataField]
    public List<EntProtoId> BonusProducePrototypes = new();

    /// <summary>
    /// If true, normal produce is not spawned — only the combo items.
    /// </summary>
    [DataField]
    public bool ReplacesNormalProduce;
}
