// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Medical.Components;
using Content.Shared._Trauma.Medical;
using Robust.Shared.Containers;

namespace Content.Server._Trauma.Medical;

/// <summary>
/// Raises <see cref="ScannerInsertedEvent"/> and <see cref="ScannerEjectedEvent"/>
/// on the connected console when a mob enters or exits a medical scanner.
/// This bridges the container events to the genetics console (and any other console
/// that listens for these events, like the cloning console in Trauma upstream).
/// </summary>
public sealed class MedicalScannerEventsSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MedicalScannerComponent, EntInsertedIntoContainerMessage>(OnSubjectInserted);
        SubscribeLocalEvent<MedicalScannerComponent, EntRemovedFromContainerMessage>(OnSubjectRemoved);
    }

    private void OnSubjectInserted(Entity<MedicalScannerComponent> ent, ref EntInsertedIntoContainerMessage args)
    {
        if (ent.Comp.ConnectedConsole is not {} console || args.Container != ent.Comp.BodyContainer)
            return;

        var ev = new ScannerInsertedEvent(ent, args.Entity);
        RaiseLocalEvent(console, ref ev);
    }

    private void OnSubjectRemoved(Entity<MedicalScannerComponent> ent, ref EntRemovedFromContainerMessage args)
    {
        if (ent.Comp.ConnectedConsole is not {} console || args.Container != ent.Comp.BodyContainer)
            return;

        var ev = new ScannerEjectedEvent(ent, args.Entity);
        RaiseLocalEvent(console, ref ev);
    }
}
