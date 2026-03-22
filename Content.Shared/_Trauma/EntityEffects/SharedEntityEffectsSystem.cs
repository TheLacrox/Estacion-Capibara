// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Trauma.EntityEffects;
using Content.Shared.EntityConditions;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects;

/// <summary>
/// Trauma - provides the generic effect event dispatch system.
/// This is a standalone system that coexists with upstream's EntityEffect.Effect() dispatch.
/// </summary>
public sealed partial class SharedEntityEffectsSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly SharedEntityConditionsSystem _condition = default!;

    /// <summary>
    /// Raises an effect to an entity. You should not be calling this unless you know what you're doing.
    /// </summary>
    public void RaiseEffectEvent<T>(EntityUid target, T effect, float scale, EntityUid? user) where T : EntityEffectBase<T>
    {
        var effectEv = new EntityEffectEvent<T>(effect, scale, user);
        RaiseLocalEvent(target, ref effectEv);
    }

    /// <summary>
    /// Applies a list of entity effects to a target entity via the upstream Effect() dispatch.
    /// </summary>
    public void ApplyEffects(EntityUid target, EntityEffect[] effects, float scale = 1f, EntityUid? user = null)
    {
        var args = new EntityEffectBaseArgs(target, EntityManager);
        foreach (var effect in effects)
        {
            if (effect.ShouldApply(args))
                effect.Effect(args);
        }
    }

    /// <summary>
    /// Tries to apply a single entity effect, checking conditions first.
    /// </summary>
    public bool TryApplyEffect(EntityUid target, EntityEffect effect, float scale = 1f, EntityUid? user = null)
    {
        var args = new EntityEffectBaseArgs(target, EntityManager);
        if (!effect.ShouldApply(args))
            return false;

        effect.Effect(args);
        return true;
    }

    /// <summary>
    /// Applies an EntityEffectPrototype's effects to a target, checking prototype-level conditions.
    /// Used by NestedEffect.
    /// </summary>
    public void TryApplyEffect(EntityUid target, ProtoId<EntityEffectPrototype> id, float scale = 1f, EntityUid? user = null)
    {
        var proto = _proto.Index(id);
        if (_condition.TryConditions(target, proto.Conditions))
            ApplyEffects(target, proto.Effects, scale, user);
    }
}

/// <summary>
/// This is a basic abstract entity effect system for handling entity effects via events.
/// </summary>
/// <typeparam name="T">The Component that is required for the effect</typeparam>
/// <typeparam name="TEffect">The Entity Effect itself</typeparam>
public abstract partial class EntityEffectSystem<T, TEffect> : EntitySystem where T : Component where TEffect : EntityEffectBase<TEffect>
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<T, EntityEffectEvent<TEffect>>(Effect);
    }

    protected abstract void Effect(Entity<T> entity, ref EntityEffectEvent<TEffect> args);
}
