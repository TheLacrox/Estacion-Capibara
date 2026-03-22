// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Genes;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class PlantGeneSlot
{
    [DataField]
    public ProtoId<PlantGenePrototype>? Gene;

    [DataField]
    public bool Locked;
}
