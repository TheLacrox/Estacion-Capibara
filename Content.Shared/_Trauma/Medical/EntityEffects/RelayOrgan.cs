// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Components;
using Content.Shared.Body.Systems;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.Medical.EntityEffects;

/// <summary>
/// Relays entity effects to a specific organ by its slot category (e.g. "Eyes", "Brain").
/// Target must be a body.
/// </summary>
public sealed partial class RelayOrgan : EntityEffectBase<RelayOrgan>
{
    /// <summary>
    /// The organ slot category to target (e.g. "eyes", "brain").
    /// Matched against <c>OrganComponent.SlotId</c>.
    /// </summary>
    [DataField(required: true)]
    public string Category = string.Empty;

    /// <summary>
    /// Text to use for the guidebook entry.
    /// </summary>
    [DataField]
    public LocId? GuidebookText;

    [DataField(required: true)]
    public EntityEffect[] Effects = default!;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => GuidebookText is {} key ? Loc.GetString(key, ("chance", Probability)) : null;
}

public sealed class RelayOrganEffectSystem : EntityEffectSystem<BodyComponent, RelayOrgan>
{
    [Dependency] private readonly SharedBodySystem _body = default!;
    [Dependency] private readonly SharedEntityEffectsSystem _effects = default!;

    protected override void Effect(Entity<BodyComponent> ent, ref EntityEffectEvent<RelayOrgan> args)
    {
        var effect = args.Effect;
        var category = effect.Category.ToLowerInvariant();

        foreach (var (organUid, organComp) in _body.GetBodyOrgans(ent, ent.Comp))
        {
            if (organComp.SlotId.Equals(category, System.StringComparison.OrdinalIgnoreCase))
            {
                _effects.ApplyEffects(organUid, effect.Effects, args.Scale, args.User);
            }
        }
    }
}
