// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Power.Components;
using Content.Shared._Capibara.StationObjectives;
using Content.Shared.Station.Components;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class PowerProductionCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        proto.Parameters.TryGetValue("targetWatts", out var targetWatts);
        if (targetWatts <= 0)
            targetWatts = 10_000_000_000f;

        proto.Parameters.TryGetValue("durationSeconds", out var durationSeconds);
        if (durationSeconds <= 0)
            durationSeconds = 60f;

        var totalSupply = 0f;
        var query = entityManager.EntityQueryEnumerator<PowerSupplierComponent, TransformComponent>();
        while (query.MoveNext(out _, out var supplier, out var xform))
        {
            if (xform.GridUid == null)
                continue;

            if (!entityManager.TryGetComponent<StationMemberComponent>(xform.GridUid, out var member))
                continue;

            if (member.Station != station.Value)
                continue;

            totalSupply += supplier.CurrentSupply;
        }

        if (totalSupply >= targetWatts)
        {
            if (!objective.RuntimeState.TryGetValue("sustainedTime", out var sustainedObj))
                sustainedObj = 0.0;

            var sustained = (double) sustainedObj + 30.0;
            objective.RuntimeState["sustainedTime"] = sustained;

            if (sustained >= durationSeconds)
                return true;
        }
        else
        {
            objective.RuntimeState["sustainedTime"] = 0.0;
        }

        return false;
    }
}
