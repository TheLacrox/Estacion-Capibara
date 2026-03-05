// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Containers.ItemSlots;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._Capibara.Economy.Components;

/// <summary>
/// Marks an entity as a salary management console.
/// Requires HOP/Captain ID insertion for authorization.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class SalaryConsoleComponent : Component
{
    public const string PrivilegedIdSlotId = "SalaryConsole-privilegedId";

    [DataField]
    public ItemSlot PrivilegedIdSlot = new();

    [DataField]
    public SoundSpecifier ErrorSound { get; private set; } = new SoundPathSpecifier("/Audio/Effects/Cargo/buzz_sigh.ogg");

    [DataField]
    public SoundSpecifier ConfirmSound { get; private set; } = new SoundPathSpecifier("/Audio/Effects/Cargo/ping.ogg");
}
