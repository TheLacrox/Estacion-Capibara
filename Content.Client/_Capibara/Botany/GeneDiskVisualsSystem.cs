// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Client._Capibara.Botany;

/// <summary>
/// Client system that tints gene disk sprites based on the stored gene's rarity.
/// </summary>
public sealed class GeneDiskVisualsSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneDiskComponent, AfterAutoHandleStateEvent>(OnStateChanged);
        SubscribeLocalEvent<GeneDiskComponent, ComponentStartup>(OnStartup);
    }

    private void OnStartup(EntityUid uid, GeneDiskComponent comp, ComponentStartup args)
    {
        UpdateColor(uid, comp);
    }

    private void OnStateChanged(EntityUid uid, GeneDiskComponent comp, ref AfterAutoHandleStateEvent args)
    {
        UpdateColor(uid, comp);
    }

    private void UpdateColor(EntityUid uid, GeneDiskComponent comp)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        if (comp.StoredGene != null && _protoManager.TryIndex(comp.StoredGene.Value, out var geneProto))
        {
            sprite.Color = GetRarityColor(geneProto.Rarity);
        }
        else
        {
            sprite.Color = Color.White;
        }
    }

    private static Color GetRarityColor(PlantGeneRarity rarity)
    {
        return rarity switch
        {
            PlantGeneRarity.Common => new Color(200 / 255f, 200 / 255f, 200 / 255f),     // light gray
            PlantGeneRarity.Uncommon => new Color(100 / 255f, 220 / 255f, 100 / 255f),   // green
            PlantGeneRarity.Rare => new Color(100 / 255f, 150 / 255f, 255 / 255f),       // blue
            PlantGeneRarity.Legendary => new Color(220 / 255f, 100 / 255f, 255 / 255f),  // purple
            _ => Color.White,
        };
    }
}
