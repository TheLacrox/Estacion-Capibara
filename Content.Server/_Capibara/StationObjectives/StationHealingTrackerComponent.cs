// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives;

/// <summary>
///     Tracks the total number of patients healed from critical/dead state on a station.
///     Attached to station entities by the StationObjectivesRuleSystem.
/// </summary>
[RegisterComponent]
public sealed partial class StationHealingTrackerComponent : Component
{
    public int TotalPatientsHealed;
}
