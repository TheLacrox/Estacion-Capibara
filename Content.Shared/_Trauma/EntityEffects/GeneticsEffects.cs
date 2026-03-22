// SPDX-License-Identifier: AGPL-3.0-or-later

// EntityEffect classes for genetics mutation prototypes.
// Ported from Trauma-Station.

using Content.Shared.EntityEffects;
using Content.Shared.Eye.Blinding.Systems;
using Content.Shared.Humanoid;
using Content.Shared.Trigger.Systems;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// Deletes the target entity.
/// </summary>
public sealed partial class Delete : EntityEffect
{
    public override void Effect(EntityEffectBaseArgs args)
    {
        args.EntityManager.QueueDeleteEntity(args.TargetEntity);
    }

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Modifies eye damage by a given amount, floored to an integer.
/// Ported from Trauma-Station EyeDamageEntityEffectSystem.
/// </summary>
public sealed partial class EyeDamage : EntityEffect
{
    /// <summary>
    /// The amount of eye damage to add (negative heals).
    /// </summary>
    [DataField]
    public int Amount = -1;

    public override void Effect(EntityEffectBaseArgs args)
    {
        var blindable = args.EntityManager.System<BlindableSystem>();
        var scale = 1f;
        if (args is EntityEffectReagentArgs reagentArgs)
            scale = reagentArgs.Scale.Float();

        var amount = (int) Math.Floor(Amount * scale);
        blindable.AdjustEyeDamage((args.TargetEntity, null), amount);
    }

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Scrambles the target entity's DNA/appearance.
/// Ported from Trauma-Station ScrambleDnaEntityEffectSystem.
/// </summary>
public sealed partial class ScrambleDna : EntityEffect
{
    public override void Effect(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.HasComponent<HumanoidAppearanceComponent>(args.TargetEntity))
            return;

        var scramble = args.EntityManager.System<DnaScrambleOnTriggerSystem>();
        scramble.Scramble(args.TargetEntity);
    }

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}
