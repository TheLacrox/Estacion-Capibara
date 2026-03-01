# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Alex Evgrashin <aevgrashin@yandex.ru>
# SPDX-FileCopyrightText: 2022 TheDarkElites <73414180+TheDarkElites@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 ike709 <ike709@github.com>
# SPDX-FileCopyrightText: 2022 ike709 <ike709@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <comedian_vs_clown@hotmail.com>
# SPDX-FileCopyrightText: 2023 Chronophylos <nikolai@chronophylos.com>
# SPDX-FileCopyrightText: 2023 Daniil Sikinami <60344369+VigersRay@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
# SPDX-FileCopyrightText: 2024 Julian Giebel <juliangiebel@live.de>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later


### UI

# For the PDA screen
comp-pda-ui = ID: [color=white]{$owner}[/color], [color=yellow]{$jobTitle}[/color]

comp-pda-ui-blank = ID:

comp-pda-ui-owner = Propietario: [color=white]{$actualOwnerName}[/color]

comp-pda-io-program-list-button = Programas

comp-pda-io-settings-button = Configuración

comp-pda-io-program-fallback-title = Programa

comp-pda-io-no-programs-available = No hay programas disponibles

pda-bound-user-interface-show-uplink-title = Abrir Uplink
pda-bound-user-interface-show-uplink-description = Accede a tu uplink

pda-bound-user-interface-lock-uplink-title = Bloquear Uplink
pda-bound-user-interface-lock-uplink-description = Impide que alguien acceda a tu uplink sin el código

comp-pda-ui-menu-title = PDA

comp-pda-ui-footer = Asistente Digital Personal

comp-pda-ui-station = Estación: [color=white]{$station}[/color]

comp-pda-ui-station-alert-level = Nivel de alerta: [color={ $color }]{ $level }[/color]

comp-pda-ui-station-alert-level-instructions = Instrucciones: [color=white]{ $instructions }[/color]

comp-pda-ui-station-time = Duración del turno: [color=white]{ $time }[/color]

comp-pda-ui-eject-id-button = Expulsar ID

comp-pda-ui-eject-pen-button = Expulsar bolígrafo

comp-pda-ui-ringtone-button = Tono de llamada

comp-pda-ui-ringtone-button-description = Cambia el tono de llamada de tu PDA

comp-pda-ui-toggle-flashlight-button = Alternar linterna

pda-bound-user-interface-music-button = Instrumento musical

pda-bound-user-interface-music-button-description = Toca música con tu PDA

comp-pda-ui-unknown = Desconocido

comp-pda-ui-unassigned = Sin asignar

pda-notification-message = [font size=12][bold]PDA[/bold] { $header }: [/font]
    "{ $message }"
