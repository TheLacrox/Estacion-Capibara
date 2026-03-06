// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives;

/// <summary>
///     Tracks the total number of cargo bounties fulfilled on a station.
///     Attached to station entities by the StationObjectivesRuleSystem.
/// </summary>
[RegisterComponent]
public sealed partial class StationBountyTrackerComponent : Component
{
    public int TotalFulfilled;
}
