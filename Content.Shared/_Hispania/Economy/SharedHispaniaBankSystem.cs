// SPDX-FileCopyrightText: 2025 Hispania Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Hispania.Economy.Components;
using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Hispania.Economy;

/// <summary>
/// Shared base for the Hispania banking system.
/// Registers the ATM ItemSlot on both client and server (same pattern as SharedIdCardConsoleSystem).
/// </summary>
public abstract partial class SharedHispaniaBankSystem : EntitySystem
{
    [Dependency] protected readonly ItemSlotsSystem _itemSlotsSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HispaniaATMComponent, ComponentInit>(OnATMInit);
        SubscribeLocalEvent<HispaniaATMComponent, ComponentRemove>(OnATMRemove);
    }

    private void OnATMInit(EntityUid uid, HispaniaATMComponent comp, ComponentInit args)
    {
        _itemSlotsSystem.AddItemSlot(uid, HispaniaATMComponent.IdSlotId, comp.IdSlot);
    }

    private void OnATMRemove(EntityUid uid, HispaniaATMComponent comp, ComponentRemove args)
    {
        _itemSlotsSystem.RemoveItemSlot(uid, comp.IdSlot);
    }
}
