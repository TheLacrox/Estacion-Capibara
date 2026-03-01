# SPDX-FileCopyrightText: 2022 Moony <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2022 Veritius <veritiusgaming@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

parse-minutes-fail = No se puede interpretar '{$minutes}' como minutos
parse-session-fail = No se encontró sesión para '{$username}'

## Role Timer Commands

# - playtime_addoverall
cmd-playtime_addoverall-desc = Añade los minutos especificados al tiempo de juego total de un jugador
cmd-playtime_addoverall-help = Uso: {$command} <nombre de usuario> <minutos>
cmd-playtime_addoverall-succeed = Se aumentó el tiempo total para {$username} a {TOSTRING($time, "dddd\\:hh\\:mm")}
cmd-playtime_addoverall-arg-user = <nombre de usuario>
cmd-playtime_addoverall-arg-minutes = <minutos>
cmd-playtime_addoverall-error-args = Se esperaban exactamente dos argumentos

# - playtime_addrole
cmd-playtime_addrole-desc = Añade los minutos especificados al tiempo de juego de un rol de un jugador
cmd-playtime_addrole-help = Uso: {$command} <nombre de usuario> <rol> <minutos>
cmd-playtime_addrole-succeed = Se aumentó el tiempo de juego del rol para {$username} / \'{$role}\' a {TOSTRING($time, "dddd\\:hh\\:mm")}
cmd-playtime_addrole-arg-user = <nombre de usuario>
cmd-playtime_addrole-arg-role = <rol>
cmd-playtime_addrole-arg-minutes = <minutos>
cmd-playtime_addrole-error-args = Se esperaban exactamente tres argumentos

# - playtime_getoverall
cmd-playtime_getoverall-desc = Obtiene los minutos especificados del tiempo de juego total de un jugador
cmd-playtime_getoverall-help = Uso: {$command} <nombre de usuario>
cmd-playtime_getoverall-success = El tiempo total para {$username} es {TOSTRING($time, "dddd\\:hh\\:mm")}.
cmd-playtime_getoverall-arg-user = <nombre de usuario>
cmd-playtime_getoverall-error-args = Se esperaba exactamente un argumento

# - GetRoleTimer
cmd-playtime_getrole-desc = Obtiene todos o un temporizador de rol de un jugador
cmd-playtime_getrole-help = Uso: {$command} <nombre de usuario> [rol]
cmd-playtime_getrole-no = No se encontraron temporizadores de rol
cmd-playtime_getrole-role = Rol: {$role}, Tiempo de juego: {$time}
cmd-playtime_getrole-overall = El tiempo de juego total es {$time}
cmd-playtime_getrole-succeed = El tiempo de juego para {$username} es: {TOSTRING($time, "dddd\\:hh\\:mm")}.
cmd-playtime_getrole-arg-user = <nombre de usuario>
cmd-playtime_getrole-arg-role = <rol|'General'>
cmd-playtime_getrole-error-args = Se esperaban exactamente uno o dos argumentos

# - playtime_save
cmd-playtime_save-desc = Guarda los tiempos de juego del jugador en la BD
cmd-playtime_save-help = Uso: {$command} <nombre de usuario>
cmd-playtime_save-succeed = Tiempo de juego guardado para {$username}
cmd-playtime_save-arg-user = <nombre de usuario>
cmd-playtime_save-error-args = Se esperaba exactamente un argumento

## 'playtime_flush' command'

cmd-playtime_flush-desc = Vuelca los rastreadores activos al almacenamiento de seguimiento de tiempo de juego.
cmd-playtime_flush-help = Uso: {$command} [nombre de usuario]
    Esto causa un volcado solo al almacenamiento interno, no vuelca a la BD inmediatamente.
    Si se proporciona un usuario, solo ese usuario es volcado.

cmd-playtime_flush-error-args = Se esperaban cero o un argumento
cmd-playtime_flush-arg-user = [nombre de usuario]
