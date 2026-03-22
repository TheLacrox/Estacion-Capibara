// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Robust.Client.GameObjects;
using Robust.Shared.Utility;

namespace Content.Client._Capibara.Botany;

/// <summary>
/// Client system that applies gene visual effects (tint + overlays) to produce entities.
/// Produce from gene-modified plants gets the same color tinting and effect overlays
/// as its parent plant.
/// </summary>
public sealed class GeneModifiedProduceVisualsSystem : EntitySystem
{
    private const string EffectLayerPrefix = "produce_gene_effect_";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneModifiedProduceComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<GeneModifiedProduceComponent, AfterAutoHandleStateEvent>(OnStateChanged);
    }

    private void OnStartup(EntityUid uid, GeneModifiedProduceComponent comp, ComponentStartup args)
    {
        UpdateVisuals(uid, comp);
    }

    private void OnStateChanged(EntityUid uid, GeneModifiedProduceComponent comp, ref AfterAutoHandleStateEvent args)
    {
        UpdateVisuals(uid, comp);
    }

    private void UpdateVisuals(EntityUid uid, GeneModifiedProduceComponent comp)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        // Apply tint to the base sprite
        if (comp.Tint != Color.White)
        {
            sprite.Color = comp.Tint;
        }

        // Remove old effect layers
        for (var i = 0; i < 8; i++)
        {
            var key = EffectLayerPrefix + i;
            if (sprite.LayerMapTryGet(key, out var existingIdx))
            {
                sprite.RemoveLayer(existingIdx);
                sprite.LayerMapRemove(key);
            }
        }

        // Add effect overlay layers
        for (var i = 0; i < comp.EffectOverlays.Count; i++)
        {
            var parts = comp.EffectOverlays[i].Split('|');
            if (parts.Length < 3)
                continue;

            var rsiPath = new ResPath(parts[0]);
            var state = parts[1];
            var color = Color.FromHex(parts[2]);

            var layerIdx = sprite.AddLayer(new SpriteSpecifier.Rsi(rsiPath, state));
            sprite.LayerMapSet(EffectLayerPrefix + i, layerIdx);
            sprite.LayerSetShader(layerIdx, "unshaded");
            sprite.LayerSetColor(layerIdx, color);
        }
    }
}
