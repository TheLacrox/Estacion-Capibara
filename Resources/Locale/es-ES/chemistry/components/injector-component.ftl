# SPDX-FileCopyrightText: 2021 20kdc <asdd2808@gmail.com>
# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Plykiya <58439124+Plykiya@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Plykiya <plykiya@protonmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

## UI

injector-draw-text = Extraer
injector-inject-text = Inyectar
injector-invalid-injector-toggle-mode = Inválido
injector-volume-label = Volumen: [color=white]{$currentVolume}/{$totalVolume}[/color]
    Modo: [color=white]{$modeString}[/color] ([color=white]{$transferVolume}u[/color])

## Entity

injector-component-drawing-text = Extrayendo ahora
injector-component-injecting-text = Inyectando ahora
injector-component-cannot-transfer-message = ¡No puedes transferir a {THE($target)}!
injector-component-cannot-draw-message = ¡No puedes extraer de {THE($target)}!
injector-component-cannot-inject-message = ¡No puedes inyectar en {THE($target)}!
injector-component-inject-success-message = ¡Inyectas {$amount}u en {THE($target)}!
injector-component-transfer-success-message = Transferiste {$amount}u a {THE($target)}.
injector-component-draw-success-message = Extrajiste {$amount}u de {THE($target)}.
injector-component-target-already-full-message = ¡{CAPITALIZE(THE($target))} ya está lleno!
injector-component-target-is-empty-message = ¡{CAPITALIZE(THE($target))} está vacío!
injector-component-cannot-toggle-draw-message = ¡Demasiado lleno para extraer!
injector-component-cannot-toggle-inject-message = ¡Nada que inyectar!

## mob-inject doafter messages

injector-component-drawing-user = Empiezas a retirar la aguja.
injector-component-injecting-user = Empiezas a introducir la aguja.
injector-component-drawing-target = ¡{CAPITALIZE(THE($user))} está intentando usar una aguja para extraerte!
injector-component-injecting-target = ¡{CAPITALIZE(THE($user))} está intentando inyectarte con una aguja!
injector-component-deny-user = ¡El exoesqueleto es demasiado grueso!
