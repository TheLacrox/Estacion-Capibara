// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared._Capibara.StationObjectives;

/// <summary>
///     Defines a group of station objectives with weighted random selection.
/// </summary>
[Prototype("stationObjectiveGroup")]
public sealed partial class StationObjectiveGroupPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public int MinObjectives { get; private set; } = 2;

    [DataField]
    public int MaxObjectives { get; private set; } = 3;

    [DataField(required: true)]
    public List<StationObjectiveEntry> Objectives { get; private set; } = new();
}

[DataDefinition]
public sealed partial class StationObjectiveEntry
{
    [DataField(required: true)]
    public ProtoId<StationObjectivePrototype> ObjectiveId { get; private set; } = default!;

    [DataField]
    public float Weight { get; private set; } = 1.0f;
}
