// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.StationObjectives;
using Content.Shared.Cargo.Components;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives.Conditions;

public sealed class CargoExportCondition : IStationObjectiveCondition
{
    public bool CheckCondition(
        ActiveStationObjective objective,
        StationObjectivePrototype proto,
        EntityUid? station,
        IEntityManager entityManager)
    {
        if (station == null)
            return false;

        if (!entityManager.TryGetComponent<StationBankAccountComponent>(station.Value, out var bank))
            return false;

        proto.Parameters.TryGetValue("targetCredits", out var targetCredits);
        if (targetCredits <= 0)
            targetCredits = 50000f;

        var currentBalance = 0;
        foreach (var account in bank.Accounts.Values)
        {
            currentBalance += account;
        }

        if (!objective.RuntimeState.TryGetValue("initialBalance", out var initialObj))
        {
            objective.RuntimeState["initialBalance"] = (double) currentBalance;
            return false;
        }

        var initialBalance = (int) (double) initialObj;
        var netEarned = currentBalance - initialBalance;

        return netEarned >= (int) targetCredits;
    }
}
