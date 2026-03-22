// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.Botany.Components;
using Content.Shared._Capibara.Botany.Genes;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._Capibara.Botany.Machines;

/// <summary>
/// Handles gene disk initialization (prefilled disks with random genes by rarity)
/// and updating the disk's entity name to show the stored gene.
/// </summary>
public sealed class GeneDiskSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly MetaDataSystem _meta = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneDiskComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(EntityUid uid, GeneDiskComponent comp, MapInitEvent args)
    {
        // If this disk has a prefilled rarity, pick a random gene of that rarity
        if (comp.PrefilledRarity is { } rarity)
        {
            var candidates = new List<PlantGenePrototype>();
            foreach (var proto in _protoManager.EnumeratePrototypes<PlantGenePrototype>())
            {
                if (proto.Rarity == rarity)
                    candidates.Add(proto);
            }

            if (candidates.Count > 0)
            {
                var chosen = _random.Pick(candidates);
                comp.StoredGene = chosen.ID;
                Dirty(uid, comp);
            }
        }

        // Always update name after MapInit (gene may have been set from prefill or data)
        if (comp.StoredGene != null)
            UpdateDiskName(uid, comp);
    }

    /// <summary>
    /// Updates the disk's entity name to show the stored gene name.
    /// Call this after changing StoredGene on any disk.
    /// </summary>
    public void UpdateDiskName(EntityUid uid, GeneDiskComponent? comp = null)
    {
        if (!Resolve(uid, ref comp))
            return;

        if (comp.StoredGene != null && _protoManager.TryIndex(comp.StoredGene.Value, out var geneProto))
        {
            var geneName = Loc.GetString(geneProto.Name);
            _meta.SetEntityName(uid, Loc.GetString("capibara-gene-disk-base-name") + " [" + geneName + "]");
        }
        else
        {
            _meta.SetEntityName(uid, Loc.GetString("capibara-gene-disk-base-name"));
        }
    }
}
