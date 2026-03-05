// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Content.Shared.Research.Components;
using Content.Shared.Station.Components;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class ResearchPointsCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        proto.Parameters.TryGetValue("targetPoints", out var targetPoints);
        if (targetPoints <= 0)
            targetPoints = 25000f;

        var totalPoints = 0;
        var query = entityManager.EntityQueryEnumerator<ResearchServerComponent, TransformComponent>();
        while (query.MoveNext(out _, out var research, out var xform))
        {
            if (xform.GridUid == null)
                continue;

            if (!entityManager.TryGetComponent<StationMemberComponent>(xform.GridUid, out var member))
                continue;

            if (member.Station != station.Value)
                continue;

            totalPoints += research.Points;
        }

        return totalPoints >= (int) targetPoints;
    }
}
