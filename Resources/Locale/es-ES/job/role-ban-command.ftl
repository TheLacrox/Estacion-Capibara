# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Riggle <27156122+RigglePrime@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Localization for role ban command

cmd-roleban-desc = Prohíbe a un jugador un rol
cmd-roleban-help = Uso: roleban <nombre o ID de usuario> <trabajo> <razón> [duración en minutos, omitir o 0 para baneo permanente]

## Completion result hints
cmd-roleban-hint-1 = <nombre o ID de usuario>
cmd-roleban-hint-2 = <trabajo>
cmd-roleban-hint-3 = <razón>
cmd-roleban-hint-4 = [duración en minutos, omitir o 0 para baneo permanente]
cmd-roleban-hint-5 = [severidad]

cmd-roleban-hint-duration-1 = Permanente
cmd-roleban-hint-duration-2 = 1 día
cmd-roleban-hint-duration-3 = 3 días
cmd-roleban-hint-duration-4 = 1 semana
cmd-roleban-hint-duration-5 = 2 semanas
cmd-roleban-hint-duration-6 = 1 mes


### Localization for role unban command

cmd-roleunban-desc = Perdona el baneo de rol de un jugador
cmd-roleunban-help = Uso: roleunban <id de baneo de rol>
cmd-roleunban-unable-to-parse-id = No se puede interpretar {$id} como un entero de id de baneo.
                                   {$help}

## Completion result hints
cmd-roleunban-hint-1 = <id de baneo de rol>


### Localization for roleban list command

cmd-rolebanlist-desc = Lista los baneos de rol del usuario
cmd-rolebanlist-help = Uso: <nombre o ID de usuario> [incluir desbaneados]

## Completion result hints
cmd-rolebanlist-hint-1 = <nombre o ID de usuario>
cmd-rolebanlist-hint-2 = [incluir desbaneados]


cmd-roleban-minutes-parse = {$time} no es una cantidad de minutos válida.\n{$help}
cmd-roleban-severity-parse = ${severity} no es una severidad válida\n{$help}.
cmd-roleban-arg-count = Cantidad de argumentos inválida.
cmd-roleban-job-parse = El trabajo {$job} no existe.
cmd-roleban-name-parse = No se puede encontrar un jugador con ese nombre.
cmd-roleban-existing = {$target} ya tiene un baneo de rol para {$role}.
cmd-roleban-success = Se prohibió a {$target} el rol {$role} con la razón {$reason} {$length}.

cmd-roleban-inf = permanentemente
cmd-roleban-until =  hasta {$expires}

# Department bans
cmd-departmentban-desc = Prohíbe a un jugador los roles de un departamento
cmd-departmentban-help = Uso: departmentban <nombre o ID de usuario> <departamento> <razón> [duración en minutos, omitir o 0 para baneo permanente]
