// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Mind;
using Content.Shared.Mind.Components;
using Content.Shared._Trauma.Roles;

namespace Content.Shared._Trauma.Mind;

/// <summary>
/// Handles raising some events on roles when mind changes.
/// </summary>
public sealed class MindRoleEventSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MindComponent, MindGotAddedEvent>(OnAdded);
        SubscribeLocalEvent<MindComponent, MindGotRemovedEvent>(OnBeforeRemoved);
    }

    private void OnAdded(Entity<MindComponent> ent, ref MindGotAddedEvent args)
    {
        if (ent.Comp.OwnedEntity is not {} mob)
            return;

        // tell roles that their mind got added to a mob
        var ev = new RoleMindAddedEvent(ent, mob);
        foreach (var role in ent.Comp.MindRoles)
        {
            RaiseLocalEvent(role, ref ev);
        }
    }

    private void OnBeforeRemoved(Entity<MindComponent> ent, ref MindGotRemovedEvent args)
    {
        // use container entity from the event since mind may have already been detached
        var mob = args.Container.Owner;
        if (mob == default)
            return;

        // tell roles that their mind got removed from a mob
        var ev = new RoleMindRemovedEvent(ent, mob);
        foreach (var role in ent.Comp.MindRoles)
        {
            RaiseLocalEvent(role, ref ev);
        }
    }
}
