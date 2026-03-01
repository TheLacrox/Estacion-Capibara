# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2023 Riggle <27156122+RigglePrime@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Vasilis <vasilis@pikachu.systems>
# SPDX-FileCopyrightText: 2024 Julian Giebel <juliangiebel@live.de>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# ban
cmd-ban-desc = Banea a alguien
cmd-ban-help = Uso: ban <nombre o ID de usuario> <motivo> [duración en minutos, omitir o 0 para ban permanente]
cmd-ban-player = No se puede encontrar un jugador con ese nombre.
cmd-ban-invalid-minutes = ¡{$minutes} no es una cantidad de minutos válida!
cmd-ban-invalid-severity = ¡{$severity} no es una gravedad válida!
cmd-ban-invalid-arguments = Cantidad de argumentos inválida
cmd-ban-hint = <nombre/ID de usuario>
cmd-ban-hint-reason = <motivo>
cmd-ban-hint-duration = [duración]
cmd-ban-hint-severity = [gravedad]

cmd-ban-hint-duration-1 = Permanente
cmd-ban-hint-duration-2 = 1 día
cmd-ban-hint-duration-3 = 3 días
cmd-ban-hint-duration-4 = 1 semana
cmd-ban-hint-duration-5 = 2 semanas
cmd-ban-hint-duration-6 = 1 mes

# panel de ban
cmd-banpanel-desc = Abre el panel de bans
cmd-banpanel-help = Uso: banpanel [nombre o GUID de usuario]
cmd-banpanel-server = Esto no se puede usar desde la consola del servidor
cmd-banpanel-player-err = No se pudo encontrar al jugador especificado

# listbans
cmd-banlist-desc = Lista los bans activos de un usuario.
cmd-banlist-help = Uso: banlist <nombre o ID de usuario>
cmd-banlist-empty = No se encontraron bans activos para {$user}
cmd-banlist-hint = <nombre/ID de usuario>

cmd-ban_exemption_update-desc = Establece una exención a un tipo de ban para un jugador.
cmd-ban_exemption_update-help = Uso: ban_exemption_update <jugador> <bandera> [<bandera> [...]]
    Especifica múltiples banderas para dar a un jugador múltiples banderas de exención de ban.
    Para eliminar todas las exenciones, ejecuta este comando con "None" como única bandera.

cmd-ban_exemption_update-nargs = Se esperaban al menos 2 argumentos
cmd-ban_exemption_update-locate = No se puede localizar al jugador '{$player}'.
cmd-ban_exemption_update-invalid-flag = Bandera inválida '{$flag}'.
cmd-ban_exemption_update-success = Banderas de exención de ban actualizadas para '{$player}' ({$uid}).
cmd-ban_exemption_update-arg-player = <jugador>
cmd-ban_exemption_update-arg-flag = <bandera>

cmd-ban_exemption_get-desc = Muestra las exenciones de ban para un jugador específico.
cmd-ban_exemption_get-help = Uso: ban_exemption_get <jugador>

cmd-ban_exemption_get-nargs = Se esperaba exactamente 1 argumento
cmd-ban_exemption_get-none = El usuario no está exento de ningún ban.
cmd-ban_exemption_get-show = El usuario está exento de las siguientes banderas de ban: {$flags}.
cmd-ban_exemption_get-arg-player = <jugador>

# Panel de ban
ban-panel-title = Panel de bans
ban-panel-player = Jugador
ban-panel-ip = IP
ban-panel-hwid = HWID
ban-panel-reason = Motivo
ban-panel-last-conn = ¿Usar IP y HWID de la última conexión?
ban-panel-submit = Banear
ban-panel-confirm = ¿Estás seguro?
ban-panel-tabs-basic = Información básica
ban-panel-tabs-reason = Motivo
ban-panel-tabs-players = Lista de jugadores
ban-panel-tabs-role = Información de ban por rol
ban-panel-no-data = Debes proporcionar un usuario, IP o HWID para banear
ban-panel-invalid-ip = La dirección IP no pudo ser analizada. Por favor, inténtalo de nuevo
ban-panel-select = Seleccionar tipo
ban-panel-server = Ban del servidor
ban-panel-role = Ban por rol
ban-panel-minutes = Minutos
ban-panel-hours = Horas
ban-panel-days = Días
ban-panel-weeks = Semanas
ban-panel-months = Meses
ban-panel-years = Años
ban-panel-permanent = Permanente
ban-panel-ip-hwid-tooltip = Deja vacío y marca la casilla de abajo para usar los detalles de la última conexión
ban-panel-severity = Gravedad:
ban-panel-erase = Borrar mensajes de chat y al jugador de la ronda

# Cadena de ban
server-ban-string = {$admin} creó un ban del servidor de gravedad {$severity} que expira {$expires} para [{$name}, {$ip}, {$hwid}], con el motivo: {$reason}
server-ban-string-no-pii = {$admin} creó un ban del servidor de gravedad {$severity} que expira {$expires} para {$name} con el motivo: {$reason}
server-ban-string-never = nunca

# Expulsión por ban
ban-kick-reason = Has sido baneado
