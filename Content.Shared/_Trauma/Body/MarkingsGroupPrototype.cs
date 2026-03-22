// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Humanoid;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Body;

/// <summary>
/// Defines a group of markings with appearance settings per layer.
/// </summary>
[Prototype]
public sealed partial class MarkingsGroupPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = string.Empty;

    /// <summary>
    /// Per-layer appearance settings (e.g. whether to match skin color).
    /// </summary>
    [DataField]
    public Dictionary<Enum, MarkingsAppearance> Appearances = new();
}

/// <summary>
/// Appearance data for a marking layer.
/// </summary>
[DataDefinition]
[Serializable, NetSerializable]
public partial record struct MarkingsAppearance
{
    /// <summary>
    /// If true, markings on this layer should match the skin color.
    /// </summary>
    [DataField]
    public bool MatchSkin;

    /// <summary>
    /// Alpha value to apply to skin-matched markings on this layer.
    /// </summary>
    [DataField]
    public float LayerAlpha = 1f;

    public MarkingsAppearance()
    {
    }
}
