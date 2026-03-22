// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany;

/// <summary>
/// Appearance data keys for genome visual effects on plants.
/// </summary>
[Serializable, NetSerializable]
public enum PlantGenomeVisuals : byte
{
    /// <summary>
    /// Color tint applied to the plant sprite, blended from all active gene tints.
    /// Value type: Color
    /// </summary>
    GeneTint,

    /// <summary>
    /// List of effect overlays as strings in "rsiPath|state|colorHex" format.
    /// Value type: List&lt;string&gt;
    /// </summary>
    EffectOverlays,
}
