// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Economy;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.Economy.UI;

[UsedImplicitly]
public sealed class SalaryConsoleBoundUserInterface : BoundUserInterface
{
    private SalaryConsoleWindow? _window;

    public SalaryConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<SalaryConsoleWindow>();
        _window.OnUpdateSalary += (accountId, salary) =>
            SendMessage(new SalaryConsoleUpdateSalaryMessage(accountId, salary));
        _window.OnInsert += () => SendMessage(new SalaryConsoleInsertIdMessage());
        _window.OnEject += () => SendMessage(new SalaryConsoleEjectIdMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is SalaryConsoleState consoleState)
            _window?.UpdateState(consoleState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
