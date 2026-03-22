// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// A machine that safely processes gene-modified produce into chemicals.
/// </summary>
[RegisterComponent]
public sealed partial class PlantCentrifugeComponent : Component
{
    public const string ProduceSlotId = "PlantCentrifuge-produce";

    [DataField(required: true)]
    public ItemSlot ProduceSlot = new();

    [DataField]
    public float YieldMultiplier = 1.5f;
}
