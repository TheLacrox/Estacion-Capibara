// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Ui;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._Capibara.Botany.UI;

[UsedImplicitly]
public sealed class DnaManipulatorBoundUserInterface : BoundUserInterface
{
    private DnaManipulatorWindow? _window;

    public DnaManipulatorBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = this.CreateWindow<DnaManipulatorWindow>();
        _window.OnExtractGene += slotIndex => SendMessage(new DnaManipulatorExtractMessage(slotIndex));
        _window.OnInsertGene += () => SendMessage(new DnaManipulatorInsertMessage());
        _window.OnEjectSeed += () => SendMessage(new DnaManipulatorEjectSeedMessage());
        _window.OnEjectDisk += () => SendMessage(new DnaManipulatorEjectDiskMessage());
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is DnaManipulatorBuiState manipState)
            _window?.UpdateState(manipState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
