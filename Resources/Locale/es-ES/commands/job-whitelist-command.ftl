# SPDX-FileCopyrightText: 2024 DrSmugleaf <10968691+DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Tunguso4ka <71643624+Tunguso4ka@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

cmd-jobwhitelist-job-does-not-exist = El trabajo {$job} no existe.
cmd-jobwhitelist-player-not-found = No se encontró al jugador {$player}.
cmd-jobwhitelist-hint-player = [jugador]
cmd-jobwhitelist-hint-job = [trabajo]

cmd-jobwhitelistadd-desc = Permite a un jugador jugar un trabajo en lista blanca.
cmd-jobwhitelistadd-help = Uso: jobwhitelistadd <nombre de usuario> <trabajo>
cmd-jobwhitelistadd-already-whitelisted = {$player} ya está en la lista blanca para jugar como {$jobId} ({$jobName}).
cmd-jobwhitelistadd-added = Se añadió a {$player} a la lista blanca de {$jobId} ({$jobName}).

cmd-jobwhitelistget-desc = Obtiene todos los trabajos para los que un jugador está en lista blanca.
cmd-jobwhitelistget-help = Uso: jobwhitelistget <nombre de usuario>
cmd-jobwhitelistget-whitelisted-none = El jugador {$player} no está en la lista blanca para ningún trabajo.
cmd-jobwhitelistget-whitelisted-for = "El jugador {$player} está en la lista blanca para:
{$jobs}"

cmd-jobwhitelistremove-desc = Elimina la capacidad de un jugador para jugar un trabajo en lista blanca.
cmd-jobwhitelistremove-help = Uso: jobwhitelistremove <nombre de usuario> <trabajo>
cmd-jobwhitelistremove-was-not-whitelisted = {$player} no estaba en la lista blanca para jugar como {$jobId} ({$jobName}).
cmd-jobwhitelistremove-removed = Se eliminó a {$player} de la lista blanca para {$jobId} ({$jobName}).
