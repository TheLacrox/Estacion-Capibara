// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.TTS;
using Content.Shared._Capibara.TTS.Prototypes;
using System.Linq;

namespace Content.Client.Lobby.UI;

public sealed partial class HumanoidProfileEditor
{
    private List<VoicePrototype> _ttsVoicePrototypes = new();

    private void InitializeTTSVoice()
    {
        TTSVoiceButton.OnItemSelected += args =>
        {
            TTSVoiceButton.SelectId(args.Id);
            SetTTSVoice(_ttsVoicePrototypes[args.Id]);
        };

        TTSVoicePlayButton.OnPressed += _ => PlayPreviewTTS();
    }

    private void UpdateTTSVoice()
    {
        if (Profile is null)
            return;

        if (!TTSVoiceContainer.Visible)
            return;

        _ttsVoicePrototypes = _prototypeManager
            .EnumeratePrototypes<VoicePrototype>()
            .Where(o => !o.Silicon)
            .OrderBy(o => o.Name)
            .ToList();

        TTSVoiceButton.Clear();

        var selectedId = -1;
        for (var i = 0; i < _ttsVoicePrototypes.Count; i++)
        {
            var voice = _ttsVoicePrototypes[i];
            if (Profile.TTSVoice != null && voice.ID == Profile.TTSVoice.Value)
                selectedId = i;

            TTSVoiceButton.AddItem(voice.Name, i);
        }

        if (selectedId == -1 && _ttsVoicePrototypes.Count > 0)
            selectedId = 0;

        if (selectedId >= 0)
        {
            TTSVoiceButton.SelectId(selectedId);
            SetTTSVoice(_ttsVoicePrototypes[selectedId]);
        }
    }

    private void SetTTSVoice(VoicePrototype voice)
    {
        Profile = Profile?.WithTTSVoice(voice);
        IsDirty = true;
    }

    private void PlayPreviewTTS()
    {
        if (Profile?.TTSVoice == null)
            return;

        var ev = new PreviewTTSRequestEvent(Profile.TTSVoice.Value);
        _entManager.System<Content.Client._Capibara.TTS.TTSPreviewSystem>().SendPreview(ev);
    }
}
