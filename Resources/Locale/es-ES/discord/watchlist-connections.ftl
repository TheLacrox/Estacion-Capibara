# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Palladinium <patrick.chieppe@hotmail.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

discord-watchlist-connection-header =
    { $players ->
        [one] {$players} jugador en una lista de seguimiento se ha
        *[other] {$players} jugadores en una lista de seguimiento se han
    } conectado a {$serverName}

discord-watchlist-connection-entry = - {$playerName} con mensaje "{$message}"{ $expiry ->
        [0] {""}
        *[other] {" "}(vence <t:{$expiry}:R>)
    }{ $otherWatchlists ->
        [0] {""}
        [one] {" "}y {$otherWatchlists} lista de seguimiento más
        *[other] {" "}y {$otherWatchlists} listas de seguimiento más
    }
