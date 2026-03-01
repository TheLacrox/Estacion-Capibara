# SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Localization used for the list verbs command.
# Mostly help + error messages.

list-verbs-command-description = Lista todos los verbos que un jugador puede usar en una entidad dada.
list-verbs-command-help = listverbs <playerUid | "self"> <targetUid>

list-verbs-command-invalid-args = listverbs requiere 2 argumentos.

list-verbs-command-invalid-player-uid = No se pudo analizar el uid del jugador, o no se pasó "self".
list-verbs-command-invalid-target-uid = No se pudo analizar el uid del objetivo.

list-verbs-command-invalid-player-entity = El uid del jugador dado no corresponde a una entidad válida.
list-verbs-command-invalid-target-entity = El uid del objetivo dado no corresponde a una entidad válida.

list-verbs-verb-listing = { $type }: { $verb }
