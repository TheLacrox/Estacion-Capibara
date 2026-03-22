// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Radiation.Systems;
using Content.Shared.Mobs;
using Content.Shared._Trauma.Genetics.Abilities;

namespace Content.Server._Trauma.Genetics.Abilities;

// Server-only because RadiationSystem is server-side
public sealed class RadiationMutationSystem : EntitySystem
{
    [Dependency] private readonly RadiationSystem _radiation = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RadiationMutationComponent, MobStateChangedEvent>(OnStateChanged);
    }

    private void OnStateChanged(Entity<RadiationMutationComponent> ent, ref MobStateChangedEvent args)
    {
        _radiation.SetSourceEnabled(ent.Owner, args.NewMobState != MobState.Dead);
    }
}
