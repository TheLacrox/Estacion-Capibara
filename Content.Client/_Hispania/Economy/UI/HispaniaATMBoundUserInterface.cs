// SPDX-FileCopyrightText: 2025 Hispania Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Hispania.Economy;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Hispania.Economy.UI;

[UsedImplicitly]
public sealed class HispaniaATMBoundUserInterface : BoundUserInterface
{
    private HispaniaATMWindow? _window;

    public HispaniaATMBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<HispaniaATMWindow>();
        _window.OnWithdraw += amount => SendMessage(new HispaniaATMWithdrawMessage(amount));
        _window.OnDeposit += () => SendMessage(new HispaniaATMDepositMessage());
        _window.OnInsert += () => SendMessage(new HispaniaATMInsertMessage());
        _window.OnEject += () => SendMessage(new HispaniaATMEjectMessage());
        _window.OnCreateAccount += () => SendMessage(new HispaniaATMCreateAccountMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is HispaniaATMState atmState)
            _window?.UpdateState(atmState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
