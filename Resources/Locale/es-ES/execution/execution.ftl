# SPDX-FileCopyrightText: 2024 Celene <4323352+CuteMoonGod@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Hmeister <nathan.springfredfoxbon4@gmail.com>
# SPDX-FileCopyrightText: 2024 Scribbles0 <91828755+Scribbles0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Veritius <veritiusgaming@gmail.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 nikthechampiongr <32041239+nikthechampiongr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

execution-verb-name = Ejecutar
execution-verb-message = Usa tu arma para ejecutar a alguien.

# Todas las cadenas de localización a continuación tienen acceso a las siguientes variables
# attacker (la persona que comete la ejecución)
# victim (la persona que es ejecutada)
# weapon (el arma usada para la ejecución)

execution-popup-melee-initial-internal = Preparas {THE($weapon)} contra la garganta de {THE($victim)}.
execution-popup-melee-initial-external = { CAPITALIZE(THE($attacker)) } prepara su {$weapon} contra la garganta de {THE($victim)}.
execution-popup-melee-complete-internal = ¡Le cortas la garganta a {THE($victim)}!
execution-popup-melee-complete-external = ¡{ CAPITALIZE(THE($attacker)) } le corta la garganta a {THE($victim)}!

execution-popup-self-initial-internal = Preparas {THE($weapon)} contra tu propia garganta.
execution-popup-self-initial-external = { CAPITALIZE(THE($attacker)) } prepara su {$weapon} contra su propia garganta.
execution-popup-self-complete-internal = ¡Te cortas tu propia garganta!
execution-popup-self-complete-external = ¡{ CAPITALIZE(THE($attacker)) } se corta su propia garganta!
