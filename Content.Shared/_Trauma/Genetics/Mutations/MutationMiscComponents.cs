// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using System.Numerics;

namespace Content.Shared._Trauma.Genetics.Mutations;

/// <summary>
/// Replaces characters in speech with Pig Latin equivalents.
/// </summary>
[RegisterComponent]
public sealed partial class PigLatinAccentComponent : Component;

/// <summary>
/// Overrides the speech font of a speaking mob.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class SpeechFontOverrideComponent : Component
{
    /// <summary>
    /// This must be a valid <c>FontPrototype</c> but it only exists in client so it cannot be validated.
    /// </summary>
    [DataField(required: true)]
    public string Font = string.Empty;

    /// <summary>
    /// When true, does nothing if the speech source is not this component's entity.
    /// </summary>
    [DataField]
    public bool SourceOnly = true;
}

/// <summary>
/// Replaces individual characters with random strings, ignoring case etc.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CharactersAccentComponent : Component
{
    [DataField]
    public Dictionary<char, List<string>> Chars = new();
}

/// <summary>
/// Randomizes the color and energy of this entity's point light on mapinit.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class RandomPointLightComponent : Component
{
    /// <summary>
    /// The possible colors to pick from.
    /// </summary>
    [DataField]
    public List<Color> Colors = new()
    {
        Color.White,
        Color.Red,
        Color.Yellow,
        Color.Green,
        Color.Blue,
        Color.Purple,
        Color.Pink
    };

    /// <summary>
    /// The min and max energy to pick from.
    /// </summary>
    [DataField(required: true)]
    public Vector2 Energy = default!;
}
