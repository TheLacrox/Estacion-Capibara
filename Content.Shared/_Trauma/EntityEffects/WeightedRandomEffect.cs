// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using System.Text;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// Like <c>WeightedRandomPrototype</c> but for <see cref="EntityEffect"/>
/// When ran it will activate a random effect.
/// </summary>
public sealed partial class WeightedRandomEffect : EntityEffectBase<WeightedRandomEffect>
{
    [DataField(required: true)]
    public List<WeightedEffect> Children;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
    {
        var builder = new StringBuilder("Randomly chooses 1 of the following effects:");
        var totalPercent = 100f / GetTotalWeights();
        foreach (var child in Children)
        {
            var percent = child.Weight * totalPercent;
            builder.Append("- ");
            builder.Append((int) percent);
            builder.Append("%: ");
            if (child.Effect.GuidebookEffectDescription(prototype, entSys) is not {} text)
            {
                builder.Append("???,");
                continue;
            }

            builder.Append(text);
            builder.Append(",");
        }

        return builder.ToString();
    }

    public float GetTotalWeights()
    {
        var total = 0f;
        foreach (var child in Children)
        {
            total += child.Weight;
        }
        return total;
    }
}

public sealed class WeightedRandomEffectSystem : EntityEffectSystem<MetaDataComponent, WeightedRandomEffect>
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedEntityEffectsSystem _effects = default!;

    protected override void Effect(Entity<MetaDataComponent> ent, ref EntityEffectEvent<WeightedRandomEffect> args)
    {
        var total = 0f;
        var effect = args.Effect;
        var target = _random.NextFloat() * effect.GetTotalWeights();
        foreach (var child in effect.Children)
        {
            total += child.Weight;
            if (total >= target)
            {
                _effects.TryApplyEffect(ent, child.Effect, args.Scale, args.User);
                return;
            }
        }
    }
}

[DataDefinition]
public partial record struct WeightedEffect()
{
    [DataField(required: true)]
    public EntityEffect Effect = default!;

    [DataField]
    public float Weight = 1f;
}
