// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Trauma.Genetics.Mutations;

/// <summary>
/// An entity with this component cannot read paper or books.
/// Used by the genetics Illiterate mutation.
/// </summary>
[RegisterComponent]
public sealed partial class BlockReadingComponent : Component
{
    /// <summary>
    /// What message is displayed when the entity fails to read.
    /// </summary>
    [DataField]
    public LocId FailReadMessage = "paper-component-illiterate";
}
