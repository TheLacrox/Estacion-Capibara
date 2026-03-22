// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Ui;

[Serializable, NetSerializable]
public enum DnaManipulatorUiKey : byte
{
    Key
}

/// <summary>
/// State sent from server to client for the DNA Manipulator display.
/// </summary>
[Serializable, NetSerializable]
public sealed class DnaManipulatorBuiState : BoundUserInterfaceState
{
    // Seed info
    public bool HasSeed;
    public string? SeedSpeciesName;
    public List<GeneSlotData> GeneSlots = new();
    public float Instability;

    // Disk info
    public bool HasDisk;
    public bool DiskHasGene;
    public string? DiskGeneName;
    public PlantGeneRarity? DiskGeneRarity;
    public float DiskIntegrity;
}

/// <summary>
/// Client requests to extract a gene from a seed slot into the disk.
/// </summary>
[Serializable, NetSerializable]
public sealed class DnaManipulatorExtractMessage : BoundUserInterfaceMessage
{
    public int SlotIndex;

    public DnaManipulatorExtractMessage(int slotIndex)
    {
        SlotIndex = slotIndex;
    }
}

/// <summary>
/// Client requests to insert a gene from the disk into the seed.
/// </summary>
[Serializable, NetSerializable]
public sealed class DnaManipulatorInsertMessage : BoundUserInterfaceMessage
{
}

/// <summary>
/// Client requests to eject the seed.
/// </summary>
[Serializable, NetSerializable]
public sealed class DnaManipulatorEjectSeedMessage : BoundUserInterfaceMessage
{
}

/// <summary>
/// Client requests to eject the disk.
/// </summary>
[Serializable, NetSerializable]
public sealed class DnaManipulatorEjectDiskMessage : BoundUserInterfaceMessage
{
}
