// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Economy.Components;

/// <summary>
/// Attached to station entities to track payroll state: salary overrides,
/// freeze status from objectives, and account ID counter.
/// </summary>
[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class StationPayrollComponent : Component
{
    /// <summary>
    /// When true, payroll skips all salary payments.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool SalariesFrozen;

    /// <summary>
    /// Prevents duplicate freeze announcements.
    /// </summary>
    [DataField]
    public bool FreezeAnnouncementSent;

    /// <summary>
    /// Prevents duplicate restore announcements.
    /// </summary>
    [DataField]
    public bool RestoreAnnouncementSent;

    /// <summary>
    /// Per-account salary overrides. Key = AccountId, Value = salary amount.
    /// </summary>
    [DataField]
    public Dictionary<int, int> SalaryOverrides = new();

    /// <summary>
    /// Auto-incrementing counter for assigning unique AccountIds.
    /// </summary>
    [DataField]
    public int NextAccountId = 1;
}
