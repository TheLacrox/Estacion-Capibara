// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.ShadowMonarch.Components;

[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class ShadowMonarchComponent : Component
{
    [DataField, AutoNetworkedField]
    public ShadowMonarchPhase CurrentPhase = ShadowMonarchPhase.Dormant;

    [DataField, AutoNetworkedField]
    public int ExtractionCount;

    [DataField, AutoNetworkedField]
    public int ShadowPower;

    [DataField]
    public HashSet<EntityUid> ShadowArmy = new();

    [DataField]
    public List<ShadowSoldierData> HiddenSoldiers = new();

    [DataField]
    public int MaxArmySize = 3;

    // Extraction actions (3 types)
    [DataField]
    public EntProtoId ActionExtractionSoldier = "ActionShadowExtractSoldier";

    [DataField]
    public EntityUid? ActionExtractionSoldierEntity;

    [DataField]
    public EntProtoId ActionExtractionTank = "ActionShadowExtractTank";

    [DataField]
    public EntityUid? ActionExtractionTankEntity;

    [DataField]
    public EntProtoId ActionExtractionMage = "ActionShadowExtractMage";

    [DataField]
    public EntityUid? ActionExtractionMageEntity;

    // Other abilities
    [DataField]
    public EntProtoId ActionStep = "ActionShadowStep";

    [DataField]
    public EntityUid? ActionStepEntity;

    [DataField]
    public EntProtoId ActionExchange = "ActionShadowExchange";

    [DataField]
    public EntityUid? ActionExchangeEntity;

    [DataField]
    public EntProtoId ActionHide = "ActionShadowHide";

    [DataField]
    public EntityUid? ActionHideEntity;

    [DataField]
    public EntProtoId ActionSummon = "ActionShadowSummon";

    [DataField]
    public EntityUid? ActionSummonEntity;

    [DataField]
    public EntProtoId ActionDomain = "ActionShadowDomain";

    [DataField]
    public EntityUid? ActionDomainEntity;

    [DataField]
    public EntProtoId ActionAscend = "ActionShadowAscend";

    [DataField]
    public EntityUid? ActionAscendEntity;

    [DataField]
    public EntProtoId ObjectiveAscend = "ShadowMonarchAscendObjective";

    /// <summary>
    /// Names of victims whose shadows were extracted, for round-end summary.
    /// </summary>
    [DataField]
    public List<string> VictimNames = new();

    // === Stat Points System ===

    [DataField, AutoNetworkedField]
    public int StatPoints;

    [DataField, AutoNetworkedField]
    public int Strength;

    [DataField, AutoNetworkedField]
    public int Agility;

    [DataField, AutoNetworkedField]
    public int Resistance;

    [DataField, AutoNetworkedField]
    public int Intellect;

    [DataField, AutoNetworkedField]
    public int KillCount;

    [DataField]
    public int MaxStatLevel = 10;

    // Stat scaling constants
    [DataField]
    public float StrengthDamagePerPoint = 2f;

    [DataField]
    public float AgilitySpeedPerPoint = 0.3f;

    [DataField]
    public float ResistanceHpPerPoint = 10f;

    [DataField]
    public float ResistanceStaminaPerPoint = 5f;

    [DataField]
    public float IntellectManaPerPoint = 10f;

    [DataField]
    public int IntellectArmyPerPoints = 3;

    // Kill point values
    [DataField]
    public int PointsPerSimpleMob = 1;

    [DataField]
    public int PointsPerHumanoid = 2;

    [DataField]
    public int PointsPerPlayer = 3;

    // === Mana System ===

    [DataField, AutoNetworkedField]
    public float ShadowMana;

    [DataField]
    public float MaxShadowMana = 50f;

    [DataField]
    public float ManaRegenPerSecond = 2f;

    [DataField]
    public float ManaStepCost = 15f;

    [DataField]
    public float ManaExchangeCost = 10f;

    [DataField]
    public float ManaHideCost = 5f;

    [DataField]
    public float ManaSummonCost = 20f;

    [DataField]
    public float ManaDomainCost = 30f;

    // Stats action
    [DataField]
    public EntProtoId ActionStats = "ActionShadowStats";

    [DataField]
    public EntityUid? ActionStatsEntity;

    // Recall action
    [DataField]
    public EntProtoId ActionRecall = "ActionShadowRecall";

    [DataField]
    public EntityUid? ActionRecallEntity;
}

[NetSerializable, Serializable]
public enum ShadowMonarchPhase : byte
{
    Dormant,
    Awakened,
    Ascended,
}
