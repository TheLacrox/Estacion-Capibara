// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Client._Capibara.Botany;

/// <summary>
/// Client system that sets the produce mob's sprite to match the source produce prototype.
/// Reads the EntityPrototype of the produce to find its sprite RSI and applies it.
/// </summary>
public sealed class ProduceMobVisualsSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ProduceMobComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<ProduceMobComponent, AfterAutoHandleStateEvent>(OnStateChanged);
    }

    private void OnStartup(EntityUid uid, ProduceMobComponent comp, ComponentStartup args)
    {
        UpdateSprite(uid, comp);
    }

    private void OnStateChanged(EntityUid uid, ProduceMobComponent comp, ref AfterAutoHandleStateEvent args)
    {
        UpdateSprite(uid, comp);
    }

    private void UpdateSprite(EntityUid uid, ProduceMobComponent comp)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        if (comp.ProducePrototypeId == null)
            return;

        if (!_protoManager.TryIndex<EntityPrototype>(comp.ProducePrototypeId, out var produceProto))
            return;

        if (!produceProto.Components.TryGetValue("Sprite", out var spriteReg))
            return;

        if (spriteReg.Component is not SpriteComponent protoSprite)
            return;

        // Copy the RSI and set the "produce" state which most produce entities use
        if (protoSprite.BaseRSI != null)
        {
            sprite.BaseRSI = protoSprite.BaseRSI;
            sprite.LayerSetRSI(0, protoSprite.BaseRSI);
            sprite.LayerSetState(0, "produce");
        }
    }
}
