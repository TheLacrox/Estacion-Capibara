// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class CargoBountyCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationBountyTrackerComponent>(station.Value, out var tracker))
            return false;

        proto.Parameters.TryGetValue("targetBounties", out var targetBounties);
        if (targetBounties <= 0)
            targetBounties = 20f;

        return tracker.TotalFulfilled >= (int) targetBounties;
    }
}
