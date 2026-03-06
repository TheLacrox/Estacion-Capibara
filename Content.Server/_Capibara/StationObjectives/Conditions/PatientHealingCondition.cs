// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class PatientHealingCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationHealingTrackerComponent>(station.Value, out var tracker))
            return false;

        proto.Parameters.TryGetValue("targetPatientsHealed", out var targetPatientsHealed);
        if (targetPatientsHealed <= 0)
            targetPatientsHealed = 30f;

        return tracker.TotalPatientsHealed >= (int) targetPatientsHealed;
    }
}
