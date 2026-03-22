// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Atmos.EntitySystems;
using Content.Server.Electrocution;
using Content.Server.Explosion.EntitySystems;
using Content.Server.Singularity.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared.Atmos;
using Content.Shared.Speech.Muting;
using Content.Shared.StatusEffect;
using Content.Shared.Stunnable;
using Content.Shared.Mobs.Components;
using Content.Shared.Nutrition;
using Content.Shared.Throwing;
using Content.Server.Weapons.Ranged.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Random;
using System.Numerics;

namespace Content.Server._Capibara.Botany;

/// <summary>
/// Handles runtime gene effects on produce — throw hit effects, landing effects, etc.
/// Setup-time effects (slipify, glow, sentient) are handled in PlantGenomeSystem.Produce.cs.
/// </summary>
public sealed class GeneProduceEffectsSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly ExplosionSystem _explosion = default!;
    [Dependency] private readonly ElectrocutionSystem _electrocution = default!;
    [Dependency] private readonly SharedStunSystem _stun = default!;
    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedTransformSystem _xform = default!;
    [Dependency] private readonly AtmosphereSystem _atmos = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly GravityWellSystem _gravityWell = default!;
    [Dependency] private readonly GunSystem _gun = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneModifiedProduceComponent, ThrowDoHitEvent>(OnThrowHit);
        SubscribeLocalEvent<GeneModifiedProduceComponent, LandEvent>(OnLand);
        SubscribeLocalEvent<GeneModifiedProduceComponent, FullyEatenEvent>(OnEaten);
    }

    /// <summary>
    /// When gene-modified produce hits an entity after being thrown.
    /// </summary>
    private void OnThrowHit(EntityUid uid, GeneModifiedProduceComponent comp, ThrowDoHitEvent args)
    {
        var target = args.Target;
        var shouldDestroy = false;

        foreach (var geneId in comp.ActiveGeneIds)
        {
            switch (geneId)
            {
                case "GeneExplosive":
                    // Explode on impact — destroy the produce
                    _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId,
                        4f, 2f, 3f);
                    shouldDestroy = true;
                    break;

                case "GeneElectrical":
                    // Shock the target
                    _electrocution.TryDoElectrocution(target, uid, 20,
                        TimeSpan.FromSeconds(3), true, ignoreInsulation: true);
                    break;

                case "GeneBluespace":
                    // Teleport the target to a random close location (rare — max 5 tiles)
                    if (HasComp<MobStateComponent>(target))
                        TeleportEntity(target, 5f);
                    break;

                case "GeneBluespaceAnomaly":
                    // Teleport the target further (legendary — max 25 tiles)
                    if (HasComp<MobStateComponent>(target))
                        TeleportEntity(target, 25f);
                    break;

                case "GeneMimetic":
                    // Silence the target for 2 minutes
                    _statusEffects.TryAddStatusEffect<MutedComponent>(target, "Muted",
                        TimeSpan.FromMinutes(2), true);
                    break;

                case "GeneHonking":
                    // Honk sound + knockdown
                    _audio.PlayPvs("/Audio/Items/bikehorn.ogg", uid,
                        AudioParams.Default.WithVolume(4f));
                    _stun.TryKnockdown(target, TimeSpan.FromSeconds(3), true);
                    break;

                case "GeneToxic":
                    // Release ammonia at impact point (toxic gas) — destroy produce
                    ReleaseGasAtEntity(uid, Gas.Ammonia, 30f);
                    shouldDestroy = true;
                    break;

                case "GeneCrystalline":
                    // Shatter handled in OnLand to avoid double-firing
                    shouldDestroy = true;
                    break;

                case "GeneMetallic":
                    // Heavy metal impact — knockdown
                    _stun.TryKnockdown(target, TimeSpan.FromSeconds(1.5f), true);
                    break;

                case "GeneTemporal":
                    // Temporal impact — longer knockdown
                    _stun.TryKnockdown(target, TimeSpan.FromSeconds(5), true);
                    break;

                case "GenePlasmaCore":
                    // Release plasma at impact point — destroy produce
                    ReleaseGasAtEntity(uid, Gas.Plasma, 40f);
                    shouldDestroy = true;
                    break;

                case "GeneSingularitySeed":
                    // Create micro gravity pull at impact point (like an anomaly)
                    _gravityWell.GravPulse(uid, 6f, 0f, baseRadialDeltaV: 3f);
                    shouldDestroy = true;
                    break;

                case "GeneVocalCords":
                    // Produce screams on hit (plays sound)
                    _audio.PlayPvs("/Audio/Voice/Human/malescream_1.ogg", uid,
                        AudioParams.Default.WithVolume(6f));
                    break;

                case "GeneSticky":
                    // Sticky produce — stay attached (no special handler yet, just knockdown)
                    _stun.TryKnockdown(target, TimeSpan.FromSeconds(1), true);
                    break;
            }
        }

        // Destroy produce that should be consumed on impact
        if (shouldDestroy)
        {
            QueueDel(uid);
        }
    }

    /// <summary>
    /// When gene-modified produce lands on the ground after being thrown.
    /// Triggers area effects at the landing point (wall hit, ground hit, etc.).
    /// </summary>
    private void OnLand(EntityUid uid, GeneModifiedProduceComponent comp, ref LandEvent args)
    {
        var shouldDestroy = false;

        foreach (var geneId in comp.ActiveGeneIds)
        {
            switch (geneId)
            {
                case "GeneExplosive":
                    _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId,
                        4f, 2f, 3f);
                    shouldDestroy = true;
                    break;

                case "GeneCrystalline":
                    // Shatter on landing — stinger grenade effect with glass sound
                    SpawnStingerEffect(uid);
                    shouldDestroy = true;
                    break;

                case "GeneToxic":
                    ReleaseGasAtEntity(uid, Gas.Ammonia, 30f);
                    shouldDestroy = true;
                    break;

                case "GeneGaseous":
                    ReleaseGasAtEntity(uid, Gas.Ammonia, 15f);
                    shouldDestroy = true;
                    break;

                case "GenePlasmaCore":
                    ReleaseGasAtEntity(uid, Gas.Plasma, 40f);
                    shouldDestroy = true;
                    break;

                case "GeneSingularitySeed":
                    // Gravity pulse at landing point
                    _gravityWell.GravPulse(uid, 6f, 0f, baseRadialDeltaV: 3f);
                    shouldDestroy = true;
                    break;

                case "GeneHonking":
                    _audio.PlayPvs("/Audio/Items/bikehorn.ogg", uid,
                        AudioParams.Default.WithVolume(4f));
                    break;

                case "GeneVocalCords":
                    _audio.PlayPvs("/Audio/Voice/Human/malescream_1.ogg", uid,
                        AudioParams.Default.WithVolume(6f));
                    break;
            }
        }

        if (shouldDestroy)
        {
            QueueDel(uid);
        }
    }

    /// <summary>
    /// When gene-modified produce is fully eaten.
    /// Effects are applied to the eater.
    /// </summary>
    private void OnEaten(EntityUid uid, GeneModifiedProduceComponent comp, ref FullyEatenEvent args)
    {
        var eater = args.User;

        foreach (var geneId in comp.ActiveGeneIds)
        {
            switch (geneId)
            {
                case "GeneBluespace":
                    // Teleport the eater close (rare — max 5 tiles)
                    TeleportEntity(eater, 5f);
                    break;

                case "GeneBluespaceAnomaly":
                    // Teleport the eater far (legendary — max 25 tiles)
                    TeleportEntity(eater, 25f);
                    break;

                case "GeneMimetic":
                    // Silence the eater for 2 minutes
                    _statusEffects.TryAddStatusEffect<MutedComponent>(eater, "Muted",
                        TimeSpan.FromMinutes(2), true);
                    break;

                case "GeneTemporal":
                    // Slow the eater
                    _stun.TryKnockdown(eater, TimeSpan.FromSeconds(5), true);
                    break;

                case "GeneElectrical":
                    // Shock the eater
                    _electrocution.TryDoElectrocution(eater, uid, 15,
                        TimeSpan.FromSeconds(2), true, ignoreInsulation: true);
                    break;

                case "GeneVocalCords":
                    // Eater screams
                    _audio.PlayPvs("/Audio/Voice/Human/malescream_1.ogg", eater,
                        AudioParams.Default.WithVolume(4f));
                    break;
            }
        }
    }

    /// <summary>
    /// Teleports an entity to a random nearby location.
    /// </summary>
    private void TeleportEntity(EntityUid target, float range)
    {
        var xform = Transform(target);
        var worldPos = _xform.GetWorldPosition(xform);

        var offset = new Vector2(
            _random.NextFloat(-range, range),
            _random.NextFloat(-range, range));

        _xform.SetWorldPosition(xform, worldPos + offset);
    }

    /// <summary>
    /// Teleports mobs/NPCs/players within range to random nearby positions.
    /// Only affects entities with MobStateComponent (living things).
    /// </summary>
    private void TeleportNearbyMobs(EntityUid source, float detectRange, float teleportRange)
    {
        var xform = Transform(source);
        var worldPos = _xform.GetWorldPosition(xform);

        foreach (var entity in _lookup.GetEntitiesInRange(xform.Coordinates, detectRange))
        {
            if (entity == source)
                continue;

            if (!HasComp<MobStateComponent>(entity))
                continue;

            var offset = new Vector2(
                _random.NextFloat(-teleportRange, teleportRange),
                _random.NextFloat(-teleportRange, teleportRange));

            _xform.SetWorldPosition(Transform(entity), worldPos + offset);
        }
    }

    /// <summary>
    /// Spawns rubber pellets shot outward — same effect as stinger grenade but without
    /// spawning a visible grenade entity. Pellets are fired as projectiles using GunSystem.
    /// </summary>
    private void SpawnStingerEffect(EntityUid source)
    {
        var mapCoords = _xform.GetMapCoordinates(source);
        _audio.PlayPvs("/Audio/Effects/glass_break2.ogg", source);

        var count = 4;
        var segmentAngle = 360f / count;

        for (var i = 0; i < count; i++)
        {
            var pellet = Spawn("PelletClusterRubber", mapCoords);
            var angleMin = segmentAngle * i;
            var angleMax = segmentAngle * (i + 1);
            var angle = Angle.FromDegrees(_random.Next((int) angleMin, (int) angleMax));
            var direction = angle.ToVec().Normalized();
            var velocity = new Vector2(_random.NextFloat(2f, 6f), _random.NextFloat(2f, 6f));
            _gun.ShootProjectile(pellet, direction, velocity, null);
        }
    }

    /// <summary>
    /// Releases gas at an entity's location.
    /// </summary>
    private void ReleaseGasAtEntity(EntityUid uid, Gas gas, float moles)
    {
        var tileMix = _atmos.GetContainingMixture(uid, false, true);
        tileMix?.AdjustMoles(gas, moles);
    }

}
