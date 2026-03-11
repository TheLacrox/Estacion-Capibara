// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.ShadowMonarch;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.ShadowMonarch.UI;

[UsedImplicitly]
public sealed class ShadowMonarchStatsBui : BoundUserInterface
{
    private ShadowMonarchStatsWindow? _window;

    public ShadowMonarchStatsBui(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<ShadowMonarchStatsWindow>();
        _window.OnAllocateStat += stat => SendMessage(new ShadowMonarchAllocateStatMessage(stat));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is ShadowMonarchStatsState statsState)
            _window?.UpdateState(statsState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
