// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Flash;
using Content.Shared.Random.Helpers;
using Content.Shared.Trigger.Systems;
using Robust.Shared.Timing;

namespace Content.Shared._Trauma.Trigger.Triggers;

public sealed class TriggerOnFlashedSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly TriggerSystem _trigger = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TriggerOnFlashedComponent, AfterFlashedEvent>(OnFlashed);
    }

    private void OnFlashed(Entity<TriggerOnFlashedComponent> ent, ref AfterFlashedEvent args)
    {
        if (SharedRandomExtensions.PredictedProb(_timing, ent.Comp.Prob, GetNetEntity(ent)))
            _trigger.Trigger(ent, args.User, ent.Comp.KeyOut);
    }
}
