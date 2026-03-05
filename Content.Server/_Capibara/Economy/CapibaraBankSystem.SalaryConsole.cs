// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Chat.Managers;
using Content.Shared._Capibara.Economy;
using Content.Shared._Capibara.Economy.Components;
using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Mobs.Components;
using Content.Shared.PDA;
using Content.Shared.Roles;
using Content.Shared.Station.Components;
using Robust.Shared.Prototypes;
using Content.Shared.UserInterface;
using Robust.Server.Player;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;
using Robust.Shared.Player;

namespace Content.Server._Capibara.Economy;

/// <summary>
/// Partial class handling Salary Console BUI interactions.
/// </summary>
public sealed partial class CapibaraBankSystem
{
    [Dependency] private readonly AccessReaderSystem _accessReader = default!;
    [Dependency] private readonly IChatManager _chatManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    private void InitializeSalaryConsole()
    {
        Subs.BuiEvents<SalaryConsoleComponent>(SalaryConsoleUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnSalaryConsoleUIOpen);
            subs.Event<SalaryConsoleUpdateSalaryMessage>(OnUpdateSalary);
            subs.Event<SalaryConsoleInsertIdMessage>(OnSalaryConsoleInsert);
            subs.Event<SalaryConsoleEjectIdMessage>(OnSalaryConsoleEject);
        });

        SubscribeLocalEvent<SalaryConsoleComponent, EntInsertedIntoContainerMessage>(OnSalaryConsoleItemInserted);
        SubscribeLocalEvent<SalaryConsoleComponent, EntRemovedFromContainerMessage>(OnSalaryConsoleItemRemoved);
    }

    private bool PrivilegedIdIsAuthorized(EntityUid uid, SalaryConsoleComponent comp)
    {
        if (!TryComp<AccessReaderComponent>(uid, out var reader))
            return true;

        var privilegedId = comp.PrivilegedIdSlot.Item;
        return privilegedId != null && _accessReader.IsAllowed(privilegedId.Value, uid, reader);
    }

    private void UpdateSalaryConsoleUiState(EntityUid uid, SalaryConsoleComponent comp)
    {
        var hasPrivilegedId = comp.PrivilegedIdSlot.HasItem;
        var isAuthorized = PrivilegedIdIsAuthorized(uid, comp);

        string? privilegedIdName = null;
        if (comp.PrivilegedIdSlot.Item is { } privItem)
        {
            var privIdCard = GetIdCardFromEntity(privItem);
            if (privIdCard != null && TryComp<IdCardComponent>(privIdCard.Value, out var privCard))
                privilegedIdName = privCard.FullName;
        }

        // Find the station payroll component
        var salariesFrozen = false;
        StationPayrollComponent? payroll = null;
        var stationQuery = EntityQueryEnumerator<StationPayrollComponent, StationDataComponent>();
        while (stationQuery.MoveNext(out _, out var sp, out _))
        {
            payroll = sp;
            salariesFrozen = sp.SalariesFrozen;
            break;
        }

        // Build employee list — filter to only show station crew
        var excludedDepartments = new HashSet<string> { "CentralCommand" };

        var employees = new List<SalaryEmployeeEntry>();
        var query = EntityQueryEnumerator<BankAccountComponent, IdCardComponent>();
        while (query.MoveNext(out _, out var bank, out var idCard))
        {
            // Skip accounts with no valid ID
            if (bank.AccountId <= 0)
                continue;

            // Use JobDepartments from the ID card (set at spawn by the job system)
            // Skip antagonist/CentCom departments
            var skip = false;
            foreach (var deptId in idCard.JobDepartments)
            {
                if (excludedDepartments.Contains(deptId.Id))
                {
                    skip = true;
                    break;
                }
            }
            if (skip)
                continue;

            var jobId = idCard.JobPrototype?.Id;
            var baseSalary = GetSalary(jobId);
            int? salaryOverride = null;

            if (payroll != null && payroll.SalaryOverrides.TryGetValue(bank.AccountId, out var overrideVal))
                salaryOverride = overrideVal;

            // Get department from the ID card's JobDepartments list
            string? deptName = null;
            string? deptColor = null;
            var deptWeight = int.MaxValue;

            if (idCard.JobDepartments.Count > 0)
            {
                // Use the first department (primary one) from the ID card
                var firstDeptId = idCard.JobDepartments[0];
                if (_prototypeManager.TryIndex(firstDeptId, out var deptProto))
                {
                    deptName = Loc.GetString(deptProto.Name);
                    deptColor = deptProto.Color.ToHex();
                    deptWeight = deptProto.Weight;
                }
            }

            employees.Add(new SalaryEmployeeEntry(
                bank.AccountId,
                idCard.FullName ?? "Unknown",
                idCard.LocalizedJobTitle,
                jobId,
                baseSalary,
                salaryOverride,
                bank.Balance,
                deptName,
                deptColor,
                deptWeight
            ));
        }

        // Sort by department weight (higher weight first), then account ID
        employees.Sort((a, b) =>
        {
            var deptCmp = b.DepartmentWeight.CompareTo(a.DepartmentWeight);
            return deptCmp != 0 ? deptCmp : a.AccountId.CompareTo(b.AccountId);
        });

        _uiSystem.SetUiState(uid, SalaryConsoleUiKey.Key,
            new SalaryConsoleState(isAuthorized, hasPrivilegedId, privilegedIdName, salariesFrozen, employees));
    }

    private void OnSalaryConsoleUIOpen(EntityUid uid, SalaryConsoleComponent comp, BoundUIOpenedEvent args)
    {
        UpdateSalaryConsoleUiState(uid, comp);
    }

    private void OnUpdateSalary(EntityUid uid, SalaryConsoleComponent comp, SalaryConsoleUpdateSalaryMessage args)
    {
        if (!PrivilegedIdIsAuthorized(uid, comp))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            return;
        }

        if (args.NewSalary < 0)
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            return;
        }

        // Find station payroll
        StationPayrollComponent? payroll = null;
        EntityUid? stationUid = null;
        var stationQuery = EntityQueryEnumerator<StationPayrollComponent, StationDataComponent>();
        while (stationQuery.MoveNext(out var sUid, out var sp, out _))
        {
            payroll = sp;
            stationUid = sUid;
            break;
        }

        if (payroll == null)
        {
            // Create one on the first station we find
            var dataQuery = EntityQueryEnumerator<StationDataComponent>();
            if (dataQuery.MoveNext(out var dataUid, out _))
            {
                payroll = EnsureComp<StationPayrollComponent>(dataUid);
                stationUid = dataUid;
            }
        }

        if (payroll == null)
            return;

        // Store the override
        payroll.SalaryOverrides[args.AccountId] = args.NewSalary;

        _audio.PlayPvs(comp.ConfirmSound, uid);

        // Notify the affected player
        NotifySalaryChange(args.AccountId, args.NewSalary);

        UpdateSalaryConsoleUiState(uid, comp);
    }

    private void NotifySalaryChange(int accountId, int newSalary)
    {
        var query = EntityQueryEnumerator<BankAccountComponent>();
        while (query.MoveNext(out var idCardUid, out var bank))
        {
            if (bank.AccountId != accountId)
                continue;

            var holder = FindHolderFromEntity(idCardUid);
            if (holder == null)
                continue;

            if (!_playerManager.TryGetSessionByEntity(holder.Value, out var session))
                continue;

            _chatManager.DispatchServerMessage(session,
                Loc.GetString("capibara-salary-console-salary-changed", ("amount", newSalary)));
            return;
        }
    }

    private void OnSalaryConsoleInsert(EntityUid uid, SalaryConsoleComponent comp, SalaryConsoleInsertIdMessage args)
    {
        var player = args.Actor;

        if (comp.PrivilegedIdSlot.HasItem)
            return;

        if (!_inventory.TryGetSlotEntity(player, "id", out var idSlot))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            return;
        }

        if (_inventory.TryUnequip(player, "id", force: true) &&
            _itemSlotsSystem.TryInsert(uid, comp.PrivilegedIdSlot, idSlot.Value, player))
        {
            _audio.PlayPvs(comp.ConfirmSound, uid);
        }
    }

    private void OnSalaryConsoleEject(EntityUid uid, SalaryConsoleComponent comp, SalaryConsoleEjectIdMessage args)
    {
        _itemSlotsSystem.TryEjectToHands(uid, comp.PrivilegedIdSlot, args.Actor);
    }

    private void OnSalaryConsoleItemInserted(EntityUid uid, SalaryConsoleComponent comp, EntInsertedIntoContainerMessage args)
    {
        UpdateSalaryConsoleUiState(uid, comp);
    }

    private void OnSalaryConsoleItemRemoved(EntityUid uid, SalaryConsoleComponent comp, EntRemovedFromContainerMessage args)
    {
        UpdateSalaryConsoleUiState(uid, comp);
    }

    /// <summary>
    /// Get an ID card from an entity that might be a PDA or an ID card directly.
    /// </summary>
    private EntityUid? GetIdCardFromEntity(EntityUid entity)
    {
        if (TryComp<PdaComponent>(entity, out var pda) && pda.ContainedId != null)
            return pda.ContainedId.Value;

        if (HasComp<IdCardComponent>(entity))
            return entity;

        return null;
    }

    /// <summary>
    /// Walk up the container hierarchy to find the mob holding this entity.
    /// </summary>
    private EntityUid? FindHolderFromEntity(EntityUid entity)
    {
        var current = entity;

        for (var i = 0; i < 5; i++)
        {
            if (HasComp<MobStateComponent>(current))
                return current;

            if (!TryComp<TransformComponent>(current, out var xform) || xform.ParentUid == EntityUid.Invalid)
                return null;

            current = xform.ParentUid;
        }

        return null;
    }

    /// <summary>
    /// Get the effective salary for a given account, checking overrides first.
    /// </summary>
    public int GetSalaryForAccount(int accountId, string? jobId, StationPayrollComponent? payroll)
    {
        if (payroll != null && payroll.SalaryOverrides.TryGetValue(accountId, out var overrideSalary))
            return overrideSalary;

        return GetSalary(jobId);
    }

}
