# SPDX-FileCopyrightText: 2022 LittleBuilderJane <63973502+LittleBuilderJane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Myctai <108953437+Myctai@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <metalgearsloth@gmail.com>
# SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2024 IProduceWidgets <107586145+IProduceWidgets@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 MilenVolf <63782763+MilenVolf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
# SPDX-FileCopyrightText: 2024 strO0pwafel <153459934+strO0pwafel@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# Commands
## Delay shuttle round end
cmd-delayroundend-desc = Detiene el temporizador que finaliza la ronda cuando la lanzadera de emergencia sale del hiperespacio.
cmd-delayroundend-help = Uso: delayroundend
emergency-shuttle-command-round-yes = Ronda retrasada.
emergency-shuttle-command-round-no = No se puede retrasar el fin de la ronda.

## Dock emergency shuttle
cmd-dockemergencyshuttle-desc = Llama a la lanzadera de emergencia y la atraca en la estación... si puede.
cmd-dockemergencyshuttle-help = Uso: dockemergencyshuttle

## Launch emergency shuttle
cmd-launchemergencyshuttle-desc = Lanza la lanzadera de emergencia anticipadamente si es posible.
cmd-launchemergencyshuttle-help = Uso: launchemergencyshuttle

# Emergency shuttle
emergency-shuttle-left = La Lanzadera de Emergencia ha abandonado la estación. Tiempo estimado hasta que la lanzadera llegue a Mando Central: {$transitTime} segundos.
emergency-shuttle-launch-time = La lanzadera de emergencia despegará en {$consoleAccumulator} segundos.
emergency-shuttle-docked = La Lanzadera de Emergencia ha atracado al {$direction} de la estación, {$location}. Partirá en {$time} segundos.{$extended}
emergency-shuttle-good-luck = La Lanzadera de Emergencia no puede encontrar la estación. Buena suerte.
emergency-shuttle-nearby = La Lanzadera de Emergencia no puede encontrar un puerto de atraque válido. Ha emergido al {$direction} de la estación, {$location}. Partirá en {$time} segundos.{$extended}
emergency-shuttle-extended = {" "}El tiempo de lanzamiento se ha extendido debido a circunstancias desfavorables.

# Emergency shuttle console popup / announcement
emergency-shuttle-console-no-early-launches = El lanzamiento anticipado está desactivado
emergency-shuttle-console-auth-left = Se necesitan {$remaining} autorizaciones más para lanzar la lanzadera anticipadamente.
emergency-shuttle-console-auth-revoked = Autorización de lanzamiento anticipado revocada, se necesitan {$remaining} autorizaciones.
emergency-shuttle-console-denied = Acceso denegado

# UI
emergency-shuttle-console-window-title = Consola de Lanzadera de Emergencia
emergency-shuttle-ui-engines = MOTORES:
emergency-shuttle-ui-idle = En espera
emergency-shuttle-ui-repeal-all = Revocar todo
emergency-shuttle-ui-early-authorize = Autorización de Lanzamiento Anticipado
emergency-shuttle-ui-authorize = AUTORIZAR
emergency-shuttle-ui-repeal = REVOCAR
emergency-shuttle-ui-authorizations = Autorizaciones
emergency-shuttle-ui-remaining = Restantes: {$remaining}

# Map Misc.
map-name-centcomm = Mando Central
map-name-terminal = Terminal de Llegadas
