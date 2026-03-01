# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Moony <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 moonheart08 <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 pointer-to-null <91910481+pointer-to-null@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Myctai <108953437+Myctai@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Radosvik <65792927+Radosvik@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 ShadowCommander <10494922+ShadowCommander@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 ZeroDayDaemon <60460608+ZeroDayDaemon@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 theashtronaut <112137107+theashtronaut@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Tom Leys <tom@crump-leys.com>
# SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Mervill <mervills.email@gmail.com>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Rainfey <rainfey0+github@gmail.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

game-ticker-restart-round = Reiniciando la ronda...
game-ticker-start-round = La ronda está comenzando ahora...
game-ticker-start-round-cannot-start-game-mode-fallback = ¡No se pudo iniciar el modo {$failedGameMode}! Usando {$fallbackMode} como respaldo...
game-ticker-start-round-cannot-start-game-mode-restart = ¡No se pudo iniciar el modo {$failedGameMode}! Reiniciando la ronda...
game-ticker-start-round-invalid-map = El mapa seleccionado {$map} no es compatible con el modo de juego {$mode}. Es posible que el modo de juego no funcione como se esperaba...
game-ticker-unknown-role = Desconocido
game-ticker-delay-start = El inicio de la ronda se ha retrasado {$seconds} segundos.
game-ticker-pause-start = El inicio de la ronda ha sido pausado.
game-ticker-pause-start-resumed = La cuenta atrás para el inicio de la ronda se ha reanudado.
game-ticker-player-join-game-message = ¡Bienvenido a Space Station 14! Si es tu primera vez jugando, asegúrate de leer las reglas del juego y no dudes en pedir ayuda en LOOC (OOC local) u OOC (generalmente disponible solo entre rondas).
game-ticker-get-info-text = ¡Bienvenido a [color=white]Space Station 14![/color]
                            La ronda actual es: [color=white]#{$roundId}[/color]
                            El número de jugadores actual es: [color=white]{$playerCount}[/color]
                            El mapa actual es: [color=white]{$mapName}[/color]
                            El modo de juego actual es: [color=white]{$gmTitle}[/color]
                            >[color=yellow]{$desc}[/color]
game-ticker-get-info-preround-text = ¡Bienvenido a [color=white]Space Station 14![/color]
                            La ronda actual es: [color=white]#{$roundId}[/color]
                            El número de jugadores actual es: [color=white]{$playerCount}[/color] ([color=white]{$readyCount}[/color] {$readyCount ->
                                [one] está
                                *[other] están
                            } listo)
                            El mapa actual es: [color=white]{$mapName}[/color]
                            El modo de juego actual es: [color=white]{$gmTitle}[/color]
                            >[color=yellow]{$desc}[/color]
game-ticker-no-map-selected = [color=yellow]¡Mapa aún no seleccionado![/color]
game-ticker-player-no-jobs-available-when-joining = Al intentar unirse a la partida, no había puestos de trabajo disponibles.

# Displayed in chat to admins when a player joins
player-join-message = El jugador {$name} se ha unido.
player-first-join-message = El jugador {$name} se ha unido por primera vez.

# Displayed in chat to admins when a player leaves
player-leave-message = El jugador {$name} se ha ido.

latejoin-arrival-announcement = ¡{$character} ({$job}) ha llegado a la estación!
latejoin-arrival-announcement-special = ¡{$job} {$character} en cubierta!
latejoin-arrival-sender = Estación
latejoin-arrivals-direction = En breve llegará una lanzadera que te llevará a tu estación.
latejoin-arrivals-direction-time = Una lanzadera que te llevará a tu estación llegará en {$time}.
latejoin-arrivals-dumped-from-shuttle = Una fuerza misteriosa te impide salir con la lanzadera de llegadas.
latejoin-arrivals-teleport-to-spawn = Una fuerza misteriosa te teleporta fuera de la lanzadera de llegadas. ¡Que tengas un turno seguro!

preset-not-enough-ready-players = No se puede iniciar {$presetName}. Requiere {$minimumPlayers} jugadores pero solo hay {$readyPlayersCount}.
preset-no-one-ready = No se puede iniciar {$presetName}. Ningún jugador está listo.

game-run-level-PreRoundLobby = Sala de espera previa a la ronda
game-run-level-InRound = En ronda
game-run-level-PostRound = Ronda terminada
