// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class BotanyHarvestCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationHarvestTrackerComponent>(station.Value, out var tracker))
            return false;

        proto.Parameters.TryGetValue("targetHarvests", out var targetHarvests);
        if (targetHarvests <= 0)
            targetHarvests = 30f;

        return tracker.TotalHarvested >= (int) targetHarvests;
    }
}
