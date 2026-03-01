# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Voting system related console commands

## 'createvote' command

cmd-createvote-desc = Crea una votación
cmd-createvote-help = Uso: createvote <'restart'|'preset'|'map'>
cmd-createvote-cannot-call-vote-now = ¡No puedes iniciar una votación ahora mismo!
cmd-createvote-invalid-vote-type = Tipo de votación no válido
cmd-createvote-arg-vote-type = <tipo de votación>

## 'customvote' command

cmd-customvote-desc = Crea una votación personalizada
cmd-customvote-help = Uso: customvote <título> <opción1> <opción2> [opción3...]
cmd-customvote-on-finished-tie = ¡La votación '{$title}' ha terminado: empate entre {$ties}!
cmd-customvote-on-finished-win = ¡La votación '{$title}' ha terminado: {$winner} gana!
cmd-customvote-arg-title = <título>
cmd-customvote-arg-option-n = <opción{ $n }>

## 'vote' command

cmd-vote-desc = Vota en una votación activa
cmd-vote-help = vote <voteId> <opción>
cmd-vote-cannot-call-vote-now = ¡No puedes iniciar una votación ahora mismo!
cmd-vote-on-execute-error-must-be-player = Debe ser un jugador
cmd-vote-on-execute-error-invalid-vote-id = ID de votación no válido
cmd-vote-on-execute-error-invalid-vote-options = Opciones de votación no válidas
cmd-vote-on-execute-error-invalid-vote = Votación no válida
cmd-vote-on-execute-error-invalid-option = Opción no válida

## 'listvotes' command

cmd-listvotes-desc = Lista las votaciones actualmente activas
cmd-listvotes-help = Uso: listvotes

## 'cancelvote' command

cmd-cancelvote-desc = Cancela una votación activa
cmd-cancelvote-help = Uso: cancelvote <id>
                      Puedes obtener el ID con el comando listvotes.
cmd-cancelvote-error-invalid-vote-id = ID de votación no válido
cmd-cancelvote-error-missing-vote-id = ID faltante
cmd-cancelvote-arg-id = <id>
