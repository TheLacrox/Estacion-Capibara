// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Temperature.Components;
using Content.Shared._Trauma.Genetics.Abilities;
using Content.Shared._Trauma.Genetics.Mutations;

namespace Content.Server._Trauma.Genetics.Abilities;

public sealed class TemperatureDamageMutationSystem : EntitySystem
{
    private EntityQuery<TemperatureComponent> _query;

    public override void Initialize()
    {
        base.Initialize();

        _query = GetEntityQuery<TemperatureComponent>();

        SubscribeLocalEvent<TemperatureDamageMutationComponent, MutationAddedEvent>(OnAdded);
        SubscribeLocalEvent<TemperatureDamageMutationComponent, MutationRemovedEvent>(OnRemoved);
    }

    private void OnAdded(Entity<TemperatureDamageMutationComponent> ent, ref MutationAddedEvent args)
    {
        if (!_query.TryComp(args.Target, out var comp))
            return;

        comp.ColdDamageThreshold += ent.Comp.ColdOffset;
        comp.HeatDamageThreshold += ent.Comp.HeatOffset;
    }

    private void OnRemoved(Entity<TemperatureDamageMutationComponent> ent, ref MutationRemovedEvent args)
    {
        if (!_query.TryComp(args.Target, out var comp))
            return;

        comp.ColdDamageThreshold -= ent.Comp.ColdOffset;
        comp.HeatDamageThreshold -= ent.Comp.HeatOffset;
    }
}
