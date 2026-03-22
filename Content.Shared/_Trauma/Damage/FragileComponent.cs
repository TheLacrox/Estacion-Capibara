// SPDX-License-Identifier: AGPL-3.0-or-later
// Stub component ported from Trauma-Station for YAML deserialization.

using Robust.Shared.GameStates;

namespace Content.Shared._Trauma.Damage;

/// <summary>
/// Modifies incoming damage from any source.
/// Stub: no runtime behavior yet.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class FragileComponent : Component
{
    [DataField]
    public float Modifier = 20000f;
}
