// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Body.Components;

namespace Content.Shared.Body;

/// <summary>
/// Event wrapper for relaying events to organs within a body.
/// Similar to BodyPartRelayedEvent but for organ-level relay.
/// </summary>
[ByRefEvent]
public record struct BodyRelayedEvent<TEvent>(Entity<BodyComponent> Body, TEvent Args);
