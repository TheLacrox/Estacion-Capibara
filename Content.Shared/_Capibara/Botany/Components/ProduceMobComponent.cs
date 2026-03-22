// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// Tags an entity as a gene-modified produce mob.
/// The produce mob is a sentient produce item that wanders around.
/// It dynamically copies its sprite and name from the source produce.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(raiseAfterAutoHandleState: true)]
public sealed partial class ProduceMobComponent : Component
{
    /// <summary>
    /// Whether this produce mob is aggressive or peaceful.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool IsAggressive;

    /// <summary>
    /// Name of the source plant species.
    /// </summary>
    [DataField, AutoNetworkedField]
    public string SourcePlantName = string.Empty;

    /// <summary>
    /// The entity prototype ID of the produce (e.g. "FoodTomato").
    /// Used by the client to copy the sprite and by the server to spawn on death.
    /// </summary>
    [DataField, AutoNetworkedField]
    public EntProtoId? ProducePrototypeId;

    /// <summary>
    /// The RSI path for the produce sprite (e.g. "Objects/Specific/Hydroponics/tomato.rsi").
    /// Set server-side from the produce entity's prototype.
    /// </summary>
    [DataField, AutoNetworkedField]
    public ResPath? ProduceSpriteRsi;

    /// <summary>
    /// The RSI state name for the produce sprite (e.g. "produce").
    /// </summary>
    [DataField, AutoNetworkedField]
    public string? ProduceSpriteState;
}
