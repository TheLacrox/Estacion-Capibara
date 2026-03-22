using Content.Shared.Humanoid;
using Content.Shared.Preferences;

namespace Content.Shared.Body;

public abstract partial class SharedVisualBodySystem
{
    private void InitializeInitial()
    {
        SubscribeLocalEvent<VisualBodyComponent, MapInitEvent>(OnVisualMapInit);
    }

    private void OnVisualMapInit(Entity<VisualBodyComponent> ent, ref MapInitEvent args)
    {
        if (!TryComp<HumanoidAppearanceComponent>(ent, out var humanoid))
            return;

        var profile = HumanoidCharacterProfile.DefaultWithSpecies(humanoid.Species);
        ApplyAppearanceTo(ent.AsNullable(), profile.Appearance, humanoid.Sex);
    }
}
