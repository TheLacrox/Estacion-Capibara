// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Ui;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.Botany.UI;

[UsedImplicitly]
public sealed class GeneSplicerBoundUserInterface : BoundUserInterface
{
    private GeneSplicerWindow? _window;

    public GeneSplicerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<GeneSplicerWindow>();
        _window.OnSplice += () => SendMessage(new GeneSplicerSpliceMessage());
        _window.OnEjectA += () => SendMessage(new GeneSplicerEjectAMessage());
        _window.OnEjectB += () => SendMessage(new GeneSplicerEjectBMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is GeneSplicerBuiState splicerState)
            _window?.UpdateState(splicerState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
