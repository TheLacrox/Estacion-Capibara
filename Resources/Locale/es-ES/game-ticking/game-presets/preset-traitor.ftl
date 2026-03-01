# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Morbo <exstrominer@gmail.com>
# SPDX-FileCopyrightText: 2021 Paul Ritter <ritter.paul1@googlemail.com>
# SPDX-FileCopyrightText: 2021 ShadowCommander <10494922+ShadowCommander@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Interrobang01 <113810873+Interrobang01@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 ZeroDayDaemon <60460608+ZeroDayDaemon@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 AJCM-git <60196617+AJCM-git@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 OctoRocket <88291550+OctoRocket@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
# SPDX-FileCopyrightText: 2023 keronshb <54602815+keronshb@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Arkanic <50847107+Arkanic@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Errant <35878406+Errant-4@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Mr. 27 <45323883+Dutch-VanDerLinde@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

## Traitor

traitor-round-end-codewords = Las palabras clave eran: [color=White]{$codewords}[/color]
traitor-round-end-agent-name = traidor

objective-issuer-syndicate = [color=crimson]El Sindicato[/color]
objective-issuer-unknown = Desconocido

# Shown at the end of a round of Traitor

traitor-title = Traidor
traitor-description = Hay traidores entre nosotros...
traitor-not-enough-ready-players = ¡No hay suficientes jugadores preparados para la partida! Había {$readyPlayersCount} jugadores preparados de los {$minimumPlayers} necesarios. No se puede iniciar Traidor.
traitor-no-one-ready = ¡Ningún jugador está preparado! No se puede iniciar Traidor.

## TraitorDeathMatch
traitor-death-match-title = Combate a Muerte de Traidores
traitor-death-match-description = Todos son traidores. Todos quieren verse muertos los unos a los otros.
traitor-death-match-station-is-too-unsafe-announcement = La estación es demasiado peligrosa para continuar. Tienes un minuto.
traitor-death-match-end-round-description-first-line = Los PDAs recuperados después...
traitor-death-match-end-round-description-entry = El PDA de {$originalName}, con {$tcBalance} TC

## TraitorRole

# TraitorRole
traitor-role-greeting =
    Eres un agente enviado por {$corporation} en nombre de [color = darkred]El Sindicato.[/color]
    Tus objetivos y palabras clave están listados en el menú de personaje.
    Usa tu enlace para comprar las herramientas que necesitarás para esta misión.
    ¡Muerte a Nanotrasen!
traitor-role-codewords =
    Las palabras clave son: [color = lightgray]
    {$codewords}.[/color]
    Las palabras clave pueden usarse en conversaciones normales para identificarte discretamente ante otros agentes del sindicato.
    Escucha con atención y mantenlas en secreto.
traitor-role-uplink-code =
    Establece tu tono de llamada con las notas [color = lightgray]{$code}[/color] para bloquear o desbloquear tu enlace.
    ¡Recuerda bloquearlo después, o la tripulación de la estación también podrá abrirlo fácilmente!
traitor-role-uplink-pen-code =
    Gira el bolígrafo a la combinación [color = lightgray]{$code}[/color] para desbloquear tu enlace.
    Los grados representan ángulos de rotación. El enlace se bloquea automáticamente al cerrarse.
traitor-role-uplink-implant =
    Tu implante de enlace ha sido activado, accede a él desde tu barra de acceso rápido.
    El enlace es seguro a menos que alguien lo extraiga de tu cuerpo.

# don't need all the flavour text for character menu
traitor-role-codewords-short =
    Las palabras clave son:
    {$codewords}.
traitor-role-uplink-code-short = Tu código de enlace es {$code}. Configúralo como tono de llamada de tu PDA para acceder al enlace.
traitor-role-uplink-pen-code-short = El código de tu enlace de bolígrafo es {$code}. Gira el bolígrafo para desbloquearlo. Se bloquea al cerrarse.
traitor-role-uplink-implant-short = Tu enlace fue implantado. Accede a él desde tu barra de acceso rápido.

traitor-role-moreinfo =
    Encuentra más información sobre tu rol en el menú de personaje.

traitor-role-nouplink =
    No tienes un enlace del sindicato. Aprovéchalo al máximo.

traitor-role-allegiances =
    Tus lealtades:

traitor-role-notes =
    Notas de tu empleador:

