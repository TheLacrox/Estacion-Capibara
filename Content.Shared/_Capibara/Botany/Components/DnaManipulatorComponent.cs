// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// A machine that extracts genes from seeds onto gene disks, and inserts genes from disks into seeds.
/// </summary>
[RegisterComponent]
public sealed partial class DnaManipulatorComponent : Component
{
    public const string SeedSlotId = "DnaManipulator-seed";
    public const string DiskSlotId = "DnaManipulator-disk";

    [DataField(required: true)]
    public ItemSlot SeedSlot = new();

    [DataField(required: true)]
    public ItemSlot DiskSlot = new();
}
