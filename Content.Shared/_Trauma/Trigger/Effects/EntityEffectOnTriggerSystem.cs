// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.EntityEffects;
using Content.Shared._Trauma.Trigger.Components;
using Content.Shared.Trigger;
using Content.Shared.Trigger.Systems;

namespace Content.Shared._Trauma.Trigger.Effects;

public sealed class EntityEffectOnTriggerSystem : XOnTriggerSystem<EntityEffectOnTriggerComponent>
{
    [Dependency] private readonly SharedEntityEffectsSystem _effects = default!;

    protected override void OnTrigger(Entity<EntityEffectOnTriggerComponent> ent, EntityUid target, ref TriggerEvent args)
    {
        _effects.ApplyEffects(target, ent.Comp.Effects, ent.Comp.Scale);
        args.Handled = true;
    }
}
