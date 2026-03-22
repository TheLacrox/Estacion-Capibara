// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Organ;
using Robust.Shared.Prototypes;

namespace Content.Shared.Body.Systems;

/// <summary>
/// Trauma - helper methods for body system organ lookup by category.
/// </summary>
public partial class SharedBodySystem
{
    /// <summary>
    /// Gets the first organ entity matching the given category on a body.
    /// </summary>
    public EntityUid? GetOrgan(EntityUid body, ProtoId<OrganCategoryPrototype> category)
    {
        foreach (var (organUid, organComp) in GetBodyOrgans(body))
        {
            if (organComp.Category == category)
                return organUid;
        }

        return null;
    }

    /// <summary>
    /// Removes an organ from the body. Returns true if removal was successful.
    /// Wrapper that finds and removes the organ by UID.
    /// </summary>
    public bool RemoveOrgan(EntityUid body, EntityUid organ)
    {
        return RemoveOrgan(organ);
    }
}
