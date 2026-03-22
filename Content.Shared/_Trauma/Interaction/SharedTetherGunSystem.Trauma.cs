// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Weapons.Misc;

namespace Content.Shared.Weapons.Misc;

/// <summary>
/// Trauma - exposes StopTether publicly for TelekinesisSystem.
/// </summary>
public abstract partial class SharedTetherGunSystem
{
    public void PublicStopTether(EntityUid gunUid, BaseForceGunComponent component, bool land = true, bool transfer = false)
    {
        StopTether(gunUid, component, land, transfer);
    }
}
