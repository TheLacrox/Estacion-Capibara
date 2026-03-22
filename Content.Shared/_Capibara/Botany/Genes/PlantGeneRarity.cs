// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Genes;

[Serializable, NetSerializable]
public enum PlantGeneRarity : byte
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Legendary = 3,
}
