// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Components;

/// <summary>
/// Stores the genome data for a plant in a hydroponics tray.
/// Layered genome: core species (immutable) + gene slots (swappable) + epigenetics (temporary).
/// Stats are written back to SeedData via PlantGenomeSystem.RecalculateStats().
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PlantGenomeComponent : Component
{
    /// <summary>
    /// The base species this plant was initialized from. Immutable.
    /// </summary>
    [DataField, AutoNetworkedField]
    public string? CoreSpeciesId;

    /// <summary>
    /// Swappable gene slots. Genes can be extracted/inserted via machines.
    /// Default 6 slots, upgradeable to 8 with research.
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<PlantGeneSlot> GeneSlots = new();

    /// <summary>
    /// Maximum number of gene slots this plant can have.
    /// </summary>
    [DataField, AutoNetworkedField]
    public int MaxSlots = 6;

    /// <summary>
    /// Current instability level. Computed from gene costs + slot penalties.
    /// Higher instability = more dangerous side effects.
    /// </summary>
    [DataField, AutoNetworkedField]
    public float Instability;

    /// <summary>
    /// Temporary environmental buffs/debuffs that decay over time.
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<EpigeneticModifier> Epigenetics = new();

    /// <summary>
    /// Server-only snapshot of the original SeedData values before any gene modifications.
    /// Used by RecalculateStats() to restore base values before reapplying genes.
    /// </summary>
    public Dictionary<string, float> BaseStatSnapshot = new();

    /// <summary>
    /// Server-only snapshot of the original boolean trait values (Seedless, Ligneous, etc.).
    /// </summary>
    public Dictionary<string, bool> BaseBoolSnapshot = new();

    /// <summary>
    /// Server-only snapshot of the original harvest type.
    /// </summary>
    public int BaseHarvestType;

    /// <summary>
    /// Server-only snapshot of the original SeedData chemicals (serialized as tuples).
    /// Stored as (Min, Max, PotencyDivisor) per reagent ID, since SeedChemQuantity is server-only.
    /// </summary>
    public Dictionary<string, (int Min, int Max, int PotDiv)> BaseChemSnapshot = new();

    /// <summary>
    /// Whether the genome has been initialized with a base stat snapshot.
    /// </summary>
    public bool Initialized;
}
