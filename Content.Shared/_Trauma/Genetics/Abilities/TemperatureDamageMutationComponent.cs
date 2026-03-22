// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;

namespace Content.Shared._Trauma.Genetics.Abilities;

/// <summary>
/// Adds an offset to cold and/or heat damage thresholds.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class TemperatureDamageMutationComponent : Component
{
    [DataField]
    public float ColdOffset;

    [DataField]
    public float HeatOffset;
}
