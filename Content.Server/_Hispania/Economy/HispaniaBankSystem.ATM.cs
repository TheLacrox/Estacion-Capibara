// SPDX-FileCopyrightText: 2025 Hispania Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Popups;
using Content.Server.Stack;
using Content.Shared._Hispania.Economy;
using Content.Shared._Hispania.Economy.Components;
using Content.Shared.Access.Components;
using Content.Shared.Cargo.Components;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.PDA;
using Content.Shared.Stacks;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;

namespace Content.Server._Hispania.Economy;

/// <summary>
/// Partial class handling ATM BUI interactions.
/// </summary>
public sealed partial class HispaniaBankSystem
{
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly StackSystem _stackSystem = default!;
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly SharedHandsSystem _hands = default!;
    [Dependency] private readonly TransformSystem _transform = default!;

    private void InitializeATM()
    {
        Subs.BuiEvents<HispaniaATMComponent>(HispaniaATMUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnATMUIOpen);
            subs.Event<HispaniaATMWithdrawMessage>(OnATMWithdraw);
            subs.Event<HispaniaATMDepositMessage>(OnATMDeposit);
            subs.Event<HispaniaATMInsertMessage>(OnATMInsert);
            subs.Event<HispaniaATMEjectMessage>(OnATMEject);
            subs.Event<HispaniaATMCreateAccountMessage>(OnATMCreateAccount);
        });

        SubscribeLocalEvent<HispaniaATMComponent, EntInsertedIntoContainerMessage>(OnATMItemInserted);
        SubscribeLocalEvent<HispaniaATMComponent, EntRemovedFromContainerMessage>(OnATMItemRemoved);
    }

    /// <summary>
    /// Gets the ID card entity from whatever is currently in the ATM's slot.
    /// Handles both bare ID cards and PDAs containing an ID card.
    /// </summary>
    private EntityUid? GetIdCardFromSlot(HispaniaATMComponent comp)
    {
        var item = comp.IdSlot.Item;
        if (item == null)
            return null;

        // If it's a PDA, get the contained ID
        if (TryComp<PdaComponent>(item.Value, out var pda) && pda.ContainedId != null)
            return pda.ContainedId.Value;

        // If it's a bare ID card
        if (HasComp<IdCardComponent>(item.Value))
            return item.Value;

        return null;
    }

    /// <summary>
    /// Sends the current ATM state to any open UI session.
    /// </summary>
    private void UpdateATMUiState(EntityUid uid, HispaniaATMComponent comp)
    {
        var hasId = comp.IdSlot.HasItem;
        var idCard = GetIdCardFromSlot(comp);

        // Always read name from the ID card itself
        var cardName = idCard != null && TryComp<IdCardComponent>(idCard.Value, out var card) ? card.FullName : null;

        if (idCard != null && TryComp<BankAccountComponent>(idCard.Value, out var bank))
        {
            // Card with bank account — normal state
            _uiSystem.SetUiState(uid, HispaniaATMUiKey.Key,
                new HispaniaATMState(bank.Balance, true, cardName, hasId, false));
        }
        else if (idCard != null)
        {
            // Card inserted but NO bank account — show create button
            _uiSystem.SetUiState(uid, HispaniaATMUiKey.Key,
                new HispaniaATMState(0, false, cardName, hasId, true));
        }
        else
        {
            // No card at all
            _uiSystem.SetUiState(uid, HispaniaATMUiKey.Key,
                new HispaniaATMState(0, false, null, hasId, false));
        }
    }

    private void OnATMUIOpen(EntityUid uid, HispaniaATMComponent comp, BoundUIOpenedEvent args)
    {
        UpdateATMUiState(uid, comp);
    }

    private void OnATMInsert(EntityUid uid, HispaniaATMComponent comp, HispaniaATMInsertMessage args)
    {
        var player = args.Actor;

        // Already has something inserted
        if (comp.IdSlot.HasItem)
            return;

        // Get the ID/PDA from the player's inventory "id" slot
        if (!_inventory.TryGetSlotEntity(player, "id", out var idSlot))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-id"), uid, player);
            return;
        }

        // Unequip from inventory and insert into ATM slot
        if (_inventory.TryUnequip(player, "id", force: true) &&
            _itemSlotsSystem.TryInsert(uid, comp.IdSlot, idSlot.Value, player))
        {
            _audio.PlayPvs(comp.ConfirmSound, uid);
        }
    }

    private void OnATMEject(EntityUid uid, HispaniaATMComponent comp, HispaniaATMEjectMessage args)
    {
        _itemSlotsSystem.TryEjectToHands(uid, comp.IdSlot, args.Actor);
    }

    private void OnATMCreateAccount(EntityUid uid, HispaniaATMComponent comp, HispaniaATMCreateAccountMessage args)
    {
        var player = args.Actor;
        var idCard = GetIdCardFromSlot(comp);

        if (idCard == null)
            return;

        // Already has an account
        if (HasComp<BankAccountComponent>(idCard.Value))
            return;

        // Create account with starting balance based on the card's job
        var bank = EnsureComp<BankAccountComponent>(idCard.Value);

        string? cardName = null;
        string? jobId = null;
        if (TryComp<IdCardComponent>(idCard.Value, out var idCardComp))
        {
            cardName = idCardComp.FullName;
            jobId = idCardComp.JobPrototype?.Id;
        }

        bank.Balance = GetStartingBalance(jobId);
        Dirty(idCard.Value, bank);

        _audio.PlayPvs(comp.ConfirmSound, uid);
        _popup.PopupEntity(Loc.GetString("hispania-atm-account-created"), uid, player);

        UpdateATMUiState(uid, comp);
        _log.Info($"Bank account created at ATM for {cardName} (job: {jobId}) with {bank.Balance} Spesos.");
    }

    private void OnATMItemInserted(EntityUid uid, HispaniaATMComponent comp, EntInsertedIntoContainerMessage args)
    {
        UpdateATMUiState(uid, comp);
    }

    private void OnATMItemRemoved(EntityUid uid, HispaniaATMComponent comp, EntRemovedFromContainerMessage args)
    {
        UpdateATMUiState(uid, comp);
    }

    private void OnATMWithdraw(EntityUid uid, HispaniaATMComponent comp, HispaniaATMWithdrawMessage args)
    {
        var player = args.Actor;
        var idCard = GetIdCardFromSlot(comp);

        if (idCard == null || !TryComp<BankAccountComponent>(idCard.Value, out var bank))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-id"), uid, player);
            return;
        }

        if (args.Amount <= 0)
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-invalid-amount"), uid, player);
            return;
        }

        if (!TryWithdraw(idCard.Value, args.Amount, out var newBalance))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-insufficient-funds"), uid, player);
            return;
        }

        // Spawn cash stack at ATM location
        var coordinates = _transform.GetMoverCoordinates(uid);
        var cashEntity = _stackSystem.Spawn(args.Amount, new ProtoId<StackPrototype>("Credit"), coordinates);

        // Try to put it in the player's hands
        _hands.TryPickupAnyHand(player, cashEntity);

        _audio.PlayPvs(comp.ConfirmSound, uid);
        _popup.PopupEntity(Loc.GetString("hispania-atm-withdraw-success", ("amount", args.Amount)), uid, player);

        // Update the ATM UI
        UpdateATMUiState(uid, comp);

        var withdrawName = TryComp<IdCardComponent>(idCard.Value, out var withdrawCard) ? withdrawCard.FullName : "Unknown";
        _log.Info($"ATM withdraw: {withdrawName} withdrew {args.Amount} Spesos. New balance: {newBalance}");
    }

    private void OnATMDeposit(EntityUid uid, HispaniaATMComponent comp, HispaniaATMDepositMessage args)
    {
        var player = args.Actor;
        var idCard = GetIdCardFromSlot(comp);

        if (idCard == null || !TryComp<BankAccountComponent>(idCard.Value, out var bank))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-id"), uid, player);
            return;
        }

        // Check what the player is holding in their active hand
        var heldEntity = _hands.GetActiveItem(player);
        if (heldEntity == null)
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-cash"), uid, player);
            return;
        }

        // Verify it's cash (has CashComponent and StackComponent)
        if (!HasComp<CashComponent>(heldEntity.Value) || !TryComp<StackComponent>(heldEntity.Value, out var stack))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-cash"), uid, player);
            return;
        }

        var amount = stack.Count;
        if (amount <= 0)
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            _popup.PopupEntity(Loc.GetString("hispania-atm-no-cash"), uid, player);
            return;
        }

        if (!TryDeposit(idCard.Value, amount, out var newBalance))
        {
            _audio.PlayPvs(comp.ErrorSound, uid);
            return;
        }

        // Delete the cash from the player's hand
        QueueDel(heldEntity.Value);

        _audio.PlayPvs(comp.ConfirmSound, uid);
        _popup.PopupEntity(Loc.GetString("hispania-atm-deposit-success", ("amount", amount)), uid, player);

        // Update the ATM UI
        UpdateATMUiState(uid, comp);

        var depositName = TryComp<IdCardComponent>(idCard.Value, out var depositCard) ? depositCard.FullName : "Unknown";
        _log.Info($"ATM deposit: {depositName} deposited {amount} Spesos. New balance: {newBalance}");
    }
}
