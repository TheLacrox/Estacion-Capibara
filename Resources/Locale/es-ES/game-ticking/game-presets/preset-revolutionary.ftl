# SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Vasilis <vasilis@pikachu.systems>
# SPDX-FileCopyrightText: 2023 coolmankid12345 <55817627+coolmankid12345@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 coolmankid12345 <coolmankid12345@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
# SPDX-FileCopyrightText: 2024 BombasterDS <115770678+BombasterDS@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Killerqu00 <47712032+Killerqu00@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Mr. 27 <45323883+Dutch-VanDerLinde@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

## Rev Head

roles-antag-rev-head-name = Cabecilla Revolucionario
roles-antag-rev-head-objective = Tu objetivo es tomar el control de la estación convirtiendo a la gente a tu causa y eliminando a todos los miembros de Mando.

head-rev-role-greeting =
    Eres un Cabecilla Revolucionario.
    Tu misión es eliminar a todo el Mando de la estación mediante la muerte, el exilio o el encarcelamiento.
    El Sindicato te ha patrocinado con un manifiesto que persuade a la tripulación a unirse a tu bando.
    Ten cuidado, esto no funcionará con Seguridad ni con el Mando — su lealtad es inquebrantable.
    ¡Viva la revolución!

head-rev-briefing =
    Usa el manifiesto para convertir gente a tu causa.
    Elimina a todos los jefes para tomar el control de la estación.

head-rev-break-mindshield = ¡El Escudo Mental neutralizó los poderes hipnóticos, pero su funcionamiento se ha visto comprometido!

## Rev

roles-antag-rev-name = Revolucionario
roles-antag-rev-objective = Tu objetivo es garantizar la seguridad de los jefes revolucionarios y seguir sus órdenes, y ayudarlos a tomar la estación eliminando a todos los miembros de Mando.

rev-break-control = ¡{$name} ha recordado su verdadera lealtad!

rev-role-greeting =
    Eres un revolucionario. Tu tarea es proteger a los revolucionarios jefe y ayudarles a tomar el control de la estación.
    La revolución debe trabajar unida para matar, reducir o convertir a todos los miembros del Mando.
    ¡Viva la revolución!

rev-briefing = Ayuda a tus cabecillas revolucionarios a convertir o eliminar a cada jefe para tomar el control de la estación.

## General

rev-title = Revolucionarios
rev-description = Hay revolucionarios ocultos entre la tripulación que buscan convertir a otros a su causa y derrocar a Mando.

rev-not-enough-ready-players = No hay suficientes jugadores listos para la partida. Había {$readyPlayersCount} jugadores listos de los {$minimumPlayers} necesarios. No se pueden iniciar los Revolucionarios.
rev-no-one-ready = ¡Ningún jugador está listo! No se pueden iniciar los Revolucionarios.
rev-no-heads = No había Jefes Revolucionarios para seleccionar. No se pueden iniciar los Revolucionarios.

rev-won = Los jefes revolucionarios sobrevivieron y tomaron con éxito el control de la estación.

rev-lost = El Mando sobrevivió y neutralizó a todos los Cabecillas Rev.

rev-stalemate = Tanto Mando como los jefes revolucionarios han muerto. Es un empate.

rev-reverse-stalemate = Tanto Mando como los jefes revolucionarios sobrevivieron.

rev-headrev-count = {$initialCount ->
    [one] Había un revolucionario jefe:
    *[other] Había {$initialCount} revolucionarios jefe:
}

rev-headrev-name-user = [color=#5e9cff]{$name}[/color] ([color=gray]{$username}[/color]) convirtió a {$count} {$count ->
    [one] persona
    *[other] personas
}

rev-headrev-name = [color=#5e9cff]{$name}[/color] convirtió a {$count} {$count ->
    [one] persona
    *[other] personas
}

## Deconverted window

rev-deconverted-title = ¡Desconvertido!
rev-deconverted-text =
    Cuando el último cabecilla fue neutralizado, la revolución terminó.

    Ya no eres un revolucionario, así que compórtate.
rev-deconverted-confirm = Confirmar
