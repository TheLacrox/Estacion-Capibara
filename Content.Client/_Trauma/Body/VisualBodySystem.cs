// <Trauma>
using System.Linq;
using Content.Shared.Body;
using Content.Shared.Body.Organ;
using Content.Shared.CCVar;
using Content.Shared.Humanoid.Markings;
using Content.Shared.Humanoid;
using Content.Shared._Shitmed.Body.Organ;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Configuration;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client.Body;

public sealed class VisualBodySystem : SharedVisualBodySystem
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly MarkingManager _marking = default!;
    [Dependency] private readonly SpriteSystem _sprite = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<VisualOrganComponent, OrganEnabledEvent>(OnOrganEnabled);
        SubscribeLocalEvent<VisualOrganComponent, OrganDisabledEvent>(OnOrganDisabled);
        SubscribeLocalEvent<VisualOrganComponent, AfterAutoHandleStateEvent>(OnOrganState);

        SubscribeLocalEvent<VisualOrganMarkingsComponent, OrganEnabledEvent>(OnMarkingsEnabled);
        SubscribeLocalEvent<VisualOrganMarkingsComponent, OrganDisabledEvent>(OnMarkingsDisabled);
        SubscribeLocalEvent<VisualOrganMarkingsComponent, AfterAutoHandleStateEvent>(OnMarkingsState);

        SubscribeLocalEvent<VisualOrganMarkingsComponent, BodyRelayedEvent<HumanoidLayerVisibilityChangedEvent>>(OnMarkingsChangedVisibility);
    }

    private void OnOrganEnabled(Entity<VisualOrganComponent> ent, ref OrganEnabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            ApplyVisual(ent, body);
    }

    private void OnOrganDisabled(Entity<VisualOrganComponent> ent, ref OrganDisabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            RemoveVisual(ent, body);
    }

    private void OnOrganState(Entity<VisualOrganComponent> ent, ref AfterAutoHandleStateEvent args)
    {
        if (Comp<OrganComponent>(ent).Body is not { } body)
            return;

        ApplyVisual(ent, body);
    }

    private void ApplyVisual(Entity<VisualOrganComponent> ent, EntityUid target)
    {
        if (!_sprite.LayerMapTryGet(target, ent.Comp.Layer, out var index, false))
            return;

        _sprite.LayerSetData(target, index, ent.Comp.Data);
    }

    private void RemoveVisual(Entity<VisualOrganComponent> ent, EntityUid target)
    {
        if (!_sprite.LayerMapTryGet(target, ent.Comp.Layer, out var index, false))
            return;

        _sprite.LayerSetRsiState(target, index, RSI.StateId.Invalid);
    }

    private void OnMarkingsEnabled(Entity<VisualOrganMarkingsComponent> ent, ref OrganEnabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            ApplyMarkings(ent, body);
    }

    private void OnMarkingsDisabled(Entity<VisualOrganMarkingsComponent> ent, ref OrganDisabledEvent args)
    {
        if (args.Organ.Comp.Body is {} body)
            RemoveMarkings(ent, body);
    }

    private void OnMarkingsState(Entity<VisualOrganMarkingsComponent> ent, ref AfterAutoHandleStateEvent args)
    {
        if (Comp<OrganComponent>(ent).Body is not { } body)
            return;

        RemoveMarkings(ent, body);
        ApplyMarkings(ent, body);
    }

    protected override void SetOrganColor(Entity<VisualOrganComponent> ent, Color color)
    {
        base.SetOrganColor(ent, color);

        if (Comp<OrganComponent>(ent).Body is not { } body)
            return;

        ApplyVisual(ent, body);
    }

    protected override void SetOrganMarkings(Entity<VisualOrganMarkingsComponent> ent, Dictionary<HumanoidVisualLayers, List<Marking>> markings)
    {
        base.SetOrganMarkings(ent, markings);

        if (Comp<OrganComponent>(ent).Body is not { } body)
            return;

        RemoveMarkings(ent, body);
        ApplyMarkings(ent, body);
    }

    protected override void SetOrganAppearance(Entity<VisualOrganComponent> ent, PrototypeLayerData data)
    {
        base.SetOrganAppearance(ent, data);

        if (Comp<OrganComponent>(ent).Body is not { } body)
            return;

        ApplyVisual(ent, body);
    }

    private void ApplyMarkings(Entity<VisualOrganMarkingsComponent> ent, EntityUid target)
    {
        var applied = new List<Marking>();
        foreach (var markings in ent.Comp.Markings.Values)
        {
            foreach (var marking in markings)
            {
                if (!_marking.TryGetMarking(marking, out var proto))
                    continue;

                if (!_sprite.LayerMapTryGet(target, proto.BodyPart, out var index, true))
                    continue;

                for (var i = 0; i < proto.Sprites.Count; i++)
                {
                    var sprite = proto.Sprites[i];

                    DebugTools.Assert(sprite is SpriteSpecifier.Rsi);
                    if (sprite is not SpriteSpecifier.Rsi rsi)
                        continue;

                    var layerId = $"{proto.ID}-{rsi.RsiState}";

                    if (!_sprite.LayerMapTryGet(target, layerId, out _, false))
                    {
                        var layer = _sprite.AddLayer(target, sprite, index + i + 1);
                        _sprite.LayerMapSet(target, layerId, layer);
                        _sprite.LayerSetSprite(target, layerId, rsi);
                    }

                    if (marking.MarkingColors is not null && i < marking.MarkingColors.Count)
                        _sprite.LayerSetColor(target, layerId, marking.MarkingColors[i]);
                    else
                        _sprite.LayerSetColor(target, layerId, Color.White);
                }

                applied.Add(marking);
            }
        }
        ent.Comp.AppliedMarkings = applied;
    }

    private void RemoveMarkings(Entity<VisualOrganMarkingsComponent> ent, EntityUid target)
    {
        foreach (var marking in ent.Comp.AppliedMarkings)
        {
            if (!_marking.TryGetMarking(marking, out var proto))
                continue;

            foreach (var sprite in proto.Sprites)
            {
                DebugTools.Assert(sprite is SpriteSpecifier.Rsi);
                if (sprite is not SpriteSpecifier.Rsi rsi)
                    continue;

                var layerId = $"{proto.ID}-{rsi.RsiState}";

                if (!_sprite.LayerMapTryGet(target, layerId, out var index, false))
                    continue;

                _sprite.LayerMapRemove(target, layerId);
                _sprite.RemoveLayer(target, index);
            }
        }
    }

    private void OnMarkingsChangedVisibility(Entity<VisualOrganMarkingsComponent> ent, ref BodyRelayedEvent<HumanoidLayerVisibilityChangedEvent> args)
    {
        // TODO: Implement layer visibility changes when HumanoidLayerVisibilityChangedEvent is ported
    }
}

/// <summary>
/// Placeholder for Trauma's layer visibility event.
/// </summary>
[ByRefEvent]
public readonly record struct HumanoidLayerVisibilityChangedEvent(Enum Layer, bool Visible);
