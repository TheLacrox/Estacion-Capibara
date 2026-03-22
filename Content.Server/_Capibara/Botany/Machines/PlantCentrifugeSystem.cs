// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Server.Botany.Systems;
using Content.Server.Popups;
using Content.Server.Power.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Ui;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Fluids.Components;
using Content.Server.Fluids.EntitySystems;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Containers;

namespace Content.Server._Capibara.Botany.Machines;

/// <summary>
/// Processes produce into separated chemicals with better yield than grinding.
/// Safely handles dangerous gene-modified produce.
/// </summary>
public sealed class PlantCentrifugeSystem : EntitySystem
{
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly BotanySystem _botanySystem = default!;
    [Dependency] private readonly ItemSlotsSystem _itemSlots = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly SharedSolutionContainerSystem _solutionSystem = default!;
    [Dependency] private readonly PuddleSystem _puddleSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        Subs.BuiEvents<PlantCentrifugeComponent>(PlantCentrifugeUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnUiOpened);
            subs.Event<PlantCentrifugeProcessMessage>(OnProcess);
            subs.Event<PlantCentrifugeEjectMessage>(OnEject);
        });

        SubscribeLocalEvent<PlantCentrifugeComponent, EntInsertedIntoContainerMessage>(OnItemChanged);
        SubscribeLocalEvent<PlantCentrifugeComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
        SubscribeLocalEvent<PlantCentrifugeComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, PlantCentrifugeComponent comp, InteractUsingEvent args)
    {
        if (args.Handled || !this.IsPowered(uid, EntityManager))
            return;

        if (!HasComp<ProduceComponent>(args.Used))
            return;

        if (_itemSlots.TryInsertFromHand(uid, comp.ProduceSlot, args.User))
        {
            args.Handled = true;
            UpdateUiState(uid, comp);
        }
    }

    private void OnItemChanged(EntityUid uid, PlantCentrifugeComponent comp, EntInsertedIntoContainerMessage args)
        => UpdateUiState(uid, comp);

    private void OnItemRemoved(EntityUid uid, PlantCentrifugeComponent comp, EntRemovedFromContainerMessage args)
        => UpdateUiState(uid, comp);

    private void OnUiOpened(EntityUid uid, PlantCentrifugeComponent comp, BoundUIOpenedEvent args)
        => UpdateUiState(uid, comp);

    private void OnEject(EntityUid uid, PlantCentrifugeComponent comp, PlantCentrifugeEjectMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.ProduceSlot, args.Actor);
        UpdateUiState(uid, comp);
    }

    private void OnProcess(EntityUid uid, PlantCentrifugeComponent comp, PlantCentrifugeProcessMessage args)
    {
        if (!this.IsPowered(uid, EntityManager))
            return;

        if (comp.ProduceSlot.Item is not { } produceItem)
            return;

        if (!TryComp<ProduceComponent>(produceItem, out var produce))
            return;

        // Extract chemicals from produce and spill them at the centrifuge location
        // The centrifuge safely processes all produce types (no explosions, no toxin clouds)
        if (_solutionSystem.TryGetSolution(produceItem, "food", out var soln, out var solution))
        {
            // Build the spill solution BEFORE removing from produce
            var spillSoln = new Solution();
            foreach (var reagent in solution.Contents)
            {
                // Apply yield multiplier
                var amount = reagent.Quantity * comp.YieldMultiplier;
                spillSoln.AddReagent(reagent.Reagent, amount);
            }

            // Remove from produce and spill at centrifuge
            _solutionSystem.RemoveAllSolution(soln.Value);
            _puddleSystem.TrySpillAt(uid, spillSoln, out _);
        }

        _popup.PopupEntity(Loc.GetString("capibara-plant-centrifuge-processed"), uid, args.Actor, PopupType.Medium);

        // Delete the produce
        QueueDel(produceItem);
        UpdateUiState(uid, comp);
    }

    private void UpdateUiState(EntityUid uid, PlantCentrifugeComponent comp)
    {
        var state = new PlantCentrifugeBuiState();

        if (comp.ProduceSlot.Item is { } item)
        {
            state.HasProduce = true;
            state.ProduceName = MetaData(item).EntityName;
            state.CanProcess = true;
        }

        _uiSystem.SetUiState(uid, PlantCentrifugeUiKey.Key, state);
    }
}
