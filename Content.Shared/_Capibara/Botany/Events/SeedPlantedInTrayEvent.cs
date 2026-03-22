// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Capibara.Botany.Events;

/// <summary>
/// Raised when a seed is planted into a hydroponics tray.
/// Used by PlantGenomeSystem to initialize the genome component.
/// </summary>
[ByRefEvent]
public readonly record struct SeedPlantedInTrayEvent(EntityUid PlantHolder, EntityUid SeedEntity, EntityUid User);
