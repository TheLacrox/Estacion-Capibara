// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Atmos.EntitySystems;
using Content.Server.Botany.Components;
using Content.Shared.Atmos;
using Content.Server.Electrocution;
using Content.Server.Emp;
using Content.Server.Explosion.EntitySystems;
using Content.Server.Popups;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared.Popups;
using Robust.Shared.Random;
using Robust.Server.GameObjects;
using Content.Server.Weapons.Ranged.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using System.Numerics;

namespace Content.Server._Capibara.Botany;

public sealed partial class PlantGenomeSystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly ExplosionSystem _explosion = default!;
    [Dependency] private readonly EmpSystem _emp = default!;
    [Dependency] private readonly ElectrocutionSystem _electrocution = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly TransformSystem _xform = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly GunSystem _gun = default!;

    /// <summary>
    /// Process instability effects during a growth cycle.
    /// Called from OnGrowthCycle when genome is initialized.
    /// </summary>
    public void ProcessInstability(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        if (holder.Seed == null || holder.Dead)
            return;

        var instability = genome.Instability;

        // Stable zone (0-25): no effects
        if (instability <= 25f)
            return;

        // Unstable zone (26-50): 5% stat drift
        if (instability <= 50f)
        {
            if (_random.Prob(0.05f))
                ApplyStatDrift(uid, genome, 0.05f);
            return;
        }

        // Volatile zone (51-75): 15% stat drift, 5% gene corruption, -2 health
        if (instability <= 75f)
        {
            if (_random.Prob(0.15f))
                ApplyStatDrift(uid, genome, 0.1f);
            if (_random.Prob(0.05f))
                CorruptGene(uid, genome);
            holder.Health -= 2f;
            return;
        }

        // Critical zone (76-100): 25% stat drift, 15% gene corruption, 5% meltdown, -5 health
        if (instability <= 100f)
        {
            if (_random.Prob(0.25f))
                ApplyStatDrift(uid, genome, 0.15f);
            if (_random.Prob(0.15f))
                CorruptGene(uid, genome);
            if (_random.Prob(0.05f))
                TriggerMeltdown(uid, genome, holder);
            holder.Health -= 5f;
            return;
        }

        // Meltdown zone (100+): guaranteed death in 1-3 cycles
        holder.Health -= 30f;
        if (holder.Health <= 0)
            TriggerMeltdown(uid, genome, holder);
    }

    /// <summary>
    /// Randomly drifts one stat by a percentage of its base value.
    /// Modifies the BaseStatSnapshot so drift persists across RecalculateStats cycles.
    /// </summary>
    private void ApplyStatDrift(EntityUid uid, PlantGenomeComponent genome, float maxDrift)
    {
        var drift = _random.NextFloat(-maxDrift, maxDrift);
        var statNames = new[] { "Potency", "Yield", "Lifespan", "Production", "Endurance" };
        var stat = _random.Pick(statNames);

        if (!genome.BaseStatSnapshot.TryGetValue(stat, out var baseVal))
            return;

        if (stat == "Yield")
            genome.BaseStatSnapshot[stat] = Math.Max(0, baseVal + (int)(baseVal * drift));
        else
            genome.BaseStatSnapshot[stat] = baseVal * (1f + drift);
    }

    /// <summary>
    /// Replaces a random gene with nothing (removes it).
    /// </summary>
    private void CorruptGene(EntityUid uid, PlantGenomeComponent genome)
    {
        // Find filled gene slots
        var filledSlots = new List<int>();
        for (var i = 0; i < genome.GeneSlots.Count; i++)
        {
            if (genome.GeneSlots[i].Gene != null && !genome.GeneSlots[i].Locked)
                filledSlots.Add(i);
        }

        if (filledSlots.Count == 0)
            return;

        var targetIdx = _random.Pick(filledSlots);
        genome.GeneSlots[targetIdx] = new PlantGeneSlot { Gene = null, Locked = false };

        _popup.PopupEntity(Loc.GetString("capibara-instability-gene-corrupted"), uid, PopupType.MediumCaution);

        RecalculateInstability(uid, genome);
    }

    /// <summary>
    /// Triggers a meltdown event themed to the plant's highest-rarity gene.
    /// Each failure mode produces a different dramatic effect.
    /// </summary>
    private void TriggerMeltdown(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        var failureMode = GetDominantFailureMode(genome);

        _popup.PopupEntity(Loc.GetString("capibara-instability-meltdown",
            ("type", failureMode ?? "generic")), uid, PopupType.LargeCaution);

        // Execute the themed meltdown effect
        ExecuteMeltdownEffect(uid, failureMode);

        // Kill the plant
        holder.Dead = true;
        holder.Health = 0;
    }

    /// <summary>
    /// Executes the actual meltdown effect based on failure mode.
    /// </summary>
    private void ExecuteMeltdownEffect(EntityUid uid, string? failureMode)
    {
        var coords = Transform(uid).Coordinates;
        var mapCoords = _xform.GetMapCoordinates(uid);

        switch (failureMode)
        {
            case "explosive":
                // Big boom — moderate explosion
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 8f, 2f, 4f);
                break;

            case "bluespace":
                // Teleport nearby entities randomly within 10 tiles
                TeleportNearbyEntities(uid, 3f, 10f);
                break;

            case "electrical":
                // EMP pulse + shock nearby entities
                _emp.EmpPulse(mapCoords, 4f, 50000f, 5f);
                ShockNearbyEntities(uid, 3f, 30);
                break;

            case "toxic":
                // Spawn toxic gas at the location
                SpawnGasCloud(uid, "Ammonia", 15f);
                break;

            case "flash":
                // Bright flash — blind nearby entities
                // Use a small explosion with no tile damage as a concussive flash
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 2f, 1f, 1f, tileBreakScale: 0f, maxTileBreak: 0);
                _emp.EmpPulse(mapCoords, 2f, 10000f, 2f);
                break;

            case "thorny":
                // Small explosion (thorns flying everywhere)
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 4f, 2f, 2f);
                break;

            case "slip":
                // Spawn slippery puddle — small explosion as the banana-ocalypse
                SpawnGasCloud(uid, "WaterVapor", 8f);
                break;

            case "sticky":
                // Spawn foam-like spread
                SpawnGasCloud(uid, "NitrousOxide", 8f);
                break;

            case "healing":
                // Ironic — heals everyone nearby, but the plant still dies
                // Harmless meltdown, just a bright flash
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 1f, 1f, 0.5f, tileBreakScale: 0f, maxTileBreak: 0);
                break;

            case "gas":
                // Gas cloud release
                SpawnGasCloud(uid, "Ammonia", 20f);
                break;

            case "crystal":
                // Shattering — shoot rubber pellets outward with glass sound
                SpawnCrystalPellets(uid);
                break;

            case "metallic":
                // EMP blast (magnetic metal disruption)
                _emp.EmpPulse(mapCoords, 5f, 80000f, 8f);
                break;

            case "sentient":
                // The plant screams and spawns aggressive produce mobs
                SpawnMeltdownProduceMobs(uid);
                break;

            case "mimetic":
                // Invisible explosion — small but surprising
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 5f, 2f, 3f);
                break;

            case "honking":
                // Honk explosion — stun + small blast
                _audio.PlayPvs("/Audio/Items/bikehorn.ogg", uid, AudioParams.Default.WithVolume(8f));
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 3f, 1f, 2f, tileBreakScale: 0f, maxTileBreak: 0);
                break;

            default:
                // Generic meltdown — small explosion
                _explosion.QueueExplosion(uid, ExplosionSystem.DefaultExplosionPrototypeId, 4f, 2f, 2f);
                break;
        }
    }

    /// <summary>
    /// Teleports entities within range to random nearby positions.
    /// </summary>
    private void TeleportNearbyEntities(EntityUid source, float range, float teleportRange)
    {
        var xform = Transform(source);
        var worldPos = _xform.GetWorldPosition(xform);

        foreach (var entity in _lookup.GetEntitiesInRange(xform.Coordinates, range))
        {
            if (entity == source)
                continue;

            var targetXform = Transform(entity);
            var offset = new Vector2(
                _random.NextFloat(-teleportRange, teleportRange),
                _random.NextFloat(-teleportRange, teleportRange));

            _xform.SetWorldPosition(targetXform, worldPos + offset);
        }
    }

    /// <summary>
    /// Shocks all entities with StatusEffects within range.
    /// </summary>
    private void ShockNearbyEntities(EntityUid source, float range, int damage)
    {
        var xform = Transform(source);

        foreach (var entity in _lookup.GetEntitiesInRange(xform.Coordinates, range))
        {
            if (entity == source)
                continue;

            _electrocution.TryDoElectrocution(entity, source, damage,
                TimeSpan.FromSeconds(2), true, ignoreInsulation: true);
        }
    }

    /// <summary>
    /// Spawns a gas cloud by releasing atmospheric gas at the plant's location.
    /// Uses the atmos system to add gas moles to the tile.
    /// </summary>
    private void SpawnGasCloud(EntityUid source, string gasName, float moles)
    {
        var tileMix = _atmos.GetContainingMixture(source, false, true);
        if (tileMix != null)
        {
            // Map gas name strings to the Gas enum
            var gas = gasName switch
            {
                "Ammonia" => Gas.Ammonia,
                "WaterVapor" => Gas.WaterVapor,
                "Plasma" => Gas.Plasma,
                "NitrousOxide" => Gas.NitrousOxide,
                _ => Gas.Ammonia,
            };
            tileMix.AdjustMoles(gas, moles);
        }

        // Small visual poof
        _explosion.QueueExplosion(source, ExplosionSystem.DefaultExplosionPrototypeId,
            1f, 1f, 0.5f, tileBreakScale: 0f, maxTileBreak: 0);
    }

    /// <summary>
    /// Spawns angry produce mobs during a sentient meltdown.
    /// Uses the plant's actual produce prototypes so they have the correct sprite.
    /// </summary>
    private void SpawnMeltdownProduceMobs(EntityUid source)
    {
        if (!TryComp<PlantHolderComponent>(source, out var holder) || holder.Seed == null)
            return;

        var coords = Transform(source).Coordinates;
        var count = _random.Next(2, 5); // Spawn 2-4 angry produce mobs

        for (var i = 0; i < count; i++)
        {
            if (holder.Seed.ProductPrototypes.Count == 0)
                break;

            var productProto = _random.Pick(holder.Seed.ProductPrototypes);
            var product = Spawn(productProto, coords);
            _produceMob.MakeProduceSentient(product, aggressive: true);
        }
    }

    /// <summary>
    /// Spawns rubber pellets shot outward — crystal meltdown effect.
    /// </summary>
    private void SpawnCrystalPellets(EntityUid source)
    {
        var mapCoords = _xform.GetMapCoordinates(source);
        _audio.PlayPvs("/Audio/Effects/glass_break2.ogg", source);

        var count = 10;
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
    /// Gets the failure mode of the highest-rarity gene in the genome.
    /// </summary>
    private string? GetDominantFailureMode(PlantGenomeComponent genome)
    {
        string? bestMode = null;
        var bestRarity = PlantGeneRarity.Common;

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null || !_protoManager.TryIndex(slot.Gene.Value, out var geneProto))
                continue;

            if (geneProto.FailureMode != null && geneProto.Rarity >= bestRarity)
            {
                bestRarity = geneProto.Rarity;
                bestMode = geneProto.FailureMode;
            }
        }

        return bestMode;
    }
}
