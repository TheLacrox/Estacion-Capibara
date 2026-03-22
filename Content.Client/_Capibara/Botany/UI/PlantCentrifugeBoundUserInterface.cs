// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Ui;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.Botany.UI;

[UsedImplicitly]
public sealed class PlantCentrifugeBoundUserInterface : BoundUserInterface
{
    private PlantCentrifugeWindow? _window;

    public PlantCentrifugeBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<PlantCentrifugeWindow>();
        _window.OnProcess += () => SendMessage(new PlantCentrifugeProcessMessage());
        _window.OnEject += () => SendMessage(new PlantCentrifugeEjectMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is PlantCentrifugeBuiState centState)
            _window?.UpdateState(centState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
