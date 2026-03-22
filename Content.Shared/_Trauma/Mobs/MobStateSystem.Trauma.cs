// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Mobs.Components;

namespace Content.Shared.Mobs.Systems;

/// <summary>
/// Trauma - methods relating to softcrit/hardcrit
/// </summary>
public partial class MobStateSystem
{
    /// <summary>
    /// Check if a Mob is specifically softcrit, not hardcrit.
    /// </summary>
    public bool IsSoftCrit(EntityUid target, MobStateComponent? component = null)
    {
        if (!_mobStateQuery.Resolve(target, ref component, false))
            return false;
        return component.CurrentState == MobState.SoftCrit;
    }

    /// <summary>
    /// Check if a Mob is specifically hardcrit, not softcrit.
    /// </summary>
    public bool IsHardCrit(EntityUid target, MobStateComponent? component = null)
    {
        if (!_mobStateQuery.Resolve(target, ref component, false))
            return false;
        return component.CurrentState == MobState.Critical;
    }
}
