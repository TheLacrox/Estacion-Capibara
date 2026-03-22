// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Capibara.Botany.Ui;

[Serializable, NetSerializable]
public enum PlantCentrifugeUiKey : byte
{
    Key
}

[Serializable, NetSerializable]
public sealed class PlantCentrifugeBuiState : BoundUserInterfaceState
{
    public bool HasProduce;
    public string? ProduceName;
    public bool CanProcess;
}

[Serializable, NetSerializable]
public sealed class PlantCentrifugeProcessMessage : BoundUserInterfaceMessage
{
}

[Serializable, NetSerializable]
public sealed class PlantCentrifugeEjectMessage : BoundUserInterfaceMessage
{
}
