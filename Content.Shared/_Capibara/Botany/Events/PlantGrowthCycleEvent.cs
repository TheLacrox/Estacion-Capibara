// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Capibara.Botany.Events;

/// <summary>
/// Raised at the end of each plant growth cycle.
/// MutationLevel is captured before the base system resets it to 0.
/// </summary>
[ByRefEvent]
public readonly record struct PlantGrowthCycleEvent(EntityUid PlantHolder, float MutationLevel);
