// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// A machine that combines two gene disks to create a new gene.
/// </summary>
[RegisterComponent]
public sealed partial class GeneSplicerComponent : Component
{
    public const string DiskSlotAId = "GeneSplicer-diskA";
    public const string DiskSlotBId = "GeneSplicer-diskB";

    [DataField(required: true)]
    public ItemSlot DiskSlotA = new();

    [DataField(required: true)]
    public ItemSlot DiskSlotB = new();
}
