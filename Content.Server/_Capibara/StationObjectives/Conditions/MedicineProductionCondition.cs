// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class MedicineProductionCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationMedicineTrackerComponent>(station.Value, out var tracker))
            return false;

        proto.Parameters.TryGetValue("targetMedicineUnits", out var targetMedicineUnits);
        if (targetMedicineUnits <= 0)
            targetMedicineUnits = 500f;

        return tracker.TotalMedicineVolume >= (FixedPoint2) targetMedicineUnits;
    }
}
