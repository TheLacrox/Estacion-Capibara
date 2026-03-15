// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Capibara.TTS;

namespace Content.Client._Capibara.TTS;

/// <summary>
/// Client system to send TTS preview requests to the server.
/// </summary>
public sealed class TTSPreviewSystem : EntitySystem
{
    public void SendPreview(PreviewTTSRequestEvent ev)
    {
        RaiseNetworkEvent(ev);
    }
}
