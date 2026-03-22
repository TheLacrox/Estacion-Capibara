// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared.Trigger.Systems;

/// <summary>
/// Raised after an entity has been DNA Scrambled.
/// </summary>
[ByRefEvent]
public record struct DnaScrambledEvent(EntityUid Target);
