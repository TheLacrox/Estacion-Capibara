// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Client.Botany;
using Content.Shared._Capibara.Botany;
using Content.Shared._Capibara.Botany.Components;
using Robust.Client.GameObjects;
using Robust.Shared.Utility;

namespace Content.Client._Capibara.Botany;

/// <summary>
/// Client system that applies gene-based color tinting and effect overlays to growing plants.
/// Reads appearance data and modifies the plant sprite accordingly.
/// </summary>
public sealed class PlantGenomeTintSystem : EntitySystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
    [Dependency] private readonly SpriteSystem _sprite = default!;

    private const string EffectLayerPrefix = "gene_effect_";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PlantGenomeComponent, AppearanceChangeEvent>(OnAppearanceChange);
        SubscribeLocalEvent<PlantGenomeComponent, ComponentShutdown>(OnComponentShutdown);
    }

    private void OnComponentShutdown(EntityUid uid, PlantGenomeComponent comp, ComponentShutdown args)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        // Reset plant layer tint to white
        if (sprite.LayerMapTryGet(PlantHolderLayers.Plant, out _))
            _sprite.LayerSetColor((uid, sprite), PlantHolderLayers.Plant, Color.White);

        // Remove all effect overlay layers
        for (var i = 0; i < 8; i++)
        {
            var key = EffectLayerPrefix + i;
            if (sprite.LayerMapTryGet(key, out var existingIdx))
            {
                sprite.RemoveLayer(existingIdx);
                sprite.LayerMapRemove(key);
            }
        }
    }

    private void OnAppearanceChange(EntityUid uid, PlantGenomeComponent comp, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        // Apply gene tint to the plant layer
        if (_appearance.TryGetData<Color>(uid, PlantGenomeVisuals.GeneTint, out var tintColor, args.Component))
        {
            _sprite.LayerSetColor((uid, args.Sprite), PlantHolderLayers.Plant, tintColor);
        }

        // Apply effect overlays (format: "rsiPath|state|colorHex")
        if (_appearance.TryGetData<List<string>>(uid, PlantGenomeVisuals.EffectOverlays, out var overlayStrings, args.Component))
        {
            UpdateEffectOverlays(uid, args.Sprite, overlayStrings);
        }
    }

    private void UpdateEffectOverlays(EntityUid uid, SpriteComponent sprite, List<string> overlayStrings)
    {
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

        // Add new effect layers from strings
        for (var i = 0; i < overlayStrings.Count; i++)
        {
            var parts = overlayStrings[i].Split('|');
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
