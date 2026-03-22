// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Shitmed.Body.Organ;
using Content.Shared.Interaction;

namespace Content.Shared._Trauma.Interaction;

public sealed class ExtraReachSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ExtraReachComponent, OrganEnabledEvent>(OnPartEnabled);
        SubscribeLocalEvent<ExtraReachComponent, OrganDisabledEvent>(OnPartDisabled);
        // run before TK so it can use the extra reach for its check
        SubscribeLocalEvent<ExtraReachComponent, InRangeOverrideEvent>(OnRangeOverride,
            before: new[] { typeof(TelekinesisSystem) });
    }

    private void OnPartEnabled(Entity<ExtraReachComponent> ent, ref OrganEnabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            ModifyReach(body, ent.Comp.Bonus);
    }

    private void OnPartDisabled(Entity<ExtraReachComponent> ent, ref OrganDisabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            ModifyReach(body, -ent.Comp.Bonus);
    }

    private void OnRangeOverride(Entity<ExtraReachComponent> ent, ref InRangeOverrideEvent args)
    {
        args.Range += ent.Comp.Bonus;
    }

    public void ModifyReach(EntityUid uid, float reach)
    {
        // don't care if the body is being deleted
        if (TerminatingOrDeleted(uid))
            return;

        var comp = EnsureComp<ExtraReachComponent>(uid);
        comp.Bonus += reach;
        Dirty(uid, comp);

        // remove the component if it goes to 0f
        if (Math.Abs(comp.Bonus) < 0.001f)
            RemComp(uid, comp);
    }
}
