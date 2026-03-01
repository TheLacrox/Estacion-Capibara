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
roles-antag-rev-head-objective = Tu objetivo es tomar el control de la estación convirtiendo a la gente a tu causa y eliminando a todo el personal de Mando en la estación.

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
roles-antag-rev-objective = Tu objetivo es garantizar la seguridad y seguir las órdenes de los Cabecillas Revolucionarios, así como eliminar o convertir a todo el personal de Mando en la estación.

rev-break-control = ¡{$name} ha recordado su verdadera lealtad!

rev-role-greeting =
    Eres un Revolucionario.
    Tu misión es tomar el control de la estación y proteger a los Cabecillas Revolucionarios.
    Elimina o convierte al personal de Mando.
    ¡Viva la revolución!

rev-briefing = Ayuda a tus cabecillas revolucionarios a convertir o eliminar a cada jefe para tomar el control de la estación.

## General

rev-title = Revolucionarios
rev-description = Hay revolucionarios entre nosotros.

rev-not-enough-ready-players = No hay suficientes jugadores preparados para la partida. Había {$readyPlayersCount} jugadores preparados de los {$minimumPlayers} necesarios. No se puede iniciar una Revolución.
rev-no-one-ready = ¡Ningún jugador está preparado! No se puede iniciar una Revolución.
rev-no-heads = No había Cabecillas Revolucionarios para seleccionar. No se puede iniciar una Revolución.

rev-won = Los Cabecillas Rev sobrevivieron y tomaron el control de la estación con éxito.

rev-lost = El Mando sobrevivió y neutralizó a todos los Cabecillas Rev.

rev-stalemate = Todos los Cabecillas Rev y el Mando murieron. Es un empate.

rev-reverse-stalemate = Tanto el Mando como los Cabecillas Rev sobrevivieron.

rev-headrev-count = {$initialCount ->
    [one] Hubo un Cabecilla Revolucionario:
    *[other] Hubo {$initialCount} Cabecillas Revolucionarios:
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
