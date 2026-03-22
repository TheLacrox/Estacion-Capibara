// SPDX-FileCopyrightText: 2022 0x6273 <0x40@keemail.me>
// SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 Kevin Zheng <kevinz5000@gmail.com>
// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 wrexbe <wrexbe@protonmail.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 MossyGreySlope <mossygreyslope@gmail.com>
// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 drakewill-CRL <46307022+drakewill-CRL@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Botany.Components;
using Content.Server.Popups;
using Content.Server.Power.EntitySystems;
using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Robust.Shared.Random;

namespace Content.Server.Botany.Systems;

public sealed class SeedExtractorSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly PopupSystem _popupSystem = default!;
    [Dependency] private readonly BotanySystem _botanySystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<SeedExtractorComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, SeedExtractorComponent seedExtractor, InteractUsingEvent args)
    {
        if (!this.IsPowered(uid, EntityManager))
            return;

        if (!TryComp(args.Used, out ProduceComponent? produce))
            return;
        if (!_botanySystem.TryGetSeed(produce, out var seed) || seed.Seedless)
        {
            _popupSystem.PopupCursor(Loc.GetString("seed-extractor-component-no-seeds", ("name", args.Used)),
                args.User, PopupType.MediumCaution);
            return;
        }

        _popupSystem.PopupCursor(Loc.GetString("seed-extractor-component-interact-message", ("name", args.Used)),
            args.User, PopupType.Medium);

        // Capibara - capture produce genome data before deleting
        var produceGenome = CompOrNull<PlantGenomeComponent>(args.Used);
        var produceGenes = CompOrNull<GeneModifiedProduceComponent>(args.Used);

        QueueDel(args.Used);
        args.Handled = true;

        var amount = _random.Next(seedExtractor.BaseMinSeeds, seedExtractor.BaseMaxSeeds + 1);
        var coords = Transform(uid).Coordinates;

        var packetSeed = seed;
        if (amount > 1)
            packetSeed.Unique = false;

        for (var i = 0; i < amount; i++)
        {
            var seedPacket = _botanySystem.SpawnSeedPacket(packetSeed, coords, args.User);

            // Capibara - Transfer genome from produce to seed packet
            if (produceGenome != null && produceGenome.Initialized)
            {
                var targetGenome = EnsureComp<PlantGenomeComponent>(seedPacket);
                targetGenome.CoreSpeciesId = produceGenome.CoreSpeciesId;
                targetGenome.MaxSlots = produceGenome.MaxSlots;
                targetGenome.Instability = produceGenome.Instability;
                targetGenome.Initialized = true;
                targetGenome.GeneSlots.Clear();
                foreach (var slot in produceGenome.GeneSlots)
                {
                    targetGenome.GeneSlots.Add(new Content.Shared._Capibara.Botany.Genes.PlantGeneSlot
                    {
                        Gene = slot.Gene,
                        Locked = slot.Locked,
                    });
                }
                targetGenome.Epigenetics.Clear();
                targetGenome.BaseStatSnapshot = new Dictionary<string, float>(produceGenome.BaseStatSnapshot);
                targetGenome.BaseBoolSnapshot = new Dictionary<string, bool>(produceGenome.BaseBoolSnapshot);
                targetGenome.BaseChemSnapshot = new Dictionary<string, (int, int, int)>(produceGenome.BaseChemSnapshot);
                targetGenome.BaseHarvestType = produceGenome.BaseHarvestType;
                Dirty(seedPacket, targetGenome);
            }
        }
    }
}