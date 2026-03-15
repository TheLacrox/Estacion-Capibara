// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.RegularExpressions;
using Content.Server.Chat.Systems;
using Content.Server._EinsteinEngines.Language;
using Content.Shared.GameTicking;
using Content.Shared.Ghost;
using Content.Shared._Capibara.CCVar;
using Content.Shared._EinsteinEngines.Language;
using Content.Shared._Capibara.TTS;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.Player;

namespace Content.Server._Capibara.TTS;

/// <summary>
/// Server-side TTS system. Listens for entity speech, sends text to the TTS service
/// via Redis, and streams audio chunks to nearby clients.
/// </summary>
public sealed partial class TTSSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly ITTSClient _ttsClient = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly LanguageSystem _language = default!;

    private ISawmill _log = default!;
    private bool _enabled;

    /// <summary>
    /// Mirror of ChatSystem voice range constants.
    /// </summary>
    private const int VoiceRange = 10;
    private const int WhisperClearRange = 2;

    public override void Initialize()
    {
        base.Initialize();
        _log = Logger.GetSawmill("capibara.tts");

        _cfg.OnValueChanged(CapibaraCCVars.TTSEnabled, OnEnabledChanged, true);

        // TextToSpeechComponent is added to humanoids via SharedHumanoidAppearanceSystem.SetBarkVoice.
        // For non-humanoid players, we add it on spawn.
        SubscribeLocalEvent<TextToSpeechComponent, EntitySpokeEvent>(OnEntitySpoke);
        SubscribeLocalEvent<PlayerSpawnCompleteEvent>(OnPlayerSpawnComplete);
        SubscribeNetworkEvent<PreviewTTSRequestEvent>(OnPreviewTTSRequest); // Capibara - TTS preview

        InitializeAssignVoice();
    }

    public override void Shutdown()
    {
        base.Shutdown();
        _cfg.UnsubValueChanged(CapibaraCCVars.TTSEnabled, OnEnabledChanged);
    }

    private void OnEnabledChanged(bool enabled)
    {
        _enabled = enabled;
    }

    private void OnPlayerSpawnComplete(PlayerSpawnCompleteEvent args)
    {
        // Ensure all players get the TTS component, regardless of race
        EnsureComp<TextToSpeechComponent>(args.Mob);
    }

    private void OnEntitySpoke(EntityUid uid, TextToSpeechComponent component, EntitySpokeEvent args)
    {
        if (!_enabled)
            return;

        // Skip radio messages for Phase 1
        if (args.Channel != null)
            return;

        var voiceId = GetOrAssignVoice(uid, component);
        if (voiceId == null)
            return;

        var text = CleanTextForTTS(args.Message);
        if (string.IsNullOrWhiteSpace(text))
            return;

        _log.Debug("TTS request for entity {0}: voice={1}, text=\"{2}\"", uid, voiceId, text);

        var streamId = Guid.NewGuid();
        var isWhisper = args.IsWhisper;
        var netUid = GetNetEntity(uid);

        // Build a filtered recipient list that respects whisper range and language understanding
        var filter = BuildTTSFilter(uid, isWhisper, args.Language);
        if (filter.Count == 0)
            return;

        // Send header to filtered clients
        var headerEvent = new TTSHeaderEvent(streamId, netUid, isWhisper);
        RaiseNetworkEvent(headerEvent, filter);

        // Queue TTS generation
        _ttsClient.GenerateTTS(text, voiceId, TTSEffect.None,
            (data, isLast) =>
            {
                // Stream chunks to clients
                var chunkEvent = new TTSChunkEvent(streamId, data, isLast);
                RaiseNetworkEvent(chunkEvent, filter);
            },
            error =>
            {
                _log.Warning("TTS generation failed for entity {0}: {1}", uid, error);
                // Send empty final chunk so client knows to stop waiting
                var endEvent = new TTSChunkEvent(streamId, Array.Empty<byte>(), true);
                RaiseNetworkEvent(endEvent, filter);
            });
    }

    // Capibara - TTS preview
    private readonly Dictionary<ICommonSession, TimeSpan> _previewCooldowns = new();
    private static readonly TimeSpan PreviewCooldown = TimeSpan.FromSeconds(3);

    private void OnPreviewTTSRequest(PreviewTTSRequestEvent ev, EntitySessionEventArgs args)
    {
        if (!_enabled)
            return;

        var session = args.SenderSession;

        // Rate limit
        var now = TimeSpan.FromTicks(DateTime.UtcNow.Ticks);
        if (_previewCooldowns.TryGetValue(session, out var lastPreview) && now - lastPreview < PreviewCooldown)
            return;

        _previewCooldowns[session] = now;

        // Validate voice ID exists
        if (!_prototypeManager.HasIndex<Content.Shared._Capibara.TTS.Prototypes.VoicePrototype>(ev.VoiceId))
            return;

        var filter = Filter.Empty().AddPlayer(session);
        var streamId = Guid.NewGuid();

        // Use the player's own entity as source so the client plays it globally
        var sourceEntity = session.AttachedEntity != null ? GetNetEntity(session.AttachedEntity.Value) : NetEntity.Invalid;

        // Send header
        var headerEvent = new TTSHeaderEvent(streamId, sourceEntity, false);
        RaiseNetworkEvent(headerEvent, filter);

        // Generate TTS with sample text
        _ttsClient.GenerateTTS("Hola, esta es mi voz.", ev.VoiceId, TTSEffect.None,
            (data, isLast) =>
            {
                var chunkEvent = new TTSChunkEvent(streamId, data, isLast);
                RaiseNetworkEvent(chunkEvent, filter);
            },
            error =>
            {
                _log.Warning("TTS preview failed for session {0}: {1}", session, error);
                var endEvent = new TTSChunkEvent(streamId, Array.Empty<byte>(), true);
                RaiseNetworkEvent(endEvent, filter);
            });
    }
    // End Capibara - TTS preview

    /// <summary>
    /// Builds a recipient filter for TTS audio that respects whisper range and language understanding.
    /// Only includes players who can both hear and understand the speech.
    /// </summary>
    private Filter BuildTTSFilter(EntityUid speaker, bool isWhisper, LanguagePrototype language)
    {
        var filter = Filter.Empty();
        var speakerPos = _transform.GetMapCoordinates(speaker);
        var range = isWhisper ? WhisperClearRange : VoiceRange;

        foreach (var session in _playerManager.Sessions)
        {
            var listener = session.AttachedEntity;
            if (listener == null)
                continue;

            var isGhost = HasComp<GhostHearingComponent>(listener.Value);

            if (!isGhost)
            {
                // Check 1: Are they on the same map and within range?
                var listenerPos = _transform.GetMapCoordinates(listener.Value);
                if (speakerPos.MapId != listenerPos.MapId)
                    continue;

                if ((speakerPos.Position - listenerPos.Position).Length() > range)
                    continue;

                // Check 2: Can they understand the language?
                if (!_language.CanUnderstand(listener.Value, language.ID))
                    continue;
            }

            filter.AddPlayer(session);
        }

        return filter;
    }

    /// <summary>
    /// Clean text for TTS processing: strip markup tags, convert numbers to words.
    /// </summary>
    private static string CleanTextForTTS(string text)
    {
        // Remove rich text markup tags like [color=...], [bold], etc.
        text = MarkupTagRegex().Replace(text, "");

        // Convert numbers to words for natural speech
        text = NumberConverter.ConvertNumbersToWords(text);

        // Collapse whitespace
        text = WhitespaceRegex().Replace(text, " ").Trim();

        return text;
    }

    [GeneratedRegex(@"\[/?[^\]]+\]")]
    private static partial Regex MarkupTagRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}
