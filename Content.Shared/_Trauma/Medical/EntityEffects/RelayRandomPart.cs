// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Components;
using Content.Shared.Body.Part;
using Content.Shared.Body.Systems;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Shared._Trauma.Medical.EntityEffects;

/// <summary>
/// Relays entity effects to a single random body part picked from allowed types.
/// </summary>
public sealed partial class RelayRandomPart : EntityEffectBase<RelayRandomPart>
{
    /// <summary>
    /// The body part types to pick from.
    /// </summary>
    [DataField(required: true)]
    public BodyPartType[] Types = default!;

    /// <summary>
    /// Optional part symmetry to require.
    /// </summary>
    [DataField]
    public BodyPartSymmetry? PartSymmetry;

    /// <summary>
    /// Effect to apply to a random part.
    /// </summary>
    [DataField(required: true)]
    public EntityEffect Effect = default!;

    /// <summary>
    /// Effect to apply to the target body if no valid bodyparts were found.
    /// </summary>
    [DataField]
    public EntityEffect? FailEffect;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("entity-effect-guidebook-relay-random-part", ("effect", Effect.GuidebookEffectDescription(prototype, entSys)!));
}

public sealed class RelayRandomPartEffectSystem : EntityEffectSystem<BodyComponent, RelayRandomPart>
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedBodySystem _body = default!;
    [Dependency] private readonly SharedEntityEffectsSystem _effects = default!;

    private List<EntityUid> _parts = new();

    protected override void Effect(Entity<BodyComponent> ent, ref EntityEffectEvent<RelayRandomPart> args)
    {
        var effect = args.Effect;
        var symmetry = effect.PartSymmetry;
        _parts.Clear();
        foreach (var partType in effect.Types)
        {
            foreach (var (partUid, _) in _body.GetBodyChildrenOfType(ent, partType, ent.Comp, symmetry))
            {
                _parts.Add(partUid);
            }
        }

        if (_parts.Count == 0) // no parts found
        {
            if (effect.FailEffect is {} fail)
            {
                var failArgs = new EntityEffectBaseArgs(ent, EntityManager);
                if (fail.ShouldApply(failArgs))
                    fail.Effect(failArgs);
            }
            return;
        }

        var picked = _random.Pick(_parts);
        var applyArgs = new EntityEffectBaseArgs(picked, EntityManager);
        if (effect.Effect.ShouldApply(applyArgs))
            effect.Effect.Effect(applyArgs);
    }
}
