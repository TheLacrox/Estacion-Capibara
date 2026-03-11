// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.ShadowMonarch;

[NetSerializable, Serializable]
public enum ShadowMonarchStatsUiKey : byte
{
    Key,
}

[NetSerializable, Serializable]
public enum ShadowMonarchStat : byte
{
    Strength,
    Agility,
    Resistance,
    Intellect,
}

[NetSerializable, Serializable]
public sealed class ShadowMonarchStatsState : BoundUserInterfaceState
{
    public int StatPoints;
    public int KillCount;

    public int Strength;
    public int Agility;
    public int Resistance;
    public int Intellect;

    public int MaxStatLevel;

    // Computed bonuses
    public float BonusDamage;
    public float BonusSpeed;
    public float BonusHp;
    public float BonusStamina;
    public float BonusMana;
    public int BonusArmy;

    // Mana
    public float ShadowMana;
    public float MaxShadowMana;
}

[NetSerializable, Serializable]
public sealed class ShadowMonarchAllocateStatMessage : BoundUserInterfaceMessage
{
    public ShadowMonarchStat Stat;

    public ShadowMonarchAllocateStatMessage(ShadowMonarchStat stat)
    {
        Stat = stat;
    }
}
