// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.GameObjects;

namespace Content.Server._Capibara.StationObjectives;

/// <summary>
///     Tracks the total volume of medicine reagents produced via ChemMaster on a station.
///     Attached to station entities by the StationObjectivesRuleSystem.
/// </summary>
[RegisterComponent]
public sealed partial class StationMedicineTrackerComponent : Component
{
    public FixedPoint2 TotalMedicineVolume;
}
