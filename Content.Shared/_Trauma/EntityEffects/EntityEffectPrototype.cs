// SPDX-License-Identifier: AGPL-3.0-or-later
// Ported from Trauma-Station

using Content.Shared.EntityConditions;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.EntityEffects;

/// <summary>
/// A prototype for entity effects which can be reused via <see cref="NestedEffect"/>.
/// </summary>
[Prototype("entityEffect")]
public sealed partial class EntityEffectPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = string.Empty;

    /// <summary>
    /// The effects of this prototype.
    /// </summary>
    [DataField(required: true)]
    public EntityEffect[] Effects = default!;

    /// <summary>
    /// Conditions checked for this effect, regardless of the <see cref="NestedEffect"/> using it.
    /// Currently not included in the guidebook text!
    /// </summary>
    [DataField]
    public EntityCondition[]? Conditions;

    /// <summary>
    /// An override for the effect guidebook text, has "chance" passed from 0 to 1.
    /// By default one is generated from each effect.
    /// </summary>
    [DataField]
    public LocId? GuidebookText;
}
