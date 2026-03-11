// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Ghost.Roles.Components;
using Content.Server.NPC;
using Content.Server.NPC.HTN;
using Content.Server.NPC.Systems;
using Content.Shared._Capibara.ShadowMonarch;
using Content.Shared._Capibara.ShadowMonarch.Components;
using Content.Shared._Capibara.ShadowMonarch.Events;
using Content.Shared.CombatMode;
using Content.Shared.Damage;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Systems;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;
using Content.Shared.NPC.Prototypes;
using Content.Shared.Popups;
using Content.Shared.Weapons.Melee;
using Robust.Shared.Prototypes;

namespace Content.Server._Capibara.ShadowMonarch;

public sealed partial class ShadowMonarchSystem
{
    [Dependency] private readonly NPCSystem _npc = default!;
    [Dependency] private readonly SharedCombatModeSystem _combat = default!;
    [Dependency] private readonly MetaDataSystem _metaData = default!;
    [Dependency] private readonly MovementSpeedModifierSystem _movementSpeed = default!;
    [Dependency] private readonly MobThresholdSystem _mobThreshold = default!;

    private static readonly ProtoId<NpcFactionPrototype> NanoTrasenFaction = "NanoTrasen";

    private void InitializeArmy()
    {
        SubscribeLocalEvent<ShadowSoldierComponent, MobStateChangedEvent>(OnSoldierStateChanged);
        SubscribeLocalEvent<ShadowMonarchComponent, ShadowRecallEvent>(OnShadowRecall);
    }

    private void UpdateArmy(float frameTime)
    {
        var query = EntityQueryEnumerator<ShadowMonarchComponent>();
        while (query.MoveNext(out var uid, out var monarch))
        {
            monarch.ShadowArmy.RemoveWhere(s => !Exists(s) || TerminatingOrDeleted(s));
        }
    }

    /// <summary>
    /// Spawns a new shadow soldier entity from a corpse. The corpse is left untouched.
    /// </summary>
    public EntityUid SpawnShadowFromCorpse(EntityUid monarch, ShadowMonarchComponent monarchComp, EntityUid corpse, string originalName, ShadowSoldierType type)
    {
        var coords = Transform(corpse).Coordinates;

        var protoId = type switch
        {
            ShadowSoldierType.Tank => "MobShadowTank",
            ShadowSoldierType.Mage => "MobShadowMage",
            _ => "MobShadowSoldier",
        };

        var soldier = Spawn(protoId, coords);

        var soldierComp = EnsureComp<ShadowSoldierComponent>(soldier);
        soldierComp.Monarch = monarch;
        soldierComp.OriginalName = originalName;
        soldierComp.SoldierType = type;

        var nameKey = type switch
        {
            ShadowSoldierType.Tank => "shadow-monarch-tank-name",
            ShadowSoldierType.Mage => "shadow-monarch-mage-name",
            _ => "shadow-monarch-soldier-name",
        };
        _metaData.SetEntityName(soldier, Loc.GetString(nameKey, ("name", originalName)));

        _faction.ClearFactions(soldier, dirty: false);
        _faction.AddFaction(soldier, _shadowMonarchFaction);

        monarchComp.ShadowArmy.Add(soldier);

        // Ghost role so dead players can take over
        SetupGhostRole(soldier, type);

        return soldier;
    }

    /// <summary>
    /// Spawns a shadow soldier from hidden data at the monarch's position.
    /// </summary>
    public EntityUid? SpawnHiddenSoldier(EntityUid monarch, ShadowMonarchComponent monarchComp, ShadowSoldierData data)
    {
        var coords = Transform(monarch).Coordinates;

        var protoId = data.SoldierType switch
        {
            ShadowSoldierType.Tank => "MobShadowTank",
            ShadowSoldierType.Mage => "MobShadowMage",
            _ => "MobShadowSoldier",
        };

        var soldier = Spawn(protoId, coords);

        var soldierComp = EnsureComp<ShadowSoldierComponent>(soldier);
        soldierComp.Monarch = monarch;
        soldierComp.OriginalName = data.OriginalName;
        soldierComp.CombatStrength = data.CombatStrength;
        soldierComp.SoldierType = data.SoldierType;

        var nameKey = data.SoldierType switch
        {
            ShadowSoldierType.Tank => "shadow-monarch-tank-name",
            ShadowSoldierType.Mage => "shadow-monarch-mage-name",
            _ => "shadow-monarch-soldier-name",
        };
        _metaData.SetEntityName(soldier, Loc.GetString(nameKey, ("name", data.OriginalName)));

        _faction.ClearFactions(soldier, dirty: false);
        _faction.AddFaction(soldier, _shadowMonarchFaction);

        monarchComp.ShadowArmy.Add(soldier);

        // Ghost role for spawned soldiers too
        SetupGhostRole(soldier, data.SoldierType);

        return soldier;
    }

    private void SetupGhostRole(EntityUid uid, ShadowSoldierType type)
    {
        var ghostRole = EnsureComp<GhostRoleComponent>(uid);
        ghostRole.RoleName = type switch
        {
            ShadowSoldierType.Tank => Loc.GetString("shadow-soldier-ghost-role-tank-name"),
            ShadowSoldierType.Mage => Loc.GetString("shadow-soldier-ghost-role-mage-name"),
            _ => Loc.GetString("shadow-soldier-ghost-role-name"),
        };
        ghostRole.RoleDescription = Loc.GetString("shadow-soldier-ghost-role-description");
        ghostRole.RoleRules = Loc.GetString("shadow-soldier-ghost-role-rules");
        ghostRole.MindRoles = new List<EntProtoId> { "MindRoleShadowSoldier" };

        EnsureComp<GhostTakeoverAvailableComponent>(uid);
    }

    private void OnSoldierStateChanged(EntityUid uid, ShadowSoldierComponent component, MobStateChangedEvent args)
    {
        if (args.NewMobState != MobState.Dead)
            return;

        // Remove from monarch's army
        if (TryComp<ShadowMonarchComponent>(component.Monarch, out var monarch))
        {
            monarch.ShadowArmy.Remove(uid);
        }
    }

    /// <summary>
    /// Recall — teleport all living shadow soldiers to the monarch's position.
    /// </summary>
    private void OnShadowRecall(EntityUid uid, ShadowMonarchComponent component, ShadowRecallEvent args)
    {
        if (args.Handled)
            return;

        if (component.ShadowArmy.Count == 0)
        {
            _popup.PopupEntity(Loc.GetString("shadow-monarch-recall-none"), uid, uid, PopupType.SmallCaution);
            return;
        }

        var coords = Transform(uid).Coordinates;
        var recalled = 0;

        foreach (var soldier in component.ShadowArmy)
        {
            if (!Exists(soldier) || TerminatingOrDeleted(soldier))
                continue;

            Transform(soldier).Coordinates = coords;
            recalled++;
        }

        if (recalled > 0)
        {
            _popup.PopupEntity(
                Loc.GetString("shadow-monarch-recall-success", ("count", recalled)),
                uid, uid, PopupType.Medium);
        }

        args.Handled = true;
    }

    public void DespawnAllSoldiers(EntityUid monarch, ShadowMonarchComponent component)
    {
        foreach (var soldier in component.ShadowArmy)
        {
            if (Exists(soldier) && !TerminatingOrDeleted(soldier))
                QueueDel(soldier);
        }
        component.ShadowArmy.Clear();
    }
}
