# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

## Strings for the "grant_connect_bypass" command.

cmd-grant_connect_bypass-desc = Permite temporalmente a un usuario saltarse las comprobaciones de conexión normales.
cmd-grant_connect_bypass-help = Uso: grant_connect_bypass <usuario> [duración en minutos]
    Concede temporalmente a un usuario la capacidad de saltarse las restricciones de conexión normales.
    El bypass solo aplica a este servidor de juego y expirará tras (por defecto) 1 hora.
    Podrán unirse independientemente de la lista blanca, el búnker de pánico o el límite de jugadores.

cmd-grant_connect_bypass-arg-user = <usuario>
cmd-grant_connect_bypass-arg-duration = [duración en minutos]

cmd-grant_connect_bypass-invalid-args = Se esperaba 1 o 2 argumentos
cmd-grant_connect_bypass-unknown-user = No se puede encontrar al usuario '{$user}'
cmd-grant_connect_bypass-invalid-duration = Duración no válida '{$duration}'

cmd-grant_connect_bypass-success = Se añadió correctamente el bypass para el usuario '{$user}'

