// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Roles;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared._Capibara.StationObjectives;

/// <summary>
///     Defines a single station objective that can be assigned during a round.
/// </summary>
[Prototype("stationObjective")]
public sealed partial class StationObjectivePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public string Name { get; private set; } = default!;

    [DataField(required: true)]
    public string Description { get; private set; } = default!;

    [DataField(required: true)]
    public string ConditionType { get; private set; } = default!;

    [DataField]
    public Dictionary<string, float> Parameters { get; private set; } = new();

    /// <summary>
    ///     Departments relevant to this objective. Used to weight selection
    ///     proportionally to department headcount at round start.
    ///     If empty, the objective uses its base weight regardless of staffing.
    /// </summary>
    [DataField]
    public List<ProtoId<DepartmentPrototype>> Departments { get; private set; } = new();
}
