// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Linq;
using System.Threading;
using Content.Server._Capibara.StationObjectives.Conditions;
using Content.Server.Chat.Systems;
using Content.Server.Fax;
using Content.Server.GameTicking.Rules;
using Content.Shared._Capibara.Economy.Components;
using Content.Shared._Capibara.StationObjectives;
using Content.Shared._Capibara.StationObjectives.Events;
using Content.Shared.Fax.Components;
using Content.Server.GameTicking;
using Content.Server.GameTicking.Events;
using Content.Shared.GameTicking.Components;
using Content.Shared.Paper;
using Content.Shared.Station.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Timer = Robust.Shared.Timing.Timer;

namespace Content.Server._Capibara.StationObjectives;

public sealed class StationObjectivesRuleSystem : GameRuleSystem<StationObjectivesRuleComponent>
{
    [Dependency] private readonly ChatSystem _chatSystem = default!;
    [Dependency] private readonly FaxSystem _faxSystem = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    private readonly Dictionary<string, IStationObjectiveCondition> _conditions = new();

    public override void Initialize()
    {
        base.Initialize();

        // Auto-inject station objectives into every round, regardless of game preset.
        // This avoids having to edit every upstream preset YAML (which causes merge conflicts).
        SubscribeLocalEvent<RoundStartingEvent>(OnRoundStarting);
        SubscribeLocalEvent<PlantHarvestedEvent>(OnPlantHarvested);

        _conditions["PowerProductionCondition"] = new PowerProductionCondition();
        _conditions["CargoExportCondition"] = new CargoExportCondition();
        _conditions["ResearchPointsCondition"] = new ResearchPointsCondition();
        _conditions["BotanyHarvestCondition"] = new BotanyHarvestCondition();
    }

    private void OnRoundStarting(RoundStartingEvent ev)
    {
        // Only add if no StationObjectivesRule is already present (e.g., from the preset)
        var query = EntityQueryEnumerator<StationObjectivesRuleComponent>();
        if (query.MoveNext(out _, out _))
            return;

        // Use StartGameRule since RoundStartingEvent fires after StartGamePresetRules(),
        // meaning AddGameRule alone would leave the rule unstarted.
        GameTicker.StartGameRule("StationObjectivesRule");
    }

    protected override void Started(EntityUid uid, StationObjectivesRuleComponent component, GameRuleComponent gameRule, GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);

        // Prevent duplicate instances — if another active rule already exists, skip
        var query = EntityQueryEnumerator<StationObjectivesRuleComponent, GameRuleComponent>();
        while (query.MoveNext(out var otherUid, out _, out var otherRule))
        {
            if (otherUid != uid && GameTicker.IsGameRuleActive(otherUid, otherRule))
                return;
        }

        // Reset state in case the component persisted across rounds
        component.InitialFaxSent = false;
        component.Station = null;
        component.ActiveObjectives.Clear();
        component.TimerCancel = new CancellationTokenSource();

        SelectObjectives(component);
        StartCheckTimer(uid, component);

        // Schedule salary cutoff check after 30 minutes
        var capturedUid = uid;
        Timer.Spawn(TimeSpan.FromMinutes(30), () => CheckSalaryCutoff(capturedUid), component.TimerCancel.Token);
    }

    protected override void Ended(EntityUid uid, StationObjectivesRuleComponent component, GameRuleComponent gameRule, GameRuleEndedEvent args)
    {
        base.Ended(uid, component, gameRule, args);

        // Cancel all timers and clear state to prevent any lingering callbacks
        component.TimerCancel.Cancel();
        component.InitialFaxSent = true; // Prevent fax from being sent if a timer somehow fires late
        component.ActiveObjectives.Clear();
        component.Station = null;
    }

    private void SelectObjectives(StationObjectivesRuleComponent component)
    {
        if (!_prototypeManager.TryIndex(component.ObjectiveGroup, out var group))
            return;

        var count = RobustRandom.Next(group.MinObjectives, group.MaxObjectives + 1);
        count = Math.Min(count, group.Objectives.Count);

        var available = group.Objectives.ToList();
        var totalWeight = available.Sum(e => e.Weight);

        for (var i = 0; i < count && available.Count > 0; i++)
        {
            var roll = RobustRandom.NextFloat() * totalWeight;
            var cumulative = 0f;

            for (var j = 0; j < available.Count; j++)
            {
                cumulative += available[j].Weight;
                if (roll <= cumulative)
                {
                    component.ActiveObjectives.Add(new ActiveStationObjective
                    {
                        ObjectiveProtoId = available[j].ObjectiveId,
                    });
                    totalWeight -= available[j].Weight;
                    available.RemoveAt(j);
                    break;
                }
            }
        }
    }

    private void StartCheckTimer(EntityUid uid, StationObjectivesRuleComponent component)
    {
        component.TimerCancel.Cancel();
        component.TimerCancel = new CancellationTokenSource();

        var capturedUid = uid;
        var interval = TimeSpan.FromSeconds(component.CheckInterval);
        Timer.Spawn(interval, () => CheckAllObjectives(capturedUid), component.TimerCancel.Token);
    }

    private void CheckAllObjectives(EntityUid uid)
    {
        // Verify the rule entity still exists and is active
        if (!TryComp<StationObjectivesRuleComponent>(uid, out var component))
            return;

        if (!TryComp<GameRuleComponent>(uid, out var gameRule) || !GameTicker.IsGameRuleActive(uid, gameRule))
            return;

        if (component.TimerCancel.IsCancellationRequested)
            return;

        var station = component.Station;

        // Try to find a station if we don't have one yet.
        if (station == null || !HasComp<StationDataComponent>(station.Value))
        {
            var query = EntityQueryEnumerator<StationDataComponent>();
            if (query.MoveNext(out var stationUid, out _))
            {
                component.Station = stationUid;
                station = stationUid;
                EnsureComp<StationHarvestTrackerComponent>(station.Value);
            }
        }

        // Send initial objectives fax so there's always a paper copy on the bridge
        if (!component.InitialFaxSent && station != null && component.ActiveObjectives.Count > 0)
        {
            component.InitialFaxSent = true;
            SendInitialObjectivesFax(component);
        }

        var allComplete = true;

        foreach (var objective in component.ActiveObjectives)
        {
            if (objective.Completed)
                continue;

            allComplete = false;

            if (!_prototypeManager.TryIndex(objective.ObjectiveProtoId, out var proto))
                continue;

            if (!_conditions.TryGetValue(proto.ConditionType, out var condition))
                continue;

            if (condition.CheckCondition(objective, proto, station, EntityManager))
            {
                objective.Completed = true;

                if (!objective.NotificationSent)
                {
                    objective.NotificationSent = true;
                    SendCompletionFax(component, proto);
                }
            }
        }

        // If all objectives are now complete, restore salaries if they were frozen
        if (allComplete && station != null && TryComp<StationPayrollComponent>(station.Value, out var payroll))
        {
            if (payroll.SalariesFrozen)
            {
                payroll.SalariesFrozen = false;
                payroll.RestoreAnnouncementSent = true;

                _chatSystem.DispatchStationAnnouncement(
                    station.Value,
                    Loc.GetString("capibara-salary-objectives-restored"),
                    Loc.GetString("capibara-salary-objectives-sender"),
                    colorOverride: Color.FromHex("#00CC00"));
            }
        }

        // Re-schedule if not all complete and not cancelled.
        if (!allComplete && !component.TimerCancel.IsCancellationRequested)
        {
            var capturedUid = uid;
            var interval = TimeSpan.FromSeconds(component.CheckInterval);
            Timer.Spawn(interval, () => CheckAllObjectives(capturedUid), component.TimerCancel.Token);
        }
    }

    private void CheckSalaryCutoff(EntityUid uid)
    {
        // Verify the rule entity still exists and is active
        if (!TryComp<StationObjectivesRuleComponent>(uid, out var component))
            return;

        if (!TryComp<GameRuleComponent>(uid, out var gameRule) || !GameTicker.IsGameRuleActive(uid, gameRule))
            return;

        if (component.TimerCancel.IsCancellationRequested)
            return;

        // Check if all objectives are already complete
        var allComplete = component.ActiveObjectives.All(o => o.Completed);
        if (allComplete)
            return;

        var station = component.Station;
        if (station == null)
            return;

        var payroll = EnsureComp<StationPayrollComponent>(station.Value);
        if (payroll.SalariesFrozen)
            return;

        payroll.SalariesFrozen = true;
        payroll.FreezeAnnouncementSent = true;

        _chatSystem.DispatchStationAnnouncement(
            station.Value,
            Loc.GetString("capibara-salary-objectives-frozen"),
            Loc.GetString("capibara-salary-objectives-sender"),
            colorOverride: Color.FromHex("#CC0000"));
    }

    private void SendInitialObjectivesFax(StationObjectivesRuleComponent component)
    {
        var content = BuildObjectivesPaperContent(component);

        var printout = new FaxPrintout(
            content,
            Loc.GetString("station-objective-paper-name"),
            null,
            null,
            "paper_stamp-centcom",
            new List<StampDisplayInfo>
            {
                new()
                {
                    StampedName = Loc.GetString("stamp-component-stamped-name-centcom"),
                    StampedColor = Color.FromHex("#BB3232"),
                },
            }
        );

        var faxes = EntityQueryEnumerator<FaxMachineComponent>();
        while (faxes.MoveNext(out var faxEnt, out var fax))
        {
            if (!fax.ReceiveNukeCodes)
                continue;

            _faxSystem.Receive(faxEnt, printout, null, fax);
        }
    }

    private void SendCompletionFax(StationObjectivesRuleComponent component, StationObjectivePrototype completedProto)
    {
        var completedName = Loc.GetString(completedProto.Name);

        // Build status list for all objectives
        var statusLines = "";
        foreach (var obj in component.ActiveObjectives)
        {
            if (!_prototypeManager.TryIndex(obj.ObjectiveProtoId, out var objProto))
                continue;

            var name = Loc.GetString(objProto.Name);
            var status = obj.Completed
                ? Loc.GetString("station-objective-fax-status-completed")
                : Loc.GetString("station-objective-fax-status-pending");

            statusLines += $"  - {name}: {status}\n";
        }

        var content = Loc.GetString("station-objective-fax-completion",
            ("objective", completedName),
            ("statuses", statusLines.TrimEnd('\n')));

        var printout = new FaxPrintout(
            content,
            Loc.GetString("station-objective-fax-completion-name"),
            null,
            null,
            "paper_stamp-centcom",
            new List<StampDisplayInfo>
            {
                new()
                {
                    StampedName = Loc.GetString("stamp-component-stamped-name-centcom"),
                    StampedColor = Color.FromHex("#BB3232"),
                },
            }
        );

        var faxes = EntityQueryEnumerator<FaxMachineComponent>();
        while (faxes.MoveNext(out var faxEnt, out var fax))
        {
            if (!fax.ReceiveNukeCodes)
                continue;

            _faxSystem.Receive(faxEnt, printout, null, fax);
        }
    }


    private string BuildObjectivesPaperContent(StationObjectivesRuleComponent component)
    {
        var objectives = "";
        for (var i = 0; i < component.ActiveObjectives.Count; i++)
        {
            var objective = component.ActiveObjectives[i];
            if (!_prototypeManager.TryIndex(objective.ObjectiveProtoId, out var proto))
                continue;

            var description = Loc.GetString(proto.Description, GetObjectiveLocArgs(proto));
            objectives += $"[bold]{i + 1}.[/bold] {description}\n";
        }

        return Loc.GetString("station-objective-paper-content", ("objectives", objectives.TrimEnd('\n')));
    }

    private (string, object)[] GetObjectiveLocArgs(StationObjectivePrototype proto)
    {
        var args = new List<(string, object)>();
        foreach (var (key, value) in proto.Parameters)
        {
            args.Add((key, value));
        }
        return args.ToArray();
    }

    protected override void AppendRoundEndText(EntityUid uid, StationObjectivesRuleComponent component, GameRuleComponent gameRule, ref RoundEndTextAppendEvent args)
    {
        if (component.ActiveObjectives.Count == 0)
            return;

        args.AddLine(Loc.GetString("station-objective-round-end-header"));

        foreach (var objective in component.ActiveObjectives)
        {
            if (!_prototypeManager.TryIndex(objective.ObjectiveProtoId, out var proto))
                continue;

            var name = Loc.GetString(proto.Name);
            var status = objective.Completed
                ? Loc.GetString("station-objective-round-end-completed")
                : Loc.GetString("station-objective-round-end-failed");

            args.AddLine($"  - {name}: {status}");
        }
    }

    private void OnPlantHarvested(ref PlantHarvestedEvent ev)
    {
        var xform = Transform(ev.PlantHolder);
        if (xform.GridUid == null)
            return;

        // Find the station this grid belongs to.
        if (!TryComp<StationMemberComponent>(xform.GridUid, out var member))
            return;

        if (!TryComp<StationHarvestTrackerComponent>(member.Station, out var tracker))
            return;

        tracker.TotalHarvested++;
    }
}
