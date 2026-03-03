// SPDX-FileCopyrightText: 2025 space-wizards contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server._Wizden.Power.Components;
using Content.Server.Power.Components;
using Content.Shared.Power;
using Content.Shared.Verbs;

namespace Content.Server._Wizden.Power.EntitySystems;

public sealed class VoltageTogglerSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<VoltageTogglerComponent, GetVerbsEvent<Verb>>(OnGetVerb);
    }

    private void OnGetVerb(Entity<VoltageTogglerComponent> entity, ref GetVerbsEvent<Verb> args)
    {
        if (!args.CanAccess || !args.CanInteract)
            return;

        var index = 0;
        foreach (var setting in entity.Comp.Settings)
        {
            // This is because Act wont work with index.
            // Needs it to be saved in the loop.
            var currIndex = index;
            var verb = new Verb
            {
                Priority = currIndex,
                Category = VerbCategory.VoltageLevel,
                Disabled = entity.Comp.SelectedVoltageLevel == currIndex,
                Text = Loc.GetString(setting.Name),
                Act = () =>
                {
                    entity.Comp.SelectedVoltageLevel = currIndex;
                    Dirty(entity);

                    ChangeVoltage(entity, setting);
                }
            };
            args.Verbs.Add(verb);
            index++;
        }
    }

    private void ChangeVoltage(Entity<VoltageTogglerComponent> entity, VoltageSetting setting)
    {
        // Note: Hispania's Node.NodeGroupID has a private setter,
        // so we skip runtime node group switching. The power consumer
        // voltage and draw rate changes still apply.
        if (TryComp<PowerConsumerComponent>(entity, out var powerConsumerComp))
        {
            powerConsumerComp.Voltage = setting.Voltage;
            powerConsumerComp.DrawRate = setting.Wattage;
        }
    }
}
