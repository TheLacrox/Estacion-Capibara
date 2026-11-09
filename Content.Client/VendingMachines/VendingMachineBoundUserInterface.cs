// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Client.UserInterface.Controls;
using Content.Client.VendingMachines.UI;
using Content.Shared._Capibara.Economy.Components;
using Content.Shared.Inventory;
using Content.Shared.PDA;
using Content.Shared.VendingMachines;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Shared.Input;
using System.Linq;
using Content.Client._Funkystation.VendingMachines; // Goob

namespace Content.Client.VendingMachines
{
    public sealed class VendingMachineBoundUserInterface : BoundUserInterface, IVendingMachineBoundUi // Goob
    {
        [ViewVariables]
        private VendingMachineMenu? _menu;

        [ViewVariables]
        private List<VendingMachineInventoryEntry> _cachedInventory = new();

        public VendingMachineBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        protected override void Open()
        {
            base.Open();

            _menu = this.CreateWindowCenteredLeft<VendingMachineMenu>();
            _menu.Title = EntMan.GetComponent<MetaDataComponent>(Owner).EntityName;
            _menu.OnItemSelected += OnItemSelected;
            Refresh();
        }

        public void Refresh()
        {
            var enabled = EntMan.TryGetComponent(Owner, out VendingMachineComponent? bendy) && !bendy.Ejecting;

            var system = EntMan.System<VendingMachineSystem>();
            _cachedInventory = system.GetAllInventory(Owner);

            // Capibara: pass price data before populating
            UpdatePriceData();
            _menu?.Populate(_cachedInventory, enabled);
        }

        public void UpdateAmounts()
        {
            var enabled = EntMan.TryGetComponent(Owner, out VendingMachineComponent? bendy) && !bendy.Ejecting;

            var system = EntMan.System<VendingMachineSystem>();
            _cachedInventory = system.GetAllInventory(Owner);

            // Capibara: update price data
            UpdatePriceData();
            _menu?.UpdateAmounts(_cachedInventory, enabled);
        }

        /// <summary>
        /// Capibara: Check if this vendor has prices and pass them to the menu.
        /// </summary>
        private void UpdatePriceData()
        {
            if (_menu == null)
                return;

            if (!EntMan.TryGetComponent<VendingMachinePriceComponent>(Owner, out var priceComp))
            {
                _menu.SetPrices(null, 0, null);
                return;
            }

            // Get the player's bank balance from their ID card
            int? balance = null;
            var playerManager = IoCManager.Resolve<IPlayerManager>();
            if (playerManager.LocalEntity is { } player)
            {
                var invSystem = EntMan.System<InventorySystem>();
                if (invSystem.TryGetSlotEntity(player, "id", out var idSlot))
                {
                    var idCard = idSlot.Value;
                    if (EntMan.TryGetComponent<PdaComponent>(idSlot, out var pda) && pda.ContainedId != null)
                        idCard = pda.ContainedId.Value;

                    if (EntMan.TryGetComponent<BankAccountComponent>(idCard, out var bank))
                        balance = bank.Balance;
                }
            }

            _menu.SetPrices(priceComp.ItemPrices, priceComp.DefaultPrice, balance);
        }

        private void OnItemSelected(GUIBoundKeyEventArgs args, ListData data)
        {
            if (args.Function != EngineKeyFunctions.UIClick)
                return;

            if (data is not VendorItemsListData { ItemIndex: var itemIndex })
                return;

            if (_cachedInventory.Count == 0)
                return;

            var selectedItem = _cachedInventory.ElementAtOrDefault(itemIndex);

            if (selectedItem == null)
                return;

            SendPredictedMessage(new VendingMachineEjectMessage(selectedItem.Type, selectedItem.ID));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;

            if (_menu == null)
                return;

            _menu.OnItemSelected -= OnItemSelected;
            _menu.OnClose -= Close;
            _menu.Dispose();
        }
    }
}