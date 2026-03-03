// SPDX-FileCopyrightText: 2025 space-wizards contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server._Wizden.Antag.Components;
using Content.Server.Antag;
using Robust.Shared.Random;

namespace Content.Server._Wizden.Antag;

public sealed class AntagMultipleRoleSpawnerSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly ILogManager _log = default!;

    private ISawmill _sawmill = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AntagMultipleRoleSpawnerComponent, AntagSelectEntityEvent>(OnSelectEntity);

        _sawmill = _log.GetSawmill("antag_multiple_spawner");
    }

    private void OnSelectEntity(Entity<AntagMultipleRoleSpawnerComponent> ent, ref AntagSelectEntityEvent args)
    {
        // In Hispania, AntagSelectEntityEvent doesn't have AntagRoles.
        // We get the role from the game rule's definitions instead.
        var defs = args.GameRule.Comp.Definitions;
        if (defs.Count == 0)
            return;

        // Use the first definition's first PrefRole as the antag role key.
        if (defs[0].PrefRoles.Count == 0)
            return;

        var role = defs[0].PrefRoles[0];

        if (!ent.Comp.AntagRoleToPrototypes.TryGetValue(role, out var entProtos) || entProtos.Count == 0)
            return;

        args.Entity = Spawn(ent.Comp.PickAndTake ? _random.PickAndTake(entProtos) : _random.Pick(entProtos));
    }
}
