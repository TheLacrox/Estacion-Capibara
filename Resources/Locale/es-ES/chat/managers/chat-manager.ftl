# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Kara Dinyes <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 Michael Phillips <1194692+MeltedPixel@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Morbo <exstrominer@gmail.com>
# SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <comedian_vs_clown@hotmail.com>
# SPDX-FileCopyrightText: 2023 Errant <35878406+dmnct@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nairod <110078045+Nairodian@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 OctoRocket <88291550+OctoRocket@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2023 Scribbles0 <91828755+Scribbles0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deathride58 <deathride58@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2024 Errant <35878406+Errant-4@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 SlamBamActionman <83650252+SlamBamActionman@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 chavonadelal <156101927+chavonadelal@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 BombasterDS <deniskaporoshok@gmail.com>
# SPDX-FileCopyrightText: 2025 BombasterDS2 <shvalovdenis.workmail@gmail.com>
# SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### UI

chat-manager-max-message-length = Tu mensaje excede el límite de {$maxMessageLength} caracteres
chat-manager-ooc-chat-enabled-message = El chat OOC ha sido activado.
chat-manager-ooc-chat-disabled-message = El chat OOC ha sido desactivado.
chat-manager-looc-chat-enabled-message = El chat LOOC ha sido activado.
chat-manager-looc-chat-disabled-message = El chat LOOC ha sido desactivado.
chat-manager-dead-looc-chat-enabled-message = Los jugadores muertos ahora pueden usar LOOC.
chat-manager-dead-looc-chat-disabled-message = Los jugadores muertos ya no pueden usar LOOC.
chat-manager-crit-looc-chat-enabled-message = Los jugadores en estado crítico ahora pueden usar LOOC.
chat-manager-crit-looc-chat-disabled-message = Los jugadores en estado crítico ya no pueden usar LOOC.
chat-manager-admin-ooc-chat-enabled-message = El chat OOC de Admin ha sido activado.
chat-manager-admin-ooc-chat-disabled-message = El chat OOC de Admin ha sido desactivado.

chat-manager-max-message-length-exceeded-message = Tu mensaje excedió el límite de {$limit} caracteres
chat-manager-no-headset-on-message = ¡No llevas auriculares puestos!
chat-manager-no-radio-key = ¡No se ha especificado clave de radio!
chat-manager-no-such-channel = ¡No existe un canal con la clave '{$key}'!
chat-manager-whisper-headset-on-message = ¡No puedes susurrar por la radio!

chat-manager-server-wrap-message = [bold]{$message}[/bold]
chat-manager-sender-announcement = Comando Central
chat-manager-sender-announcement-wrap-message = [font size=14][bold]Anuncio de {$sender}:[/font][font size=12]
                                                {$message}[/bold][/font]
# Einstein Engines - Language begin (changing colors for text based on language color in handler)
# For the message in double quotes, the font/color/bold/italic elements are repeated twice, outside the double quotes and inside.
# The outside elements are for formatting the double quotes, and the inside elements are for formatting the text in speech bubbles ([BubbleContent]).
chat-manager-entity-say-wrap-message = [BubbleHeader][bold][Name]{$entityName}[/Name][/bold][/BubbleHeader] {$verb}, [font={$fontType} size={$fontSize}]"[BubbleContent][font="{$fontType}" size={$fontSize}][color={$color}]{$message}[/color][/font][/BubbleContent]"[/font]
chat-manager-entity-say-bold-wrap-message = [BubbleHeader][bold][Name]{$entityName}[/Name][/bold][/BubbleHeader] {$verb}, [font={$fontType} size={$fontSize}]"[BubbleContent][font="{$fontType}" size={$fontSize}][bold][color={$color}]{$message}[/color][/bold][/font][/BubbleContent]"[/font]

chat-manager-entity-whisper-wrap-message = [font size=11][italic][BubbleHeader][Name]{$entityName}[/Name][/BubbleHeader] susurra, "[BubbleContent][color={$color}][font="{$fontType}"]{$message}[/font][/color][/BubbleContent][font size=11]"[/italic][/font]
chat-manager-entity-whisper-unknown-wrap-message = [font size=11][italic][BubbleHeader]Alguien[/BubbleHeader] susurra, "[BubbleContent][color={$color}][font="{$fontType}"]{$message}[/color][/font][/BubbleContent][font size=11]"[/italic][/font]
# Einstein Engines - Language end

# chat-manager-language-prefix = ({ $language }){" "} - Removed so it doesn't show up, not wanted, but part of the language system.

# THE() is not used here because the entity and its name can technically be disconnected if a nameOverride is passed...
chat-manager-entity-me-wrap-message = [italic]{ PROPER($entity) ->
    *[false] El {$entityName} {$message}[/italic]
     [true] {CAPITALIZE($entityName)} {$message}[/italic]
    }

chat-manager-entity-looc-wrap-message = LOOC: [bold]{$entityName}:[/bold] {$message}
chat-manager-send-ooc-wrap-message = OOC: [bold]{$playerName}:[/bold] {$message}
chat-manager-send-ooc-patron-wrap-message = OOC: [icon src="{$tierIcon}"/] [bold][color={$patronColor}]{$playerName}[/color]:[/bold] {$message}
chat-manager-send-ooc-patron-wrap-message-no-icon = OOC: [bold][color={$patronColor}]{$playerName}[/color]:[/bold] {$message}

chat-manager-send-dead-chat-wrap-message = {$deadChannelName}: [bold][BubbleHeader]{$playerName}[/BubbleHeader][/bold] {$verb}: "[BubbleContent]{$message}[/BubbleContent]"
chat-manager-send-admin-dead-chat-wrap-message = {$adminChannelName}: [bold]([BubbleHeader]{$userName}[/BubbleHeader])[/bold] {$verb}: "[BubbleContent]{$message}[/BubbleContent]"
chat-manager-send-admin-chat-wrap-message = {$adminChannelName}: [bold]{$playerName}:[/bold] {$message}
chat-manager-send-admin-announcement-wrap-message = [bold]{$adminChannelName}: {$message}[/bold]

chat-manager-send-hook-ooc-wrap-message = OOC: [bold](D){$senderName}:[/bold] {$message}

chat-manager-dead-channel-name = DEAD
chat-manager-admin-channel-name = ADMIN

chat-manager-rate-limited = ¡Estás enviando mensajes demasiado rápido!
chat-manager-rate-limit-admin-announcement = Aviso de límite de frecuencia: { $player }

## Speech verbs for chat

chat-speech-verb-suffix-exclamation = !
chat-speech-verb-suffix-exclamation-strong = !!
chat-speech-verb-suffix-question = ?
chat-speech-verb-suffix-stutter = -
chat-speech-verb-suffix-mumble = ..

chat-speech-verb-name-none = Ninguno
chat-speech-verb-name-default = Por defecto
chat-speech-verb-default = dice
chat-speech-verb-name-exclamation = Exclamando
chat-speech-verb-exclamation = exclama
chat-speech-verb-name-exclamation-strong = Gritando
chat-speech-verb-exclamation-strong = grita
chat-speech-verb-name-question = Preguntando
chat-speech-verb-question = pregunta
chat-speech-verb-name-stutter = Tartamudeando
chat-speech-verb-stutter = tartamudea
chat-speech-verb-name-mumble = Murmurando
chat-speech-verb-mumble = murmura

chat-speech-verb-name-arachnid = Arácnido
chat-speech-verb-insect-1 = chirría
chat-speech-verb-insect-2 = pía
chat-speech-verb-insect-3 = chasquea

chat-speech-verb-name-moth = Polilla
chat-speech-verb-winged-1 = aletea
chat-speech-verb-winged-2 = bate las alas
chat-speech-verb-winged-3 = zumba

chat-speech-verb-name-slime = Slime
chat-speech-verb-slime-1 = chapotea
chat-speech-verb-slime-2 = burbujea
chat-speech-verb-slime-3 = rezuma

chat-speech-verb-name-plant = Diona
chat-speech-verb-plant-1 = susurra con las hojas
chat-speech-verb-plant-2 = se mece
chat-speech-verb-plant-3 = cruje

chat-speech-verb-name-robotic = Robótico
chat-speech-verb-robotic-1 = declara
chat-speech-verb-robotic-2 = emite un pitido
chat-speech-verb-robotic-3 = hace boop

chat-speech-verb-name-reptilian = Reptiliano
chat-speech-verb-reptilian-1 = sisea
chat-speech-verb-reptilian-2 = resopla
chat-speech-verb-reptilian-3 = bufa

chat-speech-verb-name-skeleton = Esqueleto / Plasmaman
chat-speech-verb-skeleton-1 = traquetea
chat-speech-verb-skeleton-2 = castañetea
chat-speech-verb-skeleton-3 = cruje los huesos
chat-speech-verb-skeleton-4 = claquea
chat-speech-verb-skeleton-5 = chasquea

chat-speech-verb-name-vox = Vox
chat-speech-verb-vox-1 = chilla
chat-speech-verb-vox-2 = aúlla
chat-speech-verb-vox-3 = grazna

chat-speech-verb-name-canine = Canino
chat-speech-verb-canine-1 = ladra
chat-speech-verb-canine-2 = aúlla
chat-speech-verb-canine-3 = ladra con fuerza

chat-speech-verb-name-goat = Cabra
chat-speech-verb-goat-1 = bala
chat-speech-verb-goat-2 = gruñe
chat-speech-verb-goat-3 = llora

chat-speech-verb-name-small-mob = Ratón
chat-speech-verb-small-mob-1 = chilla
chat-speech-verb-small-mob-2 = pía

chat-speech-verb-name-large-mob = Carpa
chat-speech-verb-large-mob-1 = ruge
chat-speech-verb-large-mob-2 = gruñe

chat-speech-verb-name-monkey = Mono
chat-speech-verb-monkey-1 = chilla
chat-speech-verb-monkey-2 = aúlla

chat-speech-verb-name-cluwne = Cluwne

chat-speech-verb-name-parrot = Loro
chat-speech-verb-parrot-1 = grazna
chat-speech-verb-parrot-2 = trina
chat-speech-verb-parrot-3 = pía

chat-speech-verb-cluwne-1 = se ríe tontamente
chat-speech-verb-cluwne-2 = carcajea
chat-speech-verb-cluwne-3 = se ríe

chat-speech-verb-name-ghost = Fantasma
chat-speech-verb-ghost-1 = se queja
chat-speech-verb-ghost-2 = suspira
chat-speech-verb-ghost-3 = tararea
chat-speech-verb-ghost-4 = murmura

chat-speech-verb-name-electricity = Electricidad
chat-speech-verb-electricity-1 = chisporrotea
chat-speech-verb-electricity-2 = zumba
chat-speech-verb-electricity-3 = crepita

chat-speech-verb-name-wawa = Wawa
chat-speech-verb-wawa-1 = entona
chat-speech-verb-wawa-2 = declara
chat-speech-verb-wawa-3 = proclama
chat-speech-verb-wawa-4 = reflexiona
