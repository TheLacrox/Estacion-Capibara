// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

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
}
