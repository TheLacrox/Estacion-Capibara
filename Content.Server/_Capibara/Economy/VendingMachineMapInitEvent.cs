// SPDX-FileCopyrightText: 2026 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Server._Capibara.Economy;

/// <summary>
/// Raised on a vending machine at the end of its map init.
/// Upstream already owns the (VendingMachineComponent, MapInitEvent) subscription.
/// </summary>
[ByRefEvent]
public readonly record struct VendingMachineMapInitEvent;
