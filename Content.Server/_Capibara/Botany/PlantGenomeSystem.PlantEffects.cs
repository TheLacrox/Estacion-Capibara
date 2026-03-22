// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Atmos.EntitySystems;
using Content.Server.Botany.Components;
using Content.Server.Singularity.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared.Mobs.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared.Atmos;
using Robust.Shared.Random;
using System.Numerics;

namespace Content.Server._Capibara.Botany;

/// <summary>
/// Handles plant-level gene effects that occur each growth cycle while the plant is alive.
/// Effects like shocking nearby entities, exuding gas, teleporting, gravity wells, etc.
/// </summary>
public sealed partial class PlantGenomeSystem
{
    [Dependency] private readonly AtmosphereSystem _atmos = default!;
    [Dependency] private readonly GravityWellSystem _gravityWell = default!;

    /// <summary>
    /// Process plant-level gene effects during each growth cycle.
    /// Called from OnGrowthCycle after gene discovery and before instability.
    /// </summary>
    public void ProcessPlantEffects(EntityUid uid, PlantGenomeComponent genome, PlantHolderComponent holder)
    {
        if (holder.Seed == null || holder.Dead)
            return;

        foreach (var slot in genome.GeneSlots)
        {
            if (slot.Gene == null)
                continue;

            switch (slot.Gene.Value.Id)
            {
                case "GeneElectrical":
                    ProcessElectricalPlantEffect(uid);
                    break;

                case "GeneKudzu":
                    // Kudzu slowly kills the plant — drains 20 HP per cycle
                    holder.Health -= 20f;
                    break;

                case "GeneOvermutated":
                    // Overmutated — unstable genome slowly kills the plant
                    holder.Health -= 10f;
                    break;

                case "GeneGaseous":
                    ProcessGaseousPlantEffect(uid, holder);
                    break;

                case "GenePlasmaCore":
                    ProcessPlasmaCoreEffect(uid);
                    break;

                case "GeneBluespaceAnomaly":
                    ProcessBluespaceAnomalyPlantEffect(uid);
                    break;

                case "GeneSingularitySeed":
                    ProcessSingularityPlantEffect(uid);
                    break;
            }
        }
    }

    /// <summary>
    /// GeneElectrical: 15% chance each cycle to shock a random nearby entity.
    /// </summary>
    private void ProcessElectricalPlantEffect(EntityUid uid)
    {
        if (!_random.Prob(0.15f))
            return;

        ShockNearbyEntities(uid, 2f, 10);
    }

    /// <summary>
    /// GeneGaseous: Exudes the plant's ExudeGasses each growth cycle.
    /// </summary>
    private void ProcessGaseousPlantEffect(EntityUid uid, PlantHolderComponent holder)
    {
        if (holder.Seed == null || holder.Seed.ExudeGasses.Count == 0)
            return;

        var tileMix = _atmos.GetContainingMixture(uid, false, true);
        if (tileMix == null)
            return;

        foreach (var (gas, amount) in holder.Seed.ExudeGasses)
        {
            tileMix.AdjustMoles(gas, amount);
        }
    }

    /// <summary>
    /// GenePlasmaCore: Exudes plasma gas each growth cycle.
    /// </summary>
    private void ProcessPlasmaCoreEffect(EntityUid uid)
    {
        var tileMix = _atmos.GetContainingMixture(uid, false, true);
        if (tileMix == null)
            return;

        tileMix.AdjustMoles(Gas.Plasma, 5f);
    }

    /// <summary>
    /// GeneBluespaceAnomaly: ~10% chance each cycle to teleport a random nearby entity
    /// (not the planter itself) to a random nearby location.
    /// </summary>
    private void ProcessBluespaceAnomalyPlantEffect(EntityUid uid)
    {
        if (!_random.Prob(0.10f))
            return;

        var xform = Transform(uid);
        var worldPos = _xform.GetWorldPosition(xform);

        // Find mobs in range, excluding plant holders
        var candidates = new List<EntityUid>();
        foreach (var entity in _lookup.GetEntitiesInRange(xform.Coordinates, 4f))
        {
            if (entity == uid)
                continue;

            // Only teleport mobs/NPCs/players
            if (!HasComp<MobStateComponent>(entity))
                continue;

            candidates.Add(entity);
        }

        if (candidates.Count == 0)
            return;

        var target = _random.Pick(candidates);
        var offset = new Vector2(
            _random.NextFloat(-25f, 25f),
            _random.NextFloat(-25f, 25f));

        _xform.SetWorldPosition(Transform(target), worldPos + offset);
    }

    /// <summary>
    /// GeneSingularitySeed: ~8% chance each cycle to create a micro gravity pulse.
    /// Pulls all entities within range toward the plant like an anomaly.
    /// </summary>
    private void ProcessSingularityPlantEffect(EntityUid uid)
    {
        if (!_random.Prob(0.08f))
            return;

        // Use the proper gravity well pulse — same system as anomalies/singularities
        _gravityWell.GravPulse(uid, 5f, 0f, baseRadialDeltaV: 2f);
    }
}
