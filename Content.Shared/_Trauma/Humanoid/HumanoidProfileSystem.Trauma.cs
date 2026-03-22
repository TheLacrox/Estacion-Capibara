// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Common.Barks;
using Content.Shared.Body;
using Content.Shared.Body.Components;
using Content.Shared.Body.Organ;
using Content.Shared.Body.Systems;
using Content.Shared.DetailExaminable;
using Content.Shared.Humanoid.Markings;
using Content.Shared.Preferences;
using Robust.Shared.Enums;
using Robust.Shared.GameObjects.Components.Localization;
using Robust.Shared.Prototypes;

namespace Content.Shared.Humanoid;

/// <summary>
/// Trauma - barks stuff and "api" for humanoid
/// </summary>
public sealed class HumanoidProfileSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly SharedBodySystem _body = default!;
    [Dependency] private readonly SharedVisualBodySystem _visualBody = default!;
    [Dependency] private readonly GrammarSystem _grammar = default!;

    public static readonly ProtoId<BarkPrototype> DefaultBarkVoice = "Alto";

    public static readonly ProtoId<OrganCategoryPrototype> EyesCategory = "Eyes";
    public static readonly ProtoId<OrganCategoryPrototype> TorsoCategory = "Torso";

    /// <summary>
    /// Organ visual layers needed for eye and skin color.
    /// </summary>
    public static readonly HashSet<HumanoidVisualLayers> CoreLayers = new()
    {
        HumanoidVisualLayers.Eyes,
        HumanoidVisualLayers.Chest
    };

    public void SetBarkVoice(Entity<HumanoidAppearanceComponent> ent, [ForbidLiteral] ProtoId<BarkPrototype>? barkvoiceId)
    {
        var voicePrototypeId = DefaultBarkVoice;
        var species = ent.Comp.Species;
        if (barkvoiceId != null &&
            _prototype.TryIndex(barkvoiceId, out var bark) &&
            bark.SpeciesWhitelist?.Contains(species) != false)
        {
            voicePrototypeId = barkvoiceId.Value;
        }
        else
        {
            // use first valid bark as a fallback
            foreach (var o in _prototype.EnumeratePrototypes<BarkPrototype>())
            {
                if (o.RoundStart && o.SpeciesWhitelist?.Contains(species) != false)
                {
                    voicePrototypeId = o.ID;
                    break;
                }
            }
        }

        var comp = EnsureComp<SpeechSynthesisComponent>(ent);
        comp.VoicePrototypeId = voicePrototypeId;
        Dirty(ent, comp);
        ent.Comp.BarkVoice = voicePrototypeId;
    }

    /// <summary>
    /// Gets the eye color from a set of organ visual data, or null if it has no eyes.
    /// </summary>
    public Color? GetEyeColor(Dictionary<ProtoId<OrganCategoryPrototype>, OrganProfileData>? organs)
        => organs?.TryGetValue(EyesCategory, out var eye) == true ? eye.EyeColor : null;

    /// <summary>
    /// Gets the skin color from a set of organ visual data, or null if it has no torso. (Should never happen)
    /// </summary>
    public Color? GetSkinColor(Dictionary<ProtoId<OrganCategoryPrototype>, OrganProfileData>? organs)
        => organs?.TryGetValue(TorsoCategory, out var torso) == true ? torso.SkinColor : null;

    /// <summary>
    /// Get the visual data you need for <see cref="GetEyeColor"/> and <see cref="GetSkinColor"/>.
    /// </summary>
    public Dictionary<ProtoId<OrganCategoryPrototype>, OrganProfileData>? GetOrgansData(EntityUid mob)
    {
        _visualBody.TryGatherMarkingsData(mob, CoreLayers, out var organs, out _, out _);
        return organs;
    }

    /// <summary>
    /// Gets an organ entity from a body by its category.
    /// </summary>
    private EntityUid? GetOrganByCategory(EntityUid mob, ProtoId<OrganCategoryPrototype> category)
    {
        foreach (var (organUid, organComp) in _body.GetBodyOrgans(mob))
        {
            if (organComp.Category == category)
                return organUid;
        }

        return null;
    }

    public void SetEyeColor(EntityUid mob, Color color)
    {
        if (!TryComp<BodyComponent>(mob, out var body) ||
            GetOrganByCategory(mob, EyesCategory) is not {} eyes ||
            !TryComp<VisualOrganComponent>(eyes, out var visual) ||
            visual.Profile.EyeColor == color)
            return;

        // Raise the event so it applies organ color etc. automatically
        var profile = visual.Profile;
        profile.EyeColor = color;
        var ev = new BodyRelayedEvent<ApplyOrganProfileDataEvent>((mob, body), new ApplyOrganProfileDataEvent(profile, null));
        RaiseLocalEvent(eyes, ref ev);
    }

    public void SetSkinColor(EntityUid mob, Color color, Color? eyeColor = null)
    {
        if (!TryComp<HumanoidAppearanceComponent>(mob, out var comp))
            return;

        _visualBody.ApplyProfile(mob, new()
        {
            Sex = comp.Sex,
            SkinColor = color,
            EyeColor = eyeColor ?? GetEyeColor(GetOrgansData(mob)) ?? Color.Black
        });
        // TODO: fix upstream thing for marking skin-matched colors
    }

    // REMOVED THE ENTIRE API AWARD!!!
    public void SetSex(Entity<HumanoidAppearanceComponent> ent, Sex sex)
    {
        var old = ent.Comp.Sex;
        if (old == sex)
            return;

        ent.Comp.Sex = sex;
        Dirty(ent);
        var ev = new SexChangedEvent(old, sex);
        RaiseLocalEvent(ent, ev);
    }

    public void SetGender(Entity<HumanoidAppearanceComponent> ent, Gender gender)
    {
        if (ent.Comp.Gender == gender)
            return;

        ent.Comp.Gender = gender;
        Dirty(ent);

        if (TryComp<GrammarComponent>(ent, out var grammar))
            _grammar.SetGender((ent, grammar), gender);
    }
}
