// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// A machine that displays the full genome readout of an inserted seed or produce.
/// </summary>
[RegisterComponent]
public sealed partial class SeedAnalyzerComponent : Component
{
    public const string SeedSlotId = "SeedAnalyzer-seed";

    [DataField(required: true)]
    public ItemSlot SeedSlot = new();
}
