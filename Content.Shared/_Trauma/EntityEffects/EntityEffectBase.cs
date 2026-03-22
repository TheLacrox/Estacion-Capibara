// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects;

/// <summary>
/// Used to store an <see cref="EntityEffect"/> so it can be raised without losing the type of the condition.
/// Trauma's generic effect base - coexists with upstream EntityEffect by providing implementations
/// of the required abstract methods and dispatching via RaiseEvent instead.
/// </summary>
/// <typeparam name="T">The Effect we are raising.</typeparam>
public abstract partial class EntityEffectBase<T> : EntityEffect where T : EntityEffectBase<T>
{
    /// <summary>
    /// Dispatches this effect via the EntityEffectEvent system instead of the upstream Effect() path.
    /// </summary>
    public override void Effect(EntityEffectBaseArgs args)
    {
        if (this is not T type)
            return;

        var raiser = args.EntityManager.System<SharedEntityEffectsSystem>();
        raiser.RaiseEffectEvent(args.TargetEntity, type, 1f, null);
    }

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
    {
        return EntityEffectGuidebookText(prototype, entSys);
    }

    /// <summary>
    /// Override this in derived classes to provide guidebook text.
    /// </summary>
    public virtual string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
    {
        return null;
    }
}
