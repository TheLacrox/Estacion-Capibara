// SPDX-License-Identifier: AGPL-3.0-or-later
// Ported from Trauma-Station

using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// An entity effect that applies the effects of an <see cref="EntityEffectPrototype"/>.
/// Used by the genetics mutation system to compose effects via prototypes.
/// </summary>
public sealed partial class NestedEffect : EntityEffect
{
    /// <summary>
    /// The entityEffect prototype whose effects to apply.
    /// </summary>
    [DataField("proto", required: true)]
    public ProtoId<EntityEffectPrototype> Proto;

    public override void Effect(EntityEffectBaseArgs args)
    {
        var effects = args.EntityManager.System<SharedEntityEffectsSystem>();
        effects.TryApplyEffect(args.TargetEntity, Proto);
    }

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
    {
        return null;
    }
}
