// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;

namespace Content.Shared._Capibara.StationObjectives.Events;

/// <summary>
///     Raised when a cargo bounty is successfully fulfilled.
/// </summary>
[ByRefEvent]
public readonly record struct BountyFulfilledEvent(EntityUid Station);
