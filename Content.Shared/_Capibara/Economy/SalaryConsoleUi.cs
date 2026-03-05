// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Economy;

[Serializable, NetSerializable]
public enum SalaryConsoleUiKey : byte
{
    Key
}

[Serializable, NetSerializable]
public sealed class SalaryConsoleState : BoundUserInterfaceState
{
    public bool IsAuthorized;
    public bool HasPrivilegedId;
    public string? PrivilegedIdName;
    public bool SalariesCutOff;
    public List<SalaryEmployeeEntry> Employees;

    public SalaryConsoleState(
        bool isAuthorized,
        bool hasPrivilegedId,
        string? privilegedIdName,
        bool salariesCutOff,
        List<SalaryEmployeeEntry> employees)
    {
        IsAuthorized = isAuthorized;
        HasPrivilegedId = hasPrivilegedId;
        PrivilegedIdName = privilegedIdName;
        SalariesCutOff = salariesCutOff;
        Employees = employees;
    }
}

[Serializable, NetSerializable]
public sealed class SalaryEmployeeEntry
{
    public int AccountId;
    public string FullName;
    public string? JobTitle;
    public string? JobPrototype;
    public int BaseSalary;
    public int? SalaryOverride;
    public int CurrentBalance;
    public string? DepartmentName;
    public string? DepartmentColor;
    public int DepartmentWeight;

    public SalaryEmployeeEntry(
        int accountId,
        string fullName,
        string? jobTitle,
        string? jobPrototype,
        int baseSalary,
        int? salaryOverride,
        int currentBalance,
        string? departmentName,
        string? departmentColor,
        int departmentWeight)
    {
        AccountId = accountId;
        FullName = fullName;
        JobTitle = jobTitle;
        JobPrototype = jobPrototype;
        BaseSalary = baseSalary;
        SalaryOverride = salaryOverride;
        CurrentBalance = currentBalance;
        DepartmentName = departmentName;
        DepartmentColor = departmentColor;
        DepartmentWeight = departmentWeight;
    }
}

[Serializable, NetSerializable]
public sealed class SalaryConsoleUpdateSalaryMessage : BoundUserInterfaceMessage
{
    public int AccountId;
    public int NewSalary;

    public SalaryConsoleUpdateSalaryMessage(int accountId, int newSalary)
    {
        AccountId = accountId;
        NewSalary = newSalary;
    }
}

[Serializable, NetSerializable]
public sealed class SalaryConsoleInsertIdMessage : BoundUserInterfaceMessage
{
}

[Serializable, NetSerializable]
public sealed class SalaryConsoleEjectIdMessage : BoundUserInterfaceMessage
{
}
