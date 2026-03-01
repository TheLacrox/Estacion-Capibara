# SPDX-FileCopyrightText: 2022 Dylan Corrales <DeathCamel58@gmail.com>
# SPDX-FileCopyrightText: 2022 Moony <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Morb <14136326+Morb0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Visne <39844191+Visne@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2024 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Hannah Giovanna Dawson <karakkaraz@gmail.com>
# SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
# SPDX-FileCopyrightText: 2024 Simon <63975668+Simyon264@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Vasilis <vasilis@pikachu.systems>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Myra <vasilis@pikachu.systems>
# SPDX-FileCopyrightText: 2025 PJB3005 <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2025 nikthechampiongr <32041239+nikthechampiongr@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

cmd-whitelistadd-desc = Añade al jugador con el nombre de usuario dado a la lista blanca del servidor.
cmd-whitelistadd-help = Uso: whitelistadd <nombre de usuario o ID de usuario>
cmd-whitelistadd-existing = ¡{$username} ya está en la lista blanca!
cmd-whitelistadd-added = {$username} añadido a la lista blanca
cmd-whitelistadd-not-found = No se puede encontrar '{$username}'
cmd-whitelistadd-arg-player = [jugador]

cmd-whitelistremove-desc = Elimina al jugador con el nombre de usuario dado de la lista blanca del servidor.
cmd-whitelistremove-help = Uso: whitelistremove <nombre de usuario o ID de usuario>
cmd-whitelistremove-existing = ¡{$username} no está en la lista blanca!
cmd-whitelistremove-removed = {$username} eliminado de la lista blanca
cmd-whitelistremove-not-found = No se puede encontrar '{$username}'
cmd-whitelistremove-arg-player = [jugador]

cmd-kicknonwhitelisted-desc = Expulsa a todos los jugadores que no están en la lista blanca del servidor.
cmd-kicknonwhitelisted-help = Uso: kicknonwhitelisted

ban-banned-permanent = Esta prohibición solo se eliminará mediante apelación.
ban-banned-permanent-appeal = Esta prohibición solo se eliminará mediante apelación. Puedes apelar en {$link}
ban-expires = Esta prohibición es por {$duration} minutos y expirará a las {$time} UTC.
ban-banned-1 = Tú, u otro usuario de este equipo o conexión, tiene prohibido jugar aquí.
ban-banned-2 = Fuiste baneado por: "{$adminName}"
ban-banned-3 = El motivo del ban es: "{$reason}"
ban-banned-4 = Los intentos de eludir esta prohibición, como crear una nueva cuenta, quedarán registrados.

soft-player-cap-full = ¡El servidor está lleno!
panic-bunker-account-denied = Este servidor está en modo pánico de bunker, habilitado frecuentemente como precaución contra ataques. Las nuevas conexiones de cuentas que no cumplan ciertos requisitos no son aceptadas temporalmente. Inténtalo más tarde
panic-bunker-account-denied-reason = Este servidor está en modo pánico de bunker, habilitado frecuentemente como precaución contra ataques. Las nuevas conexiones de cuentas que no cumplan ciertos requisitos no son aceptadas temporalmente. Inténtalo más tarde. Motivo: "{$reason}"
panic-bunker-account-reason-account = Tu cuenta de Space Station 14 es demasiado nueva. Debe tener más de {$minutes} minutos de antigüedad
panic-bunker-account-reason-overall = Tu tiempo de juego total en el servidor debe ser mayor que {$minutes} $minutes

whitelist-playtime = No tienes suficiente tiempo de juego para unirte a este servidor. Necesitas al menos {$minutes} minutos de tiempo de juego para unirte a este servidor.
whitelist-player-count = Este servidor no está aceptando jugadores actualmente. Por favor, inténtalo más tarde.
whitelist-notes = Actualmente tienes demasiadas notas de administrador para unirte a este servidor. Puedes verificar tus notas escribiendo /adminremarks en el chat.
whitelist-manual = No estás en la lista blanca de este servidor.
whitelist-blacklisted = Estás en la lista negra de este servidor.
whitelist-always-deny = No tienes permitido unirte a este servidor.
whitelist-fail-prefix = No en lista blanca: {$msg}

cmd-blacklistadd-desc = Añade al jugador con el nombre de usuario dado a la lista negra del servidor.
cmd-blacklistadd-help = Uso: blacklistadd <nombre de usuario>
cmd-blacklistadd-existing = ¡{$username} ya está en la lista negra!
cmd-blacklistadd-added = {$username} añadido a la lista negra
cmd-blacklistadd-not-found = No se puede encontrar '{$username}'
cmd-blacklistadd-arg-player = [jugador]

cmd-blacklistremove-desc = Elimina al jugador con el nombre de usuario dado de la lista negra del servidor.
cmd-blacklistremove-help = Uso: blacklistremove <nombre de usuario>
cmd-blacklistremove-existing = ¡{$username} no está en la lista negra!
cmd-blacklistremove-removed = {$username} eliminado de la lista negra
cmd-blacklistremove-not-found = No se puede encontrar '{$username}'
cmd-blacklistremove-arg-player = [jugador]

baby-jail-account-denied = Este servidor es un servidor para novatos, destinado a nuevos jugadores y a quienes quieran ayudarles. Las nuevas conexiones de cuentas demasiado antiguas o que no estén en la lista blanca no son aceptadas. ¡Echa un vistazo a otros servidores y descubre todo lo que Space Station 14 tiene para ofrecer. ¡Que te diviertas!
baby-jail-account-denied-reason = Este servidor es un servidor para novatos, destinado a nuevos jugadores y a quienes quieran ayudarles. Las nuevas conexiones de cuentas demasiado antiguas o que no estén en la lista blanca no son aceptadas. ¡Echa un vistazo a otros servidores y descubre todo lo que Space Station 14 tiene para ofrecer. ¡Que te diviertas! Motivo: "{$reason}"
baby-jail-account-reason-account = Tu cuenta de Space Station 14 es demasiado antigua. Debe tener menos de {$minutes} minutos de antigüedad
baby-jail-account-reason-overall = Tu tiempo de juego total en el servidor debe ser menor que {$minutes} $minutes

generic-misconfigured = El servidor está mal configurado y no acepta jugadores. Por favor, contacta con el propietario del servidor e inténtalo de nuevo más tarde.

ipintel-server-ratelimited = Este servidor utiliza un sistema de seguridad con verificación externa, que ha alcanzado su límite máximo de verificación. Por favor, contacta con el equipo de administración del servidor para obtener asistencia e inténtalo de nuevo más tarde.
ipintel-unknown = Este servidor utiliza un sistema de seguridad con verificación externa, pero se encontró un error. Por favor, contacta con el equipo de administración del servidor para obtener asistencia e inténtalo de nuevo más tarde.
ipintel-suspicious = Parece que te estás conectando a través de un centro de datos o VPN. Por razones administrativas, no permitimos conexiones VPN para jugar. Por favor, contacta con el equipo de administración del servidor si crees que esto es un error.

hwid-required = Tu cliente se ha negado a enviar un ID de hardware. Por favor, contacta con el equipo de administración para obtener más asistencia.
