// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Popups;
using Content.Server.Power.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared._Capibara.Botany.Ui;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._Capibara.Botany.Machines;

public sealed class GeneSplicerSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly ItemSlotsSystem _itemSlots = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly GeneDiskSystem _geneDiskSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        Subs.BuiEvents<GeneSplicerComponent>(GeneSplicerUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnUiOpened);
            subs.Event<GeneSplicerSpliceMessage>(OnSplice);
            subs.Event<GeneSplicerEjectAMessage>(OnEjectA);
            subs.Event<GeneSplicerEjectBMessage>(OnEjectB);
        });

        SubscribeLocalEvent<GeneSplicerComponent, EntInsertedIntoContainerMessage>(OnItemChanged);
        SubscribeLocalEvent<GeneSplicerComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
        SubscribeLocalEvent<GeneSplicerComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, GeneSplicerComponent comp, InteractUsingEvent args)
    {
        if (args.Handled || !this.IsPowered(uid, EntityManager))
            return;

        if (!HasComp<GeneDiskComponent>(args.Used))
            return;

        // Try slot A first, then B
        if (!comp.DiskSlotA.HasItem)
        {
            if (_itemSlots.TryInsertFromHand(uid, comp.DiskSlotA, args.User))
            {
                args.Handled = true;
                UpdateUiState(uid, comp);
            }
        }
        else if (!comp.DiskSlotB.HasItem)
        {
            if (_itemSlots.TryInsertFromHand(uid, comp.DiskSlotB, args.User))
            {
                args.Handled = true;
                UpdateUiState(uid, comp);
            }
        }
    }

    private void OnItemChanged(EntityUid uid, GeneSplicerComponent comp, EntInsertedIntoContainerMessage args)
        => UpdateUiState(uid, comp);

    private void OnItemRemoved(EntityUid uid, GeneSplicerComponent comp, EntRemovedFromContainerMessage args)
        => UpdateUiState(uid, comp);

    private void OnUiOpened(EntityUid uid, GeneSplicerComponent comp, BoundUIOpenedEvent args)
        => UpdateUiState(uid, comp);

    private void OnEjectA(EntityUid uid, GeneSplicerComponent comp, GeneSplicerEjectAMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.DiskSlotA, args.Actor);
        UpdateUiState(uid, comp);
    }

    private void OnEjectB(EntityUid uid, GeneSplicerComponent comp, GeneSplicerEjectBMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.DiskSlotB, args.Actor);
        UpdateUiState(uid, comp);
    }

    private void OnSplice(EntityUid uid, GeneSplicerComponent comp, GeneSplicerSpliceMessage args)
    {
        if (!this.IsPowered(uid, EntityManager))
            return;

        if (comp.DiskSlotA.Item is not { } diskAItem || comp.DiskSlotB.Item is not { } diskBItem)
            return;

        if (!TryComp<GeneDiskComponent>(diskAItem, out var diskA) || diskA.StoredGene == null)
            return;
        if (!TryComp<GeneDiskComponent>(diskBItem, out var diskB) || diskB.StoredGene == null)
            return;

        // Find matching recipe
        var recipe = FindRecipe(diskA.StoredGene.Value, diskB.StoredGene.Value);
        if (recipe == null)
        {
            _popup.PopupEntity(Loc.GetString("capibara-gene-splicer-no-recipe"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }

        // Roll for success
        if (_random.Prob(recipe.SuccessChance))
        {
            // Success — disk A gets result gene, disk B is emptied
            diskA.StoredGene = recipe.Output;
            diskB.StoredGene = null;
            Dirty(diskAItem, diskA);
            Dirty(diskBItem, diskB);
            _geneDiskSystem.UpdateDiskName(diskAItem, diskA);
            _geneDiskSystem.UpdateDiskName(diskBItem, diskB);
            _popup.PopupEntity(Loc.GetString("capibara-gene-splicer-success"), uid, args.Actor, PopupType.Medium);
        }
        else
        {
            // Failure — destroy disk B's gene, degrade both disks
            diskB.StoredGene = null;
            diskA.Integrity -= 25f;
            diskB.Integrity -= 25f;
            Dirty(diskAItem, diskA);
            Dirty(diskBItem, diskB);
            _geneDiskSystem.UpdateDiskName(diskBItem, diskB);

            // Destroy disks at 0 integrity
            if (diskA.Integrity <= 0)
                QueueDel(diskAItem);
            if (diskB.Integrity <= 0)
                QueueDel(diskBItem);

            _popup.PopupEntity(Loc.GetString("capibara-gene-splicer-failure"), uid, args.Actor, PopupType.LargeCaution);
        }

        UpdateUiState(uid, comp);
    }

    private GeneSpliceRecipePrototype? FindRecipe(ProtoId<PlantGenePrototype> geneA, ProtoId<PlantGenePrototype> geneB)
    {
        foreach (var recipe in _protoManager.EnumeratePrototypes<GeneSpliceRecipePrototype>())
        {
            if ((recipe.InputA == geneA && recipe.InputB == geneB) ||
                (recipe.InputA == geneB && recipe.InputB == geneA))
            {
                return recipe;
            }
        }
        return null;
    }

    private void UpdateUiState(EntityUid uid, GeneSplicerComponent comp)
    {
        var state = new GeneSplicerBuiState();

        GeneDiskComponent? diskA = null;
        GeneDiskComponent? diskB = null;

        if (comp.DiskSlotA.Item is { } diskAItem && TryComp(diskAItem, out diskA))
        {
            state.HasDiskA = true;
            state.DiskAIntegrity = diskA.Integrity;
            if (diskA.StoredGene != null && _protoManager.TryIndex(diskA.StoredGene.Value, out var gp))
            {
                state.DiskAHasGene = true;
                state.GeneNameA = Loc.GetString(gp.Name);
                state.RarityA = gp.Rarity;
            }
        }

        if (comp.DiskSlotB.Item is { } diskBItem && TryComp(diskBItem, out diskB))
        {
            state.HasDiskB = true;
            state.DiskBIntegrity = diskB.Integrity;
            if (diskB.StoredGene != null && _protoManager.TryIndex(diskB.StoredGene.Value, out var gp))
            {
                state.DiskBHasGene = true;
                state.GeneNameB = Loc.GetString(gp.Name);
                state.RarityB = gp.Rarity;
            }
        }

        // Check if a recipe exists
        if (diskA?.StoredGene != null && diskB?.StoredGene != null)
        {
            var recipe = FindRecipe(diskA.StoredGene.Value, diskB.StoredGene.Value);
            if (recipe != null)
            {
                state.CanSplice = true;
                state.SuccessChance = recipe.SuccessChance;
                if (_protoManager.TryIndex(recipe.Output, out var outputGene))
                    state.ResultGeneName = Loc.GetString(outputGene.Name);
            }
        }

        _uiSystem.SetUiState(uid, GeneSplicerUiKey.Key, state);
    }
}
