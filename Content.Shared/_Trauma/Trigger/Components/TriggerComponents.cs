// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.EntityEffects;
using Content.Shared.Trigger.Components.Effects;

namespace Content.Shared._Trauma.Trigger.Components;

/// <summary>
/// Applies a list of entity effects to the owning entity when triggered.
/// If TargetUser is true then they will be applied to the user instead.
/// </summary>
[RegisterComponent, AutoGenerateComponentState]
public sealed partial class EntityEffectOnTriggerComponent : BaseXOnTriggerComponent
{
    /// <summary>
    /// The effects to apply.
    /// </summary>
    [DataField]
    public EntityEffect[] Effects = Array.Empty<EntityEffect>();

    /// <summary>
    /// Optional scale multiplier for the effects.
    /// </summary>
    [DataField]
    public float Scale = 1f;
}
