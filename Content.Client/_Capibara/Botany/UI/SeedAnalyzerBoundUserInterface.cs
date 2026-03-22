// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Ui;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.Botany.UI;

[UsedImplicitly]
public sealed class SeedAnalyzerBoundUserInterface : BoundUserInterface
{
    private SeedAnalyzerWindow? _window;

    public SeedAnalyzerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<SeedAnalyzerWindow>();
        _window.OnEjectPressed += () => SendMessage(new SeedAnalyzerEjectMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is SeedAnalyzerBuiState analyzerState)
            _window?.UpdateState(analyzerState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
