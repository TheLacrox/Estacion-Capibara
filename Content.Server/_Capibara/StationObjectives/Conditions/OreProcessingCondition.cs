// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class OreProcessingCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationOreTrackerComponent>(station.Value, out var tracker))
            return false;

        proto.Parameters.TryGetValue("targetMaterialVolume", out var targetVolume);
        if (targetVolume <= 0)
            targetVolume = 30000f;

        return tracker.TotalMaterialVolume >= (int) targetVolume;
    }
}
