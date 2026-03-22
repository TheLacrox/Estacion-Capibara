// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared._Capibara.Botany.Genes;

/// <summary>
/// Defines a recipe for the Gene Splicer: two input genes produce one output gene.
/// </summary>
[Prototype("geneSpliceRecipe")]
public sealed partial class GeneSpliceRecipePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public ProtoId<PlantGenePrototype> InputA;

    [DataField(required: true)]
    public ProtoId<PlantGenePrototype> InputB;

    [DataField(required: true)]
    public ProtoId<PlantGenePrototype> Output;

    /// <summary>
    /// Chance of success (0-1). Failure destroys one or both disks.
    /// </summary>
    [DataField]
    public float SuccessChance = 0.5f;
}
