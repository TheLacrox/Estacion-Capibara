// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Capibara.Botany.Events;

/// <summary>
/// Raised after two plants are cross-pollinated via the botany swab.
/// Used by PlantGenomeSystem to merge gene slots with weighted inheritance.
/// </summary>
[ByRefEvent]
public record struct PlantCrossPollinatedEvent(EntityUid SourcePlant, EntityUid TargetPlant);
