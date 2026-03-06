// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.GameObjects;

namespace Content.Shared._Capibara.StationObjectives.Events;

/// <summary>
///     Raised when medicine reagents are produced via a ChemMaster (pills or bottles).
/// </summary>
[ByRefEvent]
public readonly record struct MedicineProducedEvent(FixedPoint2 Amount, EntityUid ChemMaster);
