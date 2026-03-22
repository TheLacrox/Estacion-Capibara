// SPDX-License-Identifier: AGPL-3.0-or-later
// Stub component ported from Trauma-Station for YAML deserialization.

using Robust.Shared.GameStates;

namespace Content.Shared._Trauma.Trigger;

/// <summary>
/// Triggers when the entity exits a floating or thrown state and lands on a surface.
/// Stub: no runtime behavior yet.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class TriggerOnLandComponent : Component;
