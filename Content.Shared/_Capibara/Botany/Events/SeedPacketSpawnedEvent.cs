// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Capibara.Botany.Events;

/// <summary>
/// Raised when a seed packet is spawned from a plant (via clipping or seed extraction).
/// Used by PlantGenomeSystem to transfer genome data to the seed packet.
/// </summary>
[ByRefEvent]
public record struct SeedPacketSpawnedEvent(EntityUid SeedPacket, EntityUid SourcePlantHolder);
