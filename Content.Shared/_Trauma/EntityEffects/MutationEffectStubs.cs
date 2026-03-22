// SPDX-License-Identifier: AGPL-3.0-or-later
// Stub EntityEffect classes ported from Trauma-Station for YAML deserialization.
// These allow the entityEffect YAML prototypes to load. Full implementations
// are in the Trauma-Station genetics/medical systems.

using Content.Shared.EntityEffects;
using Content.Shared.EntityEffects.Effects.StatusEffects;
using Content.Shared.Whitelist;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// Makes the target drop the items they are holding.
/// </summary>
public sealed partial class DropItems : EntityEffectBase<DropItems>
{
    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Makes the target drop a random item, then run entity effects on it.
/// </summary>
public sealed partial class DropRandomItem : EntityEffectBase<DropRandomItem>
{
    [DataField]
    public EntityEffect[] Effects = [];

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Applies fire stacks to the target entity.
/// </summary>
public sealed partial class Flammable : EntityEffectBase<Flammable>
{
    [DataField]
    public float Multiplier = 0.05f;

    [DataField]
    public float? MultiplierOnExisting;

    [DataField]
    public float FireProtectionPenetration;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Gibs the target entity.
/// </summary>
public sealed partial class Gib : EntityEffectBase<Gib>
{
    [DataField]
    public bool DropGiblets = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Knocks down the target entity.
/// </summary>
public sealed partial class Knockdown : EntityEffectBase<Knockdown>
{
    [DataField]
    public float Time = 2f;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Base class for status effect entity effects with time/type fields.
/// </summary>
public abstract partial class BaseStatusEntityEffect<T> : EntityEffectBase<T> where T : BaseStatusEntityEffect<T>
{
    [DataField]
    public TimeSpan? Time = TimeSpan.FromSeconds(2);

    [DataField]
    public StatusEffectMetabolismType Type = StatusEffectMetabolismType.Update;

}

/// <summary>
/// Applies knockdown status effect to the target.
/// </summary>
public sealed partial class ModifyKnockdown : BaseStatusEntityEffect<ModifyKnockdown>
{
    [DataField]
    public bool Crawling;

    [DataField]
    public bool Drop = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Applies paralysis status effect to the target.
/// </summary>
public sealed partial class ModifyParalysis : BaseStatusEntityEffect<ModifyParalysis>
{
    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Does nothing. Used as a weighted random option.
/// </summary>
public sealed partial class NoEffect : EntityEffectBase<NoEffect>
{
    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Plays a sound effect at the target entity.
/// </summary>
public sealed partial class PlaySoundEffect : EntityEffectBase<PlaySoundEffect>
{
    [DataField(required: true)]
    public SoundSpecifier Sound = default!;

    [DataField]
    public bool Positional = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Spawns an entity at the target's location.
/// </summary>
public sealed partial class SpawnEntity : EntityEffectBase<SpawnEntity>
{
    [DataField]
    public int Number = 1;

    [DataField(required: true)]
    public EntProtoId Entity;

    [DataField]
    public bool Predicted = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Starts a use delay on the target entity.
/// </summary>
public sealed partial class StartUseDelay : EntityEffectBase<StartUseDelay>
{
    [DataField]
    public string DelayId = "default";

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Throws the target entity in a random direction.
/// </summary>
public sealed partial class ThrowRandomly : EntityEffectBase<ThrowRandomly>
{
    [DataField]
    public float Speed = 10f;

    [DataField]
    public bool Predicted = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Sets the target entity's standing state.
/// </summary>
public sealed partial class SetStanding : EntityEffectBase<SetStanding>
{
    [DataField]
    public bool Standing = true;

    [DataField]
    public bool Force = false;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Makes the target entity attack a random mob nearby.
/// </summary>
public sealed partial class AttackOthers : EntityEffectBase<AttackOthers>
{
    [DataField]
    public bool UseHeld = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Makes the target entity melee attack itself.
/// </summary>
public sealed partial class AttackSelf : EntityEffectBase<AttackSelf>
{
    [DataField]
    public bool UseHeld = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Adds a metabolizer type to the target organ entity.
/// </summary>
public sealed partial class AddMetabolizerType : EntityEffectBase<AddMetabolizerType>
{
    [DataField(required: true)]
    public string Type = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Adds an organ/bodypart slot to a body part entity.
/// </summary>
public sealed partial class AddOrganSlot : EntityEffectBase<AddOrganSlot>
{
    [DataField(required: true)]
    public string Category = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Spawns and inserts an organ into a body part entity.
/// </summary>
public sealed partial class InsertNewOrgan : EntityEffectBase<InsertNewOrgan>
{
    [DataField(required: true)]
    public EntProtoId Organ = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Moves an organ from one body part to another.
/// </summary>
public sealed partial class MoveOrgan : EntityEffectBase<MoveOrgan>
{
    [DataField(required: true)]
    public string Organ = string.Empty;

    [DataField(required: true)]
    public string Dest = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Regenerates an organ from initial body organs.
/// </summary>
public sealed partial class RegenerateOrgan : EntityEffectBase<RegenerateOrgan>
{
    [DataField(required: true)]
    public string Slot = string.Empty;

    [DataField]
    public bool Recursive = true;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Relays effects to nearby entities matching conditions.
/// </summary>
public sealed partial class RelayNearby : EntityEffectBase<RelayNearby>
{
    [DataField]
    public EntityEffect Effect = default!;

    [DataField(required: true)]
    public string CompName = string.Empty;

    [DataField]
    public float Range = 5f;

    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField]
    public EntityWhitelist? Blacklist;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Applies effects to internal organs matching a whitelist.
/// </summary>
public sealed partial class RelayOrgans : EntityEffectBase<RelayOrgans>
{
    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField(required: true)]
    public EntityEffect[] Effects = default!;

    [DataField]
    public LocId? GuidebookText;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Removes a metabolizer type from the target organ entity.
/// </summary>
public sealed partial class RemoveMetabolizerType : EntityEffectBase<RemoveMetabolizerType>
{
    [DataField(required: true)]
    public string Type = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Removes an organ slot from a body part entity.
/// </summary>
public sealed partial class RemoveOrganSlot : EntityEffectBase<RemoveOrganSlot>
{
    [DataField(required: true)]
    public string Slot = string.Empty;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}

/// <summary>
/// Reverts the target entity's polymorph.
/// </summary>
public sealed partial class RevertPolymorph : EntityEffectBase<RevertPolymorph>
{
    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => null;
}
