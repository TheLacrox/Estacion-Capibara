// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Linq;
using Content.Server.Medical.Components;
using Content.Shared.DeviceLinking;
using Content.Shared.DeviceLinking.Events;
using Content.Shared._Trauma.Genetics.Console;
using Content.Shared._Trauma.Medical;

namespace Content.Server._Trauma.Genetics.Console;

/// <summary>
/// Server-side handler for linking the genetics console to a medical scanner via DeviceLink.
/// Sets ConnectedConsole on the medical scanner so scanner events are raised on the console.
/// </summary>
public sealed class GeneticsConsoleDeviceLinkSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneticsScannerComponent, MapInitEvent>(OnGeneticsMapInit);
        SubscribeLocalEvent<GeneticsScannerComponent, NewLinkEvent>(OnNewLink);
        SubscribeLocalEvent<GeneticsScannerComponent, PortDisconnectedEvent>(OnPortDisconnected);
    }

    private void OnGeneticsMapInit(Entity<GeneticsScannerComponent> ent, ref MapInitEvent args)
    {
        if (!TryComp<DeviceLinkSourceComponent>(ent, out var source))
            return;

        // Check existing links for medical scanners
        foreach (var port in source.Outputs.Values.SelectMany(ports => ports))
        {
            if (TryComp<MedicalScannerComponent>(port, out var scanner))
            {
                scanner.ConnectedConsole = ent;
                // Raise connected event on the genetics console
                var connEv = new ScannerConnectedEvent(port);
                RaiseLocalEvent(ent, ref connEv);

                // If a mob is already in the scanner (e.g. map load), notify the console
                if (scanner.BodyContainer.ContainedEntity is {} mob)
                {
                    var insertEv = new ScannerInsertedEvent(port, mob);
                    RaiseLocalEvent(ent, ref insertEv);
                }
            }
        }
    }

    private void OnNewLink(Entity<GeneticsScannerComponent> ent, ref NewLinkEvent args)
    {
        if (!TryComp<MedicalScannerComponent>(args.Sink, out var scanner))
            return;

        scanner.ConnectedConsole = ent;
        var connEv = new ScannerConnectedEvent(args.Sink);
        RaiseLocalEvent(ent, ref connEv);

        // If a mob is already in the scanner when linked, notify the console
        if (scanner.BodyContainer.ContainedEntity is {} mob)
        {
            var insertEv = new ScannerInsertedEvent(args.Sink, mob);
            RaiseLocalEvent(ent, ref insertEv);
        }
    }

    private void OnPortDisconnected(Entity<GeneticsScannerComponent> ent, ref PortDisconnectedEvent args)
    {
        // Clear ConnectedConsole on any linked medical scanners
        if (!TryComp<DeviceLinkSourceComponent>(ent, out var source))
            return;

        // The port is already disconnected, so we clear the connection
        // Check remaining links — if no medical scanner is linked, clear
        var hasScanner = false;
        foreach (var port in source.Outputs.Values.SelectMany(ports => ports))
        {
            if (HasComp<MedicalScannerComponent>(port))
            {
                hasScanner = true;
                break;
            }
        }

        if (!hasScanner)
        {
            var ev = new ScannerDisconnectedEvent(default);
            RaiseLocalEvent(ent, ref ev);
        }
    }
}
