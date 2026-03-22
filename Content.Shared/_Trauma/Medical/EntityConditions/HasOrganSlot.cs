// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body;
using Content.Shared.Body.Components;
using Content.Shared.Body.Part;
using Content.Shared.Body.Systems;
using Content.Shared.EntityConditions;
using Robust.Shared.Prototypes;

namespace Content.Shared._Trauma.Medical.EntityConditions;

/// <summary>
/// Requires that the target mob has an organ slot in a body part.
/// </summary>
public sealed partial class HasOrganSlot : EntityConditionBase<HasOrganSlot>
{
    /// <summary>
    /// Organ slot ID that must exist in a found body part.
    /// </summary>
    [DataField(required: true)]
    public ProtoId<OrganCategoryPrototype> Organ;

    [DataField(required: true)]
    public BodyPartType PartType;

    [DataField]
    public BodyPartSymmetry? Symmetry;

    public override string EntityConditionGuidebookText(IPrototypeManager prototype)
        => Loc.GetString("entity-condition-guidebook-organ-slot", ("inverted", Inverted), ("part", PartType), ("slot", Organ));
}

public sealed class HasOrganSlotConditionSystem : EntityConditionSystem<BodyComponent, HasOrganSlot>
{
    [Dependency] private readonly SharedBodySystem _body = default!;

    protected override void Condition(Entity<BodyComponent> ent, ref EntityConditionEvent<HasOrganSlot> args)
    {
        var slot = args.Condition.Organ;
        var partType = args.Condition.PartType;
        var symmetry = args.Condition.Symmetry;

        foreach (var (partUid, partComp) in _body.GetBodyChildrenOfType(ent, partType, ent.Comp, symmetry))
        {
            // Check if this body part has an organ with the requested category
            foreach (var (organUid, organComp) in _body.GetPartOrgans(partUid, partComp))
            {
                if (organComp.Category == slot)
                {
                    args.Result = true;
                    return;
                }
            }
        }
    }
}
