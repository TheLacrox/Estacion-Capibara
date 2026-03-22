// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Content.Shared.Mobs;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Utility;

namespace Content.Server._Capibara.Botany;

/// <summary>
/// System for converting harvested produce into sentient mobs.
/// Spawns a pre-configured mob entity, copies the produce's name, and deletes the original.
/// On death, drops the original produce item.
/// </summary>
public sealed class ProduceMobSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly MetaDataSystem _meta = default!;
    [Dependency] private readonly IPrototypeManager _protoManager = default!;

    private const string AggressiveMobProto = "CapibaraProduceMobHostile";
    private const string PassiveMobProto = "CapibaraProduceMobPassive";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ProduceMobComponent, MobStateChangedEvent>(OnMobStateChanged);
    }

    /// <summary>
    /// Converts a produce entity into a sentient mob.
    /// Spawns a mob entity at the produce's location, sets its name, and deletes the produce.
    /// </summary>
    public void MakeProduceSentient(EntityUid produceUid, bool aggressive)
    {
        var coords = Transform(produceUid).Coordinates;
        var produceName = MetaData(produceUid).EntityName;

        // Get the produce entity's prototype ID for sprite copying and death drops
        var produceProtoId = MetaData(produceUid).EntityPrototype?.ID;

        // Get sprite info from the produce entity's prototype
        ResPath? spriteRsi = null;
        string? spriteState = null;
        if (produceProtoId != null && _protoManager.TryIndex<EntityPrototype>(produceProtoId, out var produceProto))
        {
            // Look for Sprite component data in the prototype
            if (produceProto.Components.TryGetValue("Sprite", out var spriteReg))
            {
                var spriteComp = spriteReg.Component;
                // Try to get the sprite RSI path via reflection on serialized data
                if (spriteComp is IComponent comp)
                {
                    // We'll store the prototype ID and let the client resolve the sprite
                }
            }
        }

        // Spawn the appropriate mob prototype
        var proto = aggressive ? AggressiveMobProto : PassiveMobProto;
        var mobUid = Spawn(proto, coords);

        // Set the mob's name based on the source produce
        var prefix = aggressive
            ? Loc.GetString("capibara-produce-mob-aggressive-prefix")
            : Loc.GetString("capibara-produce-mob-passive-prefix");
        _meta.SetEntityName(mobUid, $"{prefix} {produceName}");
        _meta.SetEntityDescription(mobUid, Loc.GetString("capibara-produce-mob-description",
            ("name", produceName)));

        // Configure the ProduceMob component
        var produceMob = EnsureComp<ProduceMobComponent>(mobUid);
        produceMob.IsAggressive = aggressive;
        produceMob.SourcePlantName = produceName;
        produceMob.ProducePrototypeId = produceProtoId;
        Dirty(mobUid, produceMob);

        // Copy gene visual effects from produce to mob
        if (TryComp<GeneModifiedProduceComponent>(produceUid, out var sourceVisuals))
        {
            var mobVisuals = EnsureComp<GeneModifiedProduceComponent>(mobUid);
            mobVisuals.Tint = sourceVisuals.Tint;
            mobVisuals.EffectOverlays = new List<string>(sourceVisuals.EffectOverlays);
            Dirty(mobUid, mobVisuals);
        }

        // Delete the original produce entity
        QueueDel(produceUid);
    }

    /// <summary>
    /// When a produce mob dies, spawn the original produce item.
    /// </summary>
    private void OnMobStateChanged(EntityUid uid, ProduceMobComponent comp, MobStateChangedEvent args)
    {
        if (args.NewMobState != MobState.Dead)
            return;

        if (comp.ProducePrototypeId == null)
            return;

        // Spawn the original produce item at the mob's location
        var coords = Transform(uid).Coordinates;
        var produce = Spawn(comp.ProducePrototypeId, coords);

        // Copy gene visual effects to the dropped produce
        if (TryComp<GeneModifiedProduceComponent>(uid, out var mobVisuals))
        {
            var produceVisuals = EnsureComp<GeneModifiedProduceComponent>(produce);
            produceVisuals.Tint = mobVisuals.Tint;
            produceVisuals.EffectOverlays = new List<string>(mobVisuals.EffectOverlays);
            Dirty(produce, produceVisuals);
        }

        // Delete the mob
        QueueDel(uid);
    }
}
