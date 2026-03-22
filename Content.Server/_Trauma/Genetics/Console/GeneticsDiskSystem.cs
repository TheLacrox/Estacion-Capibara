// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Kitchen.Components;
using Content.Shared._Trauma.Genetics.Console;

namespace Content.Server._Trauma.Genetics.Console;

/// <summary>
/// Server-side handler for genetics disk events (microwave wiping).
/// </summary>
public sealed class GeneticsDiskServerSystem : EntitySystem
{
    [Dependency] private readonly GeneticsDiskSystem _disk = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GeneticsDiskComponent, BeingMicrowavedEvent>(OnMicrowaved);
    }

    private void OnMicrowaved(Entity<GeneticsDiskComponent> ent, ref BeingMicrowavedEvent args)
    {
        _disk.SetMutation(ent, null);
        _disk.SetEnzymes(ent, null);
    }
}
