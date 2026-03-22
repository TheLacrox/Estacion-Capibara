// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared.Body.Events;

/// <summary>
/// Raised on an entity before they bleed to modify the amount.
/// </summary>
[ByRefEvent]
public record struct BleedModifierEvent(float BleedAmount, float BleedReductionAmount);
