// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Body.Components;
using Content.Shared.Body.Systems;
using Content.Shared._Trauma.Genetics.Abilities;
using Content.Shared._Trauma.Genetics.Mutations;

namespace Content.Server._Trauma.Genetics.Abilities;

public sealed class MetabolismSpeedMutationSystem : EntitySystem
{
    [Dependency] private readonly SharedBodySystem _body = default!;

    private EntityQuery<MetabolizerComponent> _query;

    public override void Initialize()
    {
        base.Initialize();

        _query = GetEntityQuery<MetabolizerComponent>();

        SubscribeLocalEvent<MetabolismSpeedMutationComponent, MutationAddedEvent>(OnAdded);
        SubscribeLocalEvent<MetabolismSpeedMutationComponent, MutationRemovedEvent>(OnRemoved);
    }

    private void OnAdded(Entity<MetabolismSpeedMutationComponent> ent, ref MutationAddedEvent args)
    {
        Modify(args.Target, ent.Comp.Bonus);
    }

    private void OnRemoved(Entity<MetabolismSpeedMutationComponent> ent, ref MutationRemovedEvent args)
    {
        Modify(args.Target, -ent.Comp.Bonus);
    }

    private void Modify(EntityUid uid, float add)
    {
        // some shitcode mobs like dragon have metabolizer on the mob itself not organs, check edge case
        if (_query.TryComp(uid, out var mobComp))
        {
            mobComp.UpdateIntervalMultiplier += add;
            Dirty(uid, mobComp);
        }

        foreach (var (organUid, _) in _body.GetBodyOrgans(uid))
        {
            if (!_query.TryComp(organUid, out var metabolizer))
                continue;

            metabolizer.UpdateIntervalMultiplier += add;
            Dirty(organUid, metabolizer);
        }
    }
}
