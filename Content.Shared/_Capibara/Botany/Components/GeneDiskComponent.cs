// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// A gene disk that can store a single extracted plant gene.
/// Used with the DNA Manipulator to transfer genes between plants.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(raiseAfterAutoHandleState: true)]
public sealed partial class GeneDiskComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<PlantGenePrototype>? StoredGene;

    /// <summary>
    /// Disk integrity (0-100). Degrades when extracting Rare/Legendary genes.
    /// At 0, the disk is destroyed.
    /// </summary>
    [DataField, AutoNetworkedField]
    public float Integrity = 100f;

    /// <summary>
    /// If set, the disk will be filled with a random gene of this rarity on MapInit.
    /// Used for admin/testing spawn variants.
    /// </summary>
    [DataField]
    public PlantGeneRarity? PrefilledRarity;
}
