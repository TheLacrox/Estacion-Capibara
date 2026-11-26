# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Errant <35878406+Errant-4@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

set-game-preset-command-description = Establece el preajuste de juego para el número indicado de rondas próximas. También puede mostrar en el lobby el título y la descripción de otro preajuste para engañar a los jugadores.
set-game-preset-command-help-text = setgamepreset <id> [número de rondas, por defecto 1] [preajuste señuelo]
set-game-preset-optional-argument-not-integer = Si se proporciona el argumento 2, debe ser un número.

set-game-preset-preset-error = No se puede encontrar el preset de juego "{$preset}"
#set-game-preset-preset-set = Set game preset to "{$preset}"
set-game-preset-preset-set-finite = Preset de juego establecido en "{$preset}" para las próximas {$rounds} rondas.

set-game-preset-command-hint-1 = <id>

set-game-preset-command-hint-2 = [número de rondas]

set-game-preset-command-hint-3 = [preset señuelo]

set-game-preset-decoy-error = Si se proporciona el argumento 3, debe ser un preset válido. No se encontró el preset de juego "{$preset}"

set-game-preset-preset-set-finite-with-decoy = Preset de juego establecido en "{$preset}" para las próximas {$rounds} rondas, mostrando {$decoy} en el lobby.
