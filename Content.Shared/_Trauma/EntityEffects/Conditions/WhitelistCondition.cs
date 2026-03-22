// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.EntityEffects;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects.Conditions;

/// <summary>
/// An <see cref="EntityEffectCondition"/> that checks the target entity
/// against a whitelist and/or blacklist using <see cref="EntityWhitelist"/>.
/// </summary>
/// <remarks>
/// This is separate from the Trauma <c>EntityConditionBase&lt;WhitelistCondition&gt;</c>
/// in the entity condition system. Both are named WhitelistCondition, but the
/// serializer resolves them independently since they derive from different base types.
/// </remarks>
public sealed partial class WhitelistCondition : EntityEffectCondition
{
    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField]
    public EntityWhitelist? Blacklist;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        var whitelistSystem = args.EntityManager.System<EntityWhitelistSystem>();
        return whitelistSystem.CheckBoth(args.TargetEntity, blacklist: Blacklist, whitelist: Whitelist);
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
    {
        return string.Empty;
    }
}
