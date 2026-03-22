// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Capibara.Botany.Events;

/// <summary>
/// Raised after a produce entity is spawned during harvest.
/// Used by PlantGenomeSystem to apply gene effects to the produce.
/// </summary>
[ByRefEvent]
public record struct ProduceEntitySpawnedEvent(EntityUid Produce, EntityUid PlantHolder);
