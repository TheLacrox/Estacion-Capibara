// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.ShadowMonarch;
using Content.Shared._Capibara.ShadowMonarch.Components;
using Content.Shared._Capibara.ShadowMonarch.Events;
using Content.Shared.Damage;
using Content.Shared.Humanoid;
using Content.Shared.Mobs;
using Content.Shared.Movement.Components;
using Content.Shared.Popups;
using Content.Shared.Weapons.Melee;
using Content.Goobstation.Maths.FixedPoint;
using Robust.Server.GameObjects;
using Robust.Shared.Player;

namespace Content.Server._Capibara.ShadowMonarch;

public sealed partial class ShadowMonarchSystem
{
    [Dependency] private readonly UserInterfaceSystem _ui = default!;

    // Base stats for a human — used as baseline for stat bonuses
    private const float BaseSprintSpeed = 4.5f;
    private const float BaseWalkSpeed = 2.5f;
    private const float BaseHpThreshold = 100f;
    private const float BaseStaminaCrit = 100f;

    private void InitializeStats()
    {
        SubscribeLocalEvent<ShadowMonarchComponent, ShadowMonarchStatsEvent>(OnOpenStats);
        SubscribeLocalEvent<MobStateChangedEvent>(OnAnyMobStateChanged);
        SubscribeLocalEvent<ShadowMonarchComponent, ShadowMonarchAllocateStatMessage>(OnAllocateStat);
    }

    private void OnOpenStats(EntityUid uid, ShadowMonarchComponent component, ShadowMonarchStatsEvent args)
    {
        if (args.Handled)
            return;

        UpdateStatsUi(uid, component);
        _ui.TryToggleUi(uid, ShadowMonarchStatsUiKey.Key, uid);
        args.Handled = true;
    }

    private void OnAnyMobStateChanged(MobStateChangedEvent args)
    {
        // Only care about deaths
        if (args.NewMobState != MobState.Dead)
            return;

        var victim = args.Target;

        // Don't count shadow soldiers as victims
        if (HasComp<ShadowSoldierComponent>(victim))
            return;

        // Check if the killer (Origin) is a monarch or a soldier
        if (args.Origin == null)
            return;

        var killer = args.Origin.Value;
        EntityUid monarchUid;

        if (TryComp<ShadowMonarchComponent>(killer, out var monarchComp))
        {
            monarchUid = killer;
        }
        else if (TryComp<ShadowSoldierComponent>(killer, out var soldierComp))
        {
            monarchUid = soldierComp.Monarch;
            if (!TryComp(monarchUid, out monarchComp))
                return;
        }
        else
        {
            return;
        }

        // Determine points based on victim type
        int points;
        if (HasComp<ActorComponent>(victim))
            points = monarchComp.PointsPerPlayer;
        else if (HasComp<HumanoidAppearanceComponent>(victim))
            points = monarchComp.PointsPerHumanoid;
        else
            points = monarchComp.PointsPerSimpleMob;

        monarchComp.StatPoints += points;
        monarchComp.KillCount++;

        _popup.PopupEntity(
            Loc.GetString("shadow-monarch-kill-points", ("points", points)),
            monarchUid, monarchUid, PopupType.Medium);

        Dirty(monarchUid, monarchComp);
        UpdateStatsUi(monarchUid, monarchComp);
    }

    private void OnAllocateStat(EntityUid uid, ShadowMonarchComponent component, ShadowMonarchAllocateStatMessage args)
    {
        if (component.StatPoints <= 0)
            return;

        switch (args.Stat)
        {
            case ShadowMonarchStat.Strength:
                if (component.Strength >= component.MaxStatLevel)
                    return;
                component.Strength++;
                break;
            case ShadowMonarchStat.Agility:
                if (component.Agility >= component.MaxStatLevel)
                    return;
                component.Agility++;
                break;
            case ShadowMonarchStat.Resistance:
                if (component.Resistance >= component.MaxStatLevel)
                    return;
                component.Resistance++;
                break;
            case ShadowMonarchStat.Intellect:
                if (component.Intellect >= component.MaxStatLevel)
                    return;
                component.Intellect++;
                break;
            default:
                return;
        }

        component.StatPoints--;
        ApplyStatEffects(uid, component);
        Dirty(uid, component);
        UpdateStatsUi(uid, component);
    }

    private void ApplyStatEffects(EntityUid uid, ShadowMonarchComponent component)
    {
        // Strength: +2 Slash damage per point on bare-hand melee
        if (TryComp<MeleeWeaponComponent>(uid, out var melee))
        {
            var slashDamage = 8 + component.Strength * component.StrengthDamagePerPoint;
            melee.Damage = new DamageSpecifier
            {
                DamageDict = new Dictionary<string, FixedPoint2>
                {
                    ["Slash"] = FixedPoint2.New(slashDamage),
                }
            };
            Dirty(uid, melee);
        }

        // Agility: +0.3 sprint speed per point
        var sprintSpeed = BaseSprintSpeed + component.Agility * component.AgilitySpeedPerPoint;
        _movementSpeed.ChangeBaseSpeed(uid, BaseWalkSpeed, sprintSpeed, MovementSpeedModifierComponent.DefaultAcceleration);

        // Resistance: +10 HP per point, +5 stamina crit per point
        var hpThreshold = BaseHpThreshold + component.Resistance * component.ResistanceHpPerPoint;
        _mobThreshold.SetMobStateThreshold(uid, FixedPoint2.New(0), MobState.Alive);
        _mobThreshold.SetMobStateThreshold(uid, FixedPoint2.New(hpThreshold), MobState.Critical);

        if (TryComp<Content.Shared.Damage.Components.StaminaComponent>(uid, out var stamina))
        {
            stamina.CritThreshold = BaseStaminaCrit + component.Resistance * component.ResistanceStaminaPerPoint;
            Dirty(uid, stamina);
        }

        // Intellect: +10 max mana per point
        component.MaxShadowMana = 50f + component.Intellect * component.IntellectManaPerPoint;

        // Intellect: army size bonus
        component.MaxArmySize = ComputeMaxArmySize(component.ExtractionCount, component.Intellect);
    }

    private void UpdateStatsUi(EntityUid uid, ShadowMonarchComponent component)
    {
        var state = new ShadowMonarchStatsState
        {
            StatPoints = component.StatPoints,
            KillCount = component.KillCount,
            Strength = component.Strength,
            Agility = component.Agility,
            Resistance = component.Resistance,
            Intellect = component.Intellect,
            MaxStatLevel = component.MaxStatLevel,
            BonusDamage = component.Strength * component.StrengthDamagePerPoint,
            BonusSpeed = component.Agility * component.AgilitySpeedPerPoint,
            BonusHp = component.Resistance * component.ResistanceHpPerPoint,
            BonusStamina = component.Resistance * component.ResistanceStaminaPerPoint,
            BonusMana = component.Intellect * component.IntellectManaPerPoint,
            BonusArmy = component.Intellect / component.IntellectArmyPerPoints,
            ShadowMana = component.ShadowMana,
            MaxShadowMana = component.MaxShadowMana,
        };

        _ui.SetUiState(uid, ShadowMonarchStatsUiKey.Key, state);
    }

    private void UpdateMana(float frameTime)
    {
        var query = EntityQueryEnumerator<ShadowMonarchComponent>();
        while (query.MoveNext(out var uid, out var monarch))
        {
            if (monarch.ShadowMana >= monarch.MaxShadowMana)
                continue;

            monarch.ShadowMana = Math.Min(monarch.ShadowMana + monarch.ManaRegenPerSecond * frameTime, monarch.MaxShadowMana);
            // UI update is throttled — only update every second or so via the main loop
        }
    }
}
