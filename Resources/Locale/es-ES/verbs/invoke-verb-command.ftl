# SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Localization used for the invoke verb command.
# Mostly help + error messages.

invoke-verb-command-description = Invoca un verbo con el nombre dado sobre una entidad, con la entidad del jugador
invoke-verb-command-help = invokeverb <playerUid | "self"> <targetUid> <verbName | "interaction" | "activation" | "alternative">

invoke-verb-command-invalid-args = invokeverb toma 2 argumentos.

invoke-verb-command-invalid-player-uid = No se pudo analizar el uid del jugador, o no se pasó "self".
invoke-verb-command-invalid-target-uid = No se pudo analizar el uid del objetivo.

invoke-verb-command-invalid-player-entity = El uid del jugador dado no corresponde a una entidad válida.
invoke-verb-command-invalid-target-entity = El uid del objetivo dado no corresponde a una entidad válida.

invoke-verb-command-success = Se invocó el verbo '{ $verb }' en { $target } con { $player } como usuario.

invoke-verb-command-verb-not-found = No se pudo encontrar el verbo { $verb } en { $target }.
