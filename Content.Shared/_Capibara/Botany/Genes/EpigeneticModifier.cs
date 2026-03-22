// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Genes;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class EpigeneticModifier
{
    [DataField]
    public string EffectId = string.Empty;

    [DataField]
    public Dictionary<string, float> StatModifiers = new();

    [DataField]
    public int RemainingCycles;

    [DataField]
    public string Source = string.Empty;
}
