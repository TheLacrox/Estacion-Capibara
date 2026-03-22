// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body;
using Robust.Shared.Prototypes;

namespace Content.Shared.Body.Organ;

/// <summary>
/// Trauma - adds Category field to OrganComponent for visual organ management.
/// </summary>
public sealed partial class OrganComponent
{
    /// <summary>
    /// The organ category for visual management (e.g., "Eyes", "Torso").
    /// </summary>
    [DataField]
    public ProtoId<OrganCategoryPrototype>? Category;
}
