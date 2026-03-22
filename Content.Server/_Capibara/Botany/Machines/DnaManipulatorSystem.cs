// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany;
using Content.Server.Botany.Components;
using Content.Server.Botany.Systems;
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

namespace Content.Server._Capibara.Botany.Machines;

public sealed class DnaManipulatorSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
    [Dependency] private readonly BotanySystem _botanySystem = default!;
    [Dependency] private readonly ItemSlotsSystem _itemSlots = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly PlantGenomeSystem _genomeSystem = default!;
    [Dependency] private readonly GeneDiskSystem _geneDiskSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        Subs.BuiEvents<DnaManipulatorComponent>(DnaManipulatorUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnUiOpened);
            subs.Event<DnaManipulatorExtractMessage>(OnExtractGene);
            subs.Event<DnaManipulatorInsertMessage>(OnInsertGene);
            subs.Event<DnaManipulatorEjectSeedMessage>(OnEjectSeed);
            subs.Event<DnaManipulatorEjectDiskMessage>(OnEjectDisk);
        });

        SubscribeLocalEvent<DnaManipulatorComponent, EntInsertedIntoContainerMessage>(OnItemChanged);
        SubscribeLocalEvent<DnaManipulatorComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
        SubscribeLocalEvent<DnaManipulatorComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, DnaManipulatorComponent comp, InteractUsingEvent args)
    {
        if (args.Handled || !this.IsPowered(uid, EntityManager))
            return;

        // Try seed slot first, then disk slot
        if (HasComp<SeedComponent>(args.Used) && !comp.SeedSlot.HasItem)
        {
            if (_itemSlots.TryInsertFromHand(uid, comp.SeedSlot, args.User))
            {
                args.Handled = true;
                UpdateUiState(uid, comp);
            }
        }
        else if (HasComp<GeneDiskComponent>(args.Used) && !comp.DiskSlot.HasItem)
        {
            if (_itemSlots.TryInsertFromHand(uid, comp.DiskSlot, args.User))
            {
                args.Handled = true;
                UpdateUiState(uid, comp);
            }
        }
    }

    private void OnItemChanged(EntityUid uid, DnaManipulatorComponent comp, EntInsertedIntoContainerMessage args)
    {
        UpdateUiState(uid, comp);
    }

    private void OnItemRemoved(EntityUid uid, DnaManipulatorComponent comp, EntRemovedFromContainerMessage args)
    {
        UpdateUiState(uid, comp);
    }

    private void OnUiOpened(EntityUid uid, DnaManipulatorComponent comp, BoundUIOpenedEvent args)
    {
        UpdateUiState(uid, comp);
    }

    private void OnEjectSeed(EntityUid uid, DnaManipulatorComponent comp, DnaManipulatorEjectSeedMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.SeedSlot, args.Actor);
        UpdateUiState(uid, comp);
    }

    private void OnEjectDisk(EntityUid uid, DnaManipulatorComponent comp, DnaManipulatorEjectDiskMessage args)
    {
        _itemSlots.TryEjectToHands(uid, comp.DiskSlot, args.Actor);
        UpdateUiState(uid, comp);
    }

    private void OnExtractGene(EntityUid uid, DnaManipulatorComponent comp, DnaManipulatorExtractMessage args)
    {
        if (!this.IsPowered(uid, EntityManager))
            return;

        // Need a seed with genome and an empty disk
        if (comp.SeedSlot.Item is not { } seedItem)
            return;
        if (comp.DiskSlot.Item is not { } diskItem)
            return;
        if (!TryComp<GeneDiskComponent>(diskItem, out var disk))
            return;
        if (disk.StoredGene != null)
        {
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-disk-full"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }

        // Get genome from seed
        if (!TryComp<PlantGenomeComponent>(seedItem, out var genome) || !genome.Initialized)
        {
            // Initialize genome if seed doesn't have one yet
            if (!TryComp<SeedComponent>(seedItem, out var seedComp))
                return;
            if (!_botanySystem.TryGetSeed(seedComp, out var seedData))
                return;

            genome = EnsureComp<PlantGenomeComponent>(seedItem);
            InitializeGenomeFromSeed(genome, seedData);
        }

        var slotIndex = args.SlotIndex;
        if (slotIndex < 0 || slotIndex >= genome.GeneSlots.Count)
            return;

        var slot = genome.GeneSlots[slotIndex];
        if (slot.Gene == null)
        {
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-slot-empty"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }
        if (slot.Locked)
        {
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-slot-locked"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }

        // Extract gene to disk
        var geneId = slot.Gene.Value;
        disk.StoredGene = geneId;
        Dirty(diskItem, disk);
        _geneDiskSystem.UpdateDiskName(diskItem, disk);

        // Degrade disk integrity for Rare/Legendary genes
        if (_protoManager.TryIndex(geneId, out var geneProto))
        {
            if (geneProto.Rarity == PlantGeneRarity.Rare)
                disk.Integrity -= 20f;
            else if (geneProto.Rarity == PlantGeneRarity.Legendary)
                disk.Integrity -= 40f;
        }

        // Destroy disk if integrity hit 0
        if (disk.Integrity <= 0)
        {
            QueueDel(diskItem);
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-disk-destroyed"), uid, args.Actor, PopupType.LargeCaution);
        }

        // Destroy the seed packet (gene extraction consumes it)
        _itemSlots.TryEject(uid, comp.SeedSlot, null, out _);
        QueueDel(seedItem);

        _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-extract-success"), uid, args.Actor, PopupType.Medium);
        UpdateUiState(uid, comp);
    }

    private void OnInsertGene(EntityUid uid, DnaManipulatorComponent comp, DnaManipulatorInsertMessage args)
    {
        if (!this.IsPowered(uid, EntityManager))
            return;

        if (comp.SeedSlot.Item is not { } seedItem)
            return;
        if (comp.DiskSlot.Item is not { } diskItem)
            return;
        if (!TryComp<GeneDiskComponent>(diskItem, out var disk) || disk.StoredGene == null)
        {
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-disk-empty"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }

        // Get or init genome on seed
        if (!TryComp<PlantGenomeComponent>(seedItem, out var genome) || !genome.Initialized)
        {
            if (!TryComp<SeedComponent>(seedItem, out var seedComp))
                return;
            if (!_botanySystem.TryGetSeed(seedComp, out var seedData))
                return;

            genome = EnsureComp<PlantGenomeComponent>(seedItem);
            InitializeGenomeFromSeed(genome, seedData);
        }

        var geneId = disk.StoredGene.Value;

        // Check incompatibilities and duplicates
        if (_protoManager.TryIndex(geneId, out var geneProto))
        {
            foreach (var existing in genome.GeneSlots)
            {
                if (existing.Gene == null)
                    continue;

                // Duplicate check
                if (existing.Gene.Value == geneId)
                {
                    _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-duplicate"), uid, args.Actor, PopupType.MediumCaution);
                    return;
                }

                if (geneProto.Incompatible.Contains(existing.Gene.Value))
                {
                    _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-incompatible"), uid, args.Actor, PopupType.MediumCaution);
                    return;
                }
            }
        }

        // Find first empty unlocked slot
        var inserted = false;
        for (var i = 0; i < genome.GeneSlots.Count; i++)
        {
            var slot = genome.GeneSlots[i];
            if (slot.Gene == null && !slot.Locked)
            {
                genome.GeneSlots[i] = new PlantGeneSlot { Gene = geneId, Locked = false };
                inserted = true;
                break;
            }
        }

        if (!inserted)
        {
            _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-no-slots"), uid, args.Actor, PopupType.MediumCaution);
            return;
        }

        // Clear disk
        disk.StoredGene = null;
        Dirty(diskItem, disk);
        _geneDiskSystem.UpdateDiskName(diskItem, disk);

        _genomeSystem.RecalculateInstability(seedItem, genome);
        Dirty(seedItem, genome);

        _popup.PopupEntity(Loc.GetString("capibara-dna-manipulator-insert-success"), uid, args.Actor, PopupType.Medium);
        UpdateUiState(uid, comp);
    }

    private void InitializeGenomeFromSeed(PlantGenomeComponent genome, SeedData seed)
    {
        genome.CoreSpeciesId = seed.Name;
        genome.Instability = 0f;
        genome.Epigenetics.Clear();
        genome.GeneSlots.Clear();
        for (var i = 0; i < genome.MaxSlots; i++)
        {
            genome.GeneSlots.Add(new PlantGeneSlot { Gene = null, Locked = i >= 6 });
        }
        _genomeSystem.CaptureBaseStats(genome, seed);
        genome.Initialized = true;
    }

    private void UpdateUiState(EntityUid uid, DnaManipulatorComponent comp)
    {
        var state = new DnaManipulatorBuiState();

        // Seed info
        if (comp.SeedSlot.Item is { } seedItem && TryComp<SeedComponent>(seedItem, out var seedComp))
        {
            if (_botanySystem.TryGetSeed(seedComp, out var seed))
            {
                state.HasSeed = true;
                state.SeedSpeciesName = Loc.GetString(seed.DisplayName);

                if (TryComp<PlantGenomeComponent>(seedItem, out var genome) && genome.Initialized)
                {
                    state.Instability = genome.Instability;
                    foreach (var slot in genome.GeneSlots)
                    {
                        if (slot.Gene != null && _protoManager.TryIndex(slot.Gene.Value, out var gp))
                        {
                            state.GeneSlots.Add(new GeneSlotData(
                                slot.Gene, Loc.GetString(gp.Name), Loc.GetString(gp.Description),
                                gp.Rarity, slot.Locked));
                        }
                        else
                        {
                            state.GeneSlots.Add(new GeneSlotData(null, null, null, null, slot.Locked));
                        }
                    }
                }
                else
                {
                    // Show default empty slots for seeds without genome
                    for (var i = 0; i < 6; i++)
                        state.GeneSlots.Add(new GeneSlotData(null, null, null, null, false));
                }
            }
        }

        // Disk info
        if (comp.DiskSlot.Item is { } diskItem && TryComp<GeneDiskComponent>(diskItem, out var disk))
        {
            state.HasDisk = true;
            state.DiskIntegrity = disk.Integrity;
            if (disk.StoredGene != null && _protoManager.TryIndex(disk.StoredGene.Value, out var geneProto))
            {
                state.DiskHasGene = true;
                state.DiskGeneName = Loc.GetString(geneProto.Name);
                state.DiskGeneRarity = geneProto.Rarity;
            }
        }

        _uiSystem.SetUiState(uid, DnaManipulatorUiKey.Key, state);
    }
}
