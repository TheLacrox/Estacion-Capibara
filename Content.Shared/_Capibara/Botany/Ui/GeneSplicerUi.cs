// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Ui;

[Serializable, NetSerializable]
public enum GeneSplicerUiKey : byte
{
    Key
}

[Serializable, NetSerializable]
public sealed class GeneSplicerBuiState : BoundUserInterfaceState
{
    public bool HasDiskA;
    public bool DiskAHasGene;
    public string? GeneNameA;
    public PlantGeneRarity? RarityA;
    public float DiskAIntegrity;

    public bool HasDiskB;
    public bool DiskBHasGene;
    public string? GeneNameB;
    public PlantGeneRarity? RarityB;
    public float DiskBIntegrity;

    public bool CanSplice;
    public string? ResultGeneName;
    public float SuccessChance;
}

[Serializable, NetSerializable]
public sealed class GeneSplicerSpliceMessage : BoundUserInterfaceMessage
{
}

[Serializable, NetSerializable]
public sealed class GeneSplicerEjectAMessage : BoundUserInterfaceMessage
{
}

[Serializable, NetSerializable]
public sealed class GeneSplicerEjectBMessage : BoundUserInterfaceMessage
{
}
