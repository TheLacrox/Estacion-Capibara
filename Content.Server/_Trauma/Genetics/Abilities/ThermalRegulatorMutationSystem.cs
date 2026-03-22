// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Body.Components;
using Content.Shared._Trauma.Genetics.Abilities;
using Content.Shared._Trauma.Genetics.Mutations;

namespace Content.Shared._Trauma.Genetics.Abilities;

public sealed class ThermalRegulatorMutationSystem : EntitySystem
{
    private EntityQuery<ThermalRegulatorComponent> _query;

    public override void Initialize()
    {
        base.Initialize();

        _query = GetEntityQuery<ThermalRegulatorComponent>();

        SubscribeLocalEvent<ThermalRegulatorMutationComponent, MutationAddedEvent>(OnAdded);
        SubscribeLocalEvent<ThermalRegulatorMutationComponent, MutationRemovedEvent>(OnRemoved);
    }

    private void OnAdded(Entity<ThermalRegulatorMutationComponent> ent, ref MutationAddedEvent args)
    {
        if (!_query.TryComp(args.Target, out var comp))
            return;

        comp.ShiveringHeatRegulation *= ent.Comp.Shivering;
        comp.SweatHeatRegulation *= ent.Comp.Sweating;
        comp.MetabolismHeat *= ent.Comp.Metabolism;
        comp.ImplicitHeatRegulation *= ent.Comp.Regulation;
    }

    private void OnRemoved(Entity<ThermalRegulatorMutationComponent> ent, ref MutationRemovedEvent args)
    {
        if (!_query.TryComp(args.Target, out var comp))
            return;

        comp.ShiveringHeatRegulation /= ent.Comp.Shivering;
        comp.SweatHeatRegulation /= ent.Comp.Sweating;
        comp.MetabolismHeat /= ent.Comp.Metabolism;
        comp.ImplicitHeatRegulation /= ent.Comp.Regulation;
    }
}
