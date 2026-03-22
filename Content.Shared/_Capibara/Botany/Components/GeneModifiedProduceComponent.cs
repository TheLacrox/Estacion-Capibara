// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// Added to produce entities that came from gene-modified plants.
/// Stores the visual tint, effect overlay data, and active gene info
/// so the client can render them and server systems can apply behaviors.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(raiseAfterAutoHandleState: true)]
public sealed partial class GeneModifiedProduceComponent : Component
{
    /// <summary>
    /// Blended tint color from the parent plant's genes.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Color Tint = Color.White;

    /// <summary>
    /// Effect overlay strings in "rsiPath|state|colorHex" format, inherited from parent.
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<string> EffectOverlays = new();

    /// <summary>
    /// IDs of active genes from the parent plant. Used by downstream systems
    /// to apply gene-specific behaviors to produce.
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<string> ActiveGeneIds = new();

    /// <summary>
    /// Whether this produce has an electrical shock effect.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool HasElectricalEffect;
}
