# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Paul Ritter <ritter.paul1@googlemail.com>
# SPDX-FileCopyrightText: 2021 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 Mervill <mervills.email@gmail.com>
# SPDX-FileCopyrightText: 2023 alexkar598 <25136265+alexkar598@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Repo <47093363+Titian3@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 SlamBamActionman <83650252+SlamBamActionman@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# Displayed as initiator of vote when no user creates the vote
ui-vote-initiator-server = El servidor

## Default.Votes

ui-vote-restart-title = Reiniciar ronda
ui-vote-restart-succeeded = La votación de reinicio fue exitosa.
ui-vote-restart-failed = La votación de reinicio falló (se necesita { TOSTRING($ratio, "P0") }).
ui-vote-restart-fail-not-enough-ghost-players = La votación de reinicio falló: se requiere un mínimo del { $ghostPlayerRequirement }% de jugadores fantasma para iniciar una votación de reinicio. Actualmente, no hay suficientes jugadores fantasma.
ui-vote-restart-yes = Sí
ui-vote-restart-no = No
ui-vote-restart-abstain = Abstención

ui-vote-gamemode-title = Próximo modo de juego
ui-vote-gamemode-tie = ¡Empate en la votación de modo de juego! Eligiendo... { $picked }
ui-vote-gamemode-win = ¡{ $winner } ganó la votación de modo de juego!

ui-vote-map-title = Próximo mapa
ui-vote-map-tie = ¡Empate en la votación de mapa! Eligiendo... { $picked }
ui-vote-map-win = ¡{ $winner } ganó la votación de mapa!
ui-vote-map-notlobby = ¡La votación de mapas solo es válida en el vestíbulo previo a la ronda!
ui-vote-map-notlobby-time = ¡La votación de mapas solo es válida en el vestíbulo previo a la ronda con { $time } restante!


# Votekick votes
ui-vote-votekick-unknown-initiator = Un jugador
ui-vote-votekick-unknown-target = Jugador desconocido
ui-vote-votekick-title = { $initiator } ha iniciado una votación de expulsión para el usuario: { $targetEntity }. Motivo: { $reason }
ui-vote-votekick-yes = Sí
ui-vote-votekick-no = No
ui-vote-votekick-abstain = Abstención
ui-vote-votekick-success = La votación de expulsión para { $target } fue exitosa. Motivo: { $reason }
ui-vote-votekick-failure = La votación de expulsión para { $target } falló. Motivo: { $reason }
ui-vote-votekick-not-enough-eligible = No hay suficientes votantes elegibles en línea para iniciar una votación de expulsión: { $voters }/{ $requirement }
ui-vote-votekick-server-cancelled = La votación de expulsión para { $target } fue cancelada por el servidor.
