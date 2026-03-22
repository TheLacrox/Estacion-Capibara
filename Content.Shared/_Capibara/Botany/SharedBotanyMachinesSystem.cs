// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Content.Shared.Containers.ItemSlots;

namespace Content.Shared._Capibara.Botany;

/// <summary>
/// Registers item slots for all Capibara botany machines.
/// Must be shared so both client and server know about the slots.
/// </summary>
public sealed class SharedBotanyMachinesSystem : EntitySystem
{
    [Dependency] private readonly ItemSlotsSystem _itemSlots = default!;

    public override void Initialize()
    {
        base.Initialize();

        // Seed Analyzer
        SubscribeLocalEvent<SeedAnalyzerComponent, ComponentInit>(OnSeedAnalyzerInit);
        SubscribeLocalEvent<SeedAnalyzerComponent, ComponentRemove>(OnSeedAnalyzerRemove);

        // DNA Manipulator
        SubscribeLocalEvent<DnaManipulatorComponent, ComponentInit>(OnDnaManipulatorInit);
        SubscribeLocalEvent<DnaManipulatorComponent, ComponentRemove>(OnDnaManipulatorRemove);

        // Gene Splicer
        SubscribeLocalEvent<GeneSplicerComponent, ComponentInit>(OnGeneSplicerInit);
        SubscribeLocalEvent<GeneSplicerComponent, ComponentRemove>(OnGeneSplicerRemove);

        // Plant Centrifuge
        SubscribeLocalEvent<PlantCentrifugeComponent, ComponentInit>(OnPlantCentrifugeInit);
        SubscribeLocalEvent<PlantCentrifugeComponent, ComponentRemove>(OnPlantCentrifugeRemove);
    }

    // Seed Analyzer
    private void OnSeedAnalyzerInit(EntityUid uid, SeedAnalyzerComponent comp, ComponentInit args)
    {
        _itemSlots.AddItemSlot(uid, SeedAnalyzerComponent.SeedSlotId, comp.SeedSlot);
    }

    private void OnSeedAnalyzerRemove(EntityUid uid, SeedAnalyzerComponent comp, ComponentRemove args)
    {
        _itemSlots.RemoveItemSlot(uid, comp.SeedSlot);
    }

    // DNA Manipulator
    private void OnDnaManipulatorInit(EntityUid uid, DnaManipulatorComponent comp, ComponentInit args)
    {
        _itemSlots.AddItemSlot(uid, DnaManipulatorComponent.SeedSlotId, comp.SeedSlot);
        _itemSlots.AddItemSlot(uid, DnaManipulatorComponent.DiskSlotId, comp.DiskSlot);
    }

    private void OnDnaManipulatorRemove(EntityUid uid, DnaManipulatorComponent comp, ComponentRemove args)
    {
        _itemSlots.RemoveItemSlot(uid, comp.SeedSlot);
        _itemSlots.RemoveItemSlot(uid, comp.DiskSlot);
    }

    // Gene Splicer
    private void OnGeneSplicerInit(EntityUid uid, GeneSplicerComponent comp, ComponentInit args)
    {
        _itemSlots.AddItemSlot(uid, GeneSplicerComponent.DiskSlotAId, comp.DiskSlotA);
        _itemSlots.AddItemSlot(uid, GeneSplicerComponent.DiskSlotBId, comp.DiskSlotB);
    }

    private void OnGeneSplicerRemove(EntityUid uid, GeneSplicerComponent comp, ComponentRemove args)
    {
        _itemSlots.RemoveItemSlot(uid, comp.DiskSlotA);
        _itemSlots.RemoveItemSlot(uid, comp.DiskSlotB);
    }

    // Plant Centrifuge
    private void OnPlantCentrifugeInit(EntityUid uid, PlantCentrifugeComponent comp, ComponentInit args)
    {
        _itemSlots.AddItemSlot(uid, PlantCentrifugeComponent.ProduceSlotId, comp.ProduceSlot);
    }

    private void OnPlantCentrifugeRemove(EntityUid uid, PlantCentrifugeComponent comp, ComponentRemove args)
    {
        _itemSlots.RemoveItemSlot(uid, comp.ProduceSlot);
    }
}
