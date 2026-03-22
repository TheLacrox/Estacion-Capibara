// SPDX-License-Identifier: AGPL-3.0-or-later

// Adapter classes that expose Trauma EntityCondition types as upstream EntityEffectCondition types.
// The YAML genetics prototypes use these inside EntityEffect.conditions fields, which require
// EntityEffectCondition subclasses. The actual logic lives in the Trauma EntityCondition systems;
// these adapters provide the same fields so the YAML deserializes, but use the upstream dispatch.

using Content.Shared.Body;
using Content.Shared.Body.Components;
using Content.Shared.Body.Organ;
using Content.Shared.Body.Part;
using Content.Shared.Body.Systems;
using Content.Shared.EntityEffects;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared._Trauma.Genetics.Mutations;
using Content.Shared.Standing;
using Content.Shared.Timing;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects.Conditions;

/// <summary>
/// Requires the target entity is standing (not downed).
/// </summary>
public sealed partial class StandingCondition : EntityEffectCondition
{
    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.TryGetComponent<StandingStateComponent>(args.TargetEntity, out var standing))
            return false;

        return standing.Standing;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Requires that a use delay is not active.
/// </summary>
public sealed partial class UseDelayCondition : EntityEffectCondition
{
    [DataField]
    public string DelayId = UseDelaySystem.DefaultId;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.TryGetComponent<UseDelayComponent>(args.TargetEntity, out var comp))
            return true; // no delay component means no delay active

        var system = args.EntityManager.System<UseDelaySystem>();
        return !system.IsDelayed((args.TargetEntity, comp), DelayId);
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Requires the target entity is inside a container.
/// </summary>
public sealed partial class InContainerCondition : EntityEffectCondition
{
    /// <summary>
    /// If true, the condition passes when the entity is NOT in a container.
    /// </summary>
    [DataField]
    public bool Inverted;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        var containerSystem = args.EntityManager.System<SharedContainerSystem>();
        var inContainer = containerSystem.TryGetContainingContainer((args.TargetEntity, null, null), out _);
        return Inverted ? !inContainer : inContainer;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Requires the target body has a specific organ slot.
/// </summary>
public sealed partial class HasOrganSlot : EntityEffectCondition
{
    [DataField(required: true)]
    public ProtoId<OrganCategoryPrototype> Organ;

    [DataField(required: true)]
    public BodyPartType PartType;

    [DataField]
    public BodyPartSymmetry? Symmetry;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.TryGetComponent<BodyComponent>(args.TargetEntity, out var body))
            return false;

        var bodySystem = args.EntityManager.System<SharedBodySystem>();

        foreach (var (partUid, partComp) in bodySystem.GetBodyChildrenOfType(args.TargetEntity, PartType, body, Symmetry))
        {
            foreach (var (organUid, organComp) in bodySystem.GetPartOrgans(partUid, partComp))
            {
                if (organComp.Category == Organ)
                    return true;
            }
        }

        return false;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Checks the mob state of the target entity.
/// Adapter for EntityEffectCondition that wraps the MobState check.
/// </summary>
public sealed partial class MobStateCondition : EntityEffectCondition
{
    [DataField]
    public bool Inverted;

    [DataField]
    public MobState Mobstate = MobState.Alive;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.TryGetComponent<MobStateComponent>(args.TargetEntity, out var mobState))
            return false;

        var result = mobState.CurrentState == Mobstate;
        return Inverted ? !result : result;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Checks a condition on the entity that has the mutation applied,
/// not the mutation entity itself.
/// </summary>
public sealed partial class MutatedNestedCondition : EntityEffectCondition
{
    [DataField("condition")]
    public EntityEffectCondition? InnerCondition;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        // Delegate to the inner condition if present
        return InnerCondition == null || InnerCondition.Condition(args);
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Checks a named condition prototype.
/// </summary>
public sealed partial class NestedCondition : EntityEffectCondition
{
    [DataField(required: true)]
    public string Proto = string.Empty;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        // Stub: always passes. Requires EntityConditionPrototype lookup.
        return true;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}

/// <summary>
/// Checks if the entity's DNA is unstable (instability >= max).
/// Adapter for EntityEffectCondition that wraps the MutatableComponent check.
/// </summary>
public sealed partial class DnaUnstableCondition : EntityEffectCondition
{
    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (!args.EntityManager.TryGetComponent<MutatableComponent>(args.TargetEntity, out var mutatable))
            return false;

        return mutatable.TotalInstability >= mutatable.MaxInstability;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
        => string.Empty;
}
