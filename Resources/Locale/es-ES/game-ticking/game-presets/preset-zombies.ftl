# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Tom Leys <tom@crump-leys.com>
# SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2024 IProduceWidgets <107586145+IProduceWidgets@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

zombie-title = Zombis
zombie-description = ¡Los muertos vivientes han sido desatados sobre la estación! Trabaja con la tripulación para sobrevivir al brote y asegurar la estación.

zombieteors-title = Zombiemeteoros
zombieteors-description = ¡Los muertos vivientes han sido desatados sobre la estación en medio de una catastrófica lluvia de meteoritos! ¡Trabaja con el resto de la tripulación y haz todo lo posible para sobrevivir!

zombie-not-enough-ready-players = ¡No hay suficientes jugadores preparados para la partida! Había {$readyPlayersCount} jugadores preparados de los {$minimumPlayers} necesarios. No se puede iniciar Zombis.
zombie-no-one-ready = ¡Ningún jugador está preparado! No se puede iniciar Zombis.

zombie-patientzero-role-greeting = Eres el infectado inicial. Consigue suministros y prepárate para tu eventual transformación. Tu objetivo es apoderarte de la estación infectando al mayor número de personas posible.
zombie-healing = Sientes un revuelo en tu carne
zombie-infection-warning = Sientes el virus zombi apoderarse de ti
zombie-infection-underway = Tu sangre empieza a espesarse

## goob edit
zombie-start-announcement = Brote confirmado de peligro biológico de nivel 7 a bordo de la estación. Seguridad ya no puede protegerte. Dirígete a zonas protegidas y aguarda la evacuación.
### Over
zombie-alone = Te sientes completamente solo.

zombie-shuttle-call = Hemos detectado que los muertos vivientes han tomado el control de la estación. Despachando una lanzadera de emergencia para recoger al personal restante.

zombie-round-end-initial-count = {$initialCount ->
    [one] Hubo un infectado inicial:
    *[other] Hubo {$initialCount} infectados iniciales:
}
zombie-round-end-user-was-initial = - [color=plum]{$name}[/color] ([color=gray]{$username}[/color]) fue uno de los infectados iniciales.

zombie-round-end-amount-none = [color=green]¡Todos los zombis fueron erradicados![/color]
zombie-round-end-amount-low = [color=green]Casi todos los zombis fueron exterminados.[/color]
zombie-round-end-amount-medium = [color=yellow]El {$percent}% de la tripulación fue convertida en zombis.[/color]
zombie-round-end-amount-high = [color=crimson]El {$percent}% de la tripulación fue convertida en zombis.[/color]
zombie-round-end-amount-all = [color=darkred]¡Toda la tripulación se convirtió en zombis![/color]

zombie-round-end-survivor-count = {$count ->
    [one] Solo quedó un superviviente:
    *[other] Solo quedaron {$count} supervivientes:
}
zombie-round-end-user-was-survivor = - [color=White]{$name}[/color] ([color=gray]{$username}[/color]) sobrevivió al brote.
