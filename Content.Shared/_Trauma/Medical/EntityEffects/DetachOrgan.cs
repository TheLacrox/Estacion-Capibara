// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Organ;
using Content.Shared.Body.Systems;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.Medical.EntityEffects;

/// <summary>
/// Detaches this target organ from its parent part.
/// </summary>
public sealed partial class DetachOrgan : EntityEffectBase<DetachOrgan>
{
    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("entity-effect-guidebook-detach-part", ("chance", Probability));
}

public sealed class DetachOrganEffectSystem : EntityEffectSystem<OrganComponent, DetachOrgan>
{
    [Dependency] private readonly SharedBodySystem _body = default!;

    protected override void Effect(Entity<OrganComponent> ent, ref EntityEffectEvent<DetachOrgan> args)
    {
        _body.RemoveOrgan(ent);
    }
}
