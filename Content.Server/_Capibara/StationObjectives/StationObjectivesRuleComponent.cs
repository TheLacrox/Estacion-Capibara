// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Threading;
using Content.Shared._Capibara.StationObjectives;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Server._Capibara.StationObjectives;

[RegisterComponent]
public sealed partial class StationObjectivesRuleComponent : Component
{
    [DataField]
    public ProtoId<StationObjectiveGroupPrototype> ObjectiveGroup { get; private set; } = "DefaultStationObjectives";

    [DataField]
    public float CheckInterval { get; private set; } = 30f;

    public CancellationTokenSource TimerCancel = new();

    public bool InitialFaxSent;

    public EntityUid? Station;

    public List<ActiveStationObjective> ActiveObjectives = new();
}

public sealed class ActiveStationObjective
{
    public ProtoId<StationObjectivePrototype> ObjectiveProtoId;
    public bool Completed;
    public bool NotificationSent;
    public Dictionary<string, object> RuntimeState = new();
}
