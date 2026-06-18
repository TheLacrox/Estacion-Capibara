// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.IntegrationTests.Tests.Interaction;
using Content.Shared._Capibara.Economy;
using Content.Shared._Capibara.Economy.Components;
using Content.Shared.Cargo.Components;
using Content.Shared.Containers.ItemSlots;
using Robust.Shared.GameObjects;

namespace Content.IntegrationTests.Tests._Capibara.Economy;

/// <summary>
/// Headless integration tests for the Capibara ATM banking flow.
/// Drives the ATM the same way a player would: spawns the machine, inserts an ID card
/// with a bank account, powers it, opens the BUI and sends real withdraw/deposit messages,
/// then asserts the resulting game state. No client window or manual play required.
/// </summary>
public sealed class CapibaraAtmInteractionTest : InteractionTest
{
    private const string AtmProtoId = "CapibaraATM";
    private const string IdCardProtoId = "PassengerIDCard";

    /// <summary>
    /// Spawns an ID card with the given balance and inserts it into the target ATM's slot.
    /// Returns the server-side ID card entity so tests can read its balance afterwards.
    /// </summary>
    private async Task<EntityUid> InsertAccountCard(int startingBalance)
    {
        var itemSlots = SEntMan.System<ItemSlotsSystem>();
        var atm = SEntMan.GetEntity(Target!.Value);
        Assert.That(TryComp<CapibaraATMComponent>(out var atmComp), "ATM missing CapibaraATMComponent.");

        var idCard = EntityUid.Invalid;
        await Server.WaitPost(() =>
        {
            idCard = SEntMan.SpawnEntity(IdCardProtoId, SEntMan.GetCoordinates(TargetCoords));
            SEntMan.EnsureComponent<BankAccountComponent>(idCard).Balance = startingBalance;
            Assert.That(itemSlots.TryInsert(atm, atmComp!.IdSlot, idCard, null), "Failed to insert ID card into ATM slot.");
        });
        await RunTicks(5);
        return idCard;
    }

    private int GetBalance(EntityUid idCard)
    {
        return SEntMan.GetComponent<BankAccountComponent>(idCard).Balance;
    }

    [Test]
    public async Task WithdrawTest()
    {
        await SpawnTarget(AtmProtoId);
        var idCard = await InsertAccountCard(1000);

        // Power the ATM (BUI requires power, just like the vending machine test).
        await SpawnEntity("APCBasic", SEntMan.GetCoordinates(TargetCoords));
        await RunTicks(5);

        // Activating the powered ATM opens the BUI for the player.
        await Activate();
        Assert.That(IsUiOpen(CapibaraATMUiKey.Key), "ATM BUI failed to open.");

        // Withdraw 300 — same message the client window sends on button press.
        await SendBui(CapibaraATMUiKey.Key, new CapibaraATMWithdrawMessage(300));

        Assert.That(GetBalance(idCard), Is.EqualTo(700), "Balance did not decrease by the withdrawn amount.");

        // The withdrawn cash should end up in the player's hand.
        var held = HandSys.GetActiveItem((SEntMan.GetEntity(Player), Hands));
        Assert.That(held, Is.Not.Null, "No cash was placed in the player's hand after withdrawal.");
        Assert.That(SEntMan.HasComponent<CashComponent>(held!.Value), "Item placed in hand is not cash.");

        // Over-withdrawing must be rejected and leave the balance untouched.
        await SendBui(CapibaraATMUiKey.Key, new CapibaraATMWithdrawMessage(99999));
        Assert.That(GetBalance(idCard), Is.EqualTo(700), "Balance changed on an insufficient-funds withdrawal.");
    }

    [Test]
    public async Task DepositByClickTest()
    {
        await SpawnTarget(AtmProtoId);
        var idCard = await InsertAccountCard(0);

        // Clicking the ATM with cash in hand deposits it directly (InteractUsing path, no power needed).
        await InteractUsing("SpaceCash", 500);

        Assert.That(GetBalance(idCard), Is.EqualTo(500), "Clicking the ATM with cash did not credit the account.");
    }

    [Test]
    public async Task NoAccountWithdrawTest()
    {
        await SpawnTarget(AtmProtoId);

        // Insert a card that has NO bank account.
        var itemSlots = SEntMan.System<ItemSlotsSystem>();
        var atm = SEntMan.GetEntity(Target!.Value);
        Assert.That(TryComp<CapibaraATMComponent>(out var atmComp), "ATM missing CapibaraATMComponent.");

        var idCard = EntityUid.Invalid;
        await Server.WaitPost(() =>
        {
            idCard = SEntMan.SpawnEntity(IdCardProtoId, SEntMan.GetCoordinates(TargetCoords));
            Assert.That(itemSlots.TryInsert(atm, atmComp!.IdSlot, idCard, null), "Failed to insert ID card into ATM slot.");
        });
        await RunTicks(5);

        await SpawnEntity("APCBasic", SEntMan.GetCoordinates(TargetCoords));
        await RunTicks(5);

        await Activate();
        Assert.That(IsUiOpen(CapibaraATMUiKey.Key), "ATM BUI failed to open.");

        // Withdrawing from a card with no account must not create one or crash.
        await SendBui(CapibaraATMUiKey.Key, new CapibaraATMWithdrawMessage(100));
        Assert.That(SEntMan.HasComponent<BankAccountComponent>(idCard), Is.False,
            "Withdrawing from an account-less card unexpectedly created a bank account.");
    }
}
