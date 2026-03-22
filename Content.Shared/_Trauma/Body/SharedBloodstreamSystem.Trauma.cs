// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Components;
using Content.Goobstation.Maths.FixedPoint;

namespace Content.Shared.Body.Systems;

/// <summary>
/// Trauma - helper methods for bloodstream modification.
/// </summary>
public abstract partial class SharedBloodstreamSystem
{
    /// <summary>
    /// Sets the blood refresh amount for a bloodstream component.
    /// </summary>
    public void SetBloodRefreshAmount(Entity<BloodstreamComponent> ent, FixedPoint2 amount)
    {
        ent.Comp.BloodRefreshAmount = amount;
        Dirty(ent);
    }
}
