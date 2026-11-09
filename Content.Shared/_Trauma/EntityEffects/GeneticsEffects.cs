// SPDX-License-Identifier: AGPL-3.0-or-later

// EntityEffect classes for genetics mutation prototypes.
// Ported from Trauma-Station.

using Content.Shared.EntityEffects;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// Deletes the target entity.
/// </summary>
public sealed partial class Delete : EntityEffectBase<Delete>;

public sealed class DeleteEntityEffectSystem : EntityEffectSystem<MetaDataComponent, Delete>
{
    protected override void Effect(Entity<MetaDataComponent> ent, ref EntityEffectEvent<Delete> args)
    {
        PredictedQueueDel(ent.Owner);
    }
}
