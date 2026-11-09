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
injector-volume-label = Volumen: [color=white]{$currentVolume}/{$totalVolume}u[/color]
    Modo: [color=white]{$modeString}[/color]

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

injector-volume-transfer-label = Volumen: [color=white]{$currentVolume}/{$totalVolume}u[/color]
    Modo: [color=white]{$modeString}[/color] ([color=white]{$transferVolume}u[/color])

injector-toggle-verb-text = Alternar modo del inyector

injector-component-inject-mode-name = inyectar

injector-component-draw-mode-name = extraer

injector-component-dynamic-mode-name = dinámico

injector-component-mode-changed-text = Modo: {$mode}

injector-component-transfer-success-message-self = Te transfieres {$amount}u.

injector-component-inject-success-message-self = ¡Te inyectas {$amount}u!

injector-component-draw-success-message-self = Te extraes {$amount}u.

injector-component-target-already-full-message-self = ¡Ya estás lleno!

injector-component-target-is-empty-message-self = ¡Estás vacío!

injector-component-cannot-toggle-dynamic-message = ¡No se puede alternar el modo dinámico!

injector-component-empty-message = ¡{CAPITALIZE(THE($injector))} está vacío!

injector-component-blocked-user = ¡El equipo protector bloqueó tu inyección!

injector-component-blocked-other = ¡{CAPITALIZE(THE(POSS-ADJ($target)))} armadura bloqueó la inyección de {THE($user)}!

injector-component-cannot-transfer-message-self = ¡No puedes transferirte a ti mismo!

injector-component-cannot-inject-message-self = ¡No puedes inyectarte a ti mismo!

injector-component-cannot-draw-message-self = ¡No puedes extraer de ti mismo!

injector-component-ignore-mobs = ¡Este inyector solo puede interactuar con contenedores!

injector-component-needle-injecting-user = Empiezas a inyectar con la aguja.

injector-component-needle-injecting-target = ¡{CAPITALIZE(THE($user))} está intentando clavarte una aguja!

injector-component-needle-drawing-user = Empiezas a extraer con la aguja.

injector-component-needle-drawing-target = ¡{CAPITALIZE(THE($user))} está intentando usar una aguja para extraerte sangre!

injector-component-spray-injecting-user = Empiezas a preparar la boquilla del aerosol.

injector-component-spray-injecting-target = ¡{CAPITALIZE(THE($user))} está intentando colocarte una boquilla de spray!

injector-component-feel-prick-message = ¡Sientes un pequeño pinchazo!
