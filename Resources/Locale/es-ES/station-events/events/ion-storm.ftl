# SPDX-FileCopyrightText: 2023 LankLTE <135308300+LankLTE@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 BIGZi0348 <118811750+BIGZi0348@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 lzk <124214523+lzk228@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

station-event-ion-storm-start-announcement = Tormenta iónica detectada cerca de la estación. Por favor, revise todos los equipos controlados por IA en busca de errores.

ion-storm-law-scrambled-number = [font="Monospace"][scramble rate=250 length={$length} chars="@@###$$&%!01"/][/font]

ion-storm-you = TÚ
ion-storm-the-station = LA ESTACIÓN
ion-storm-the-crew = LA TRIPULACIÓN
ion-storm-the-job = EL/LA {$job}
ion-storm-clowns = LOS PAYASOS
ion-storm-heads = LOS JEFES DE PERSONAL
ion-storm-crew = TRIPULACIÓN
ion-storm-people = PERSONAS

ion-storm-adjective-things = COSAS {$adjective}
ion-storm-x-and-y = {$x} Y {$y}

# joined is short for {$number} {$adjective}
# subjects can generally be threats or jobs or objects
# thing is specified above it
ion-storm-law-on-station = HAY {$joined} {$subjects} EN LA ESTACIÓN
ion-storm-law-call-shuttle = LA LANZADERA DEBE SER LLAMADA A CAUSA DE {$joined} {$subjects} EN LA ESTACIÓN
ion-storm-law-crew-are = LA/EL {$who} AHORA SON {$joined} {$subjects}

ion-storm-law-subjects-harmful = {$adjective} {$subjects} SON DAÑINOS PARA LA TRIPULACIÓN
ion-storm-law-must-harmful = QUIENES {$must} SON DAÑINOS PARA LA TRIPULACIÓN
# thing is a concept or action
ion-storm-law-thing-harmful = {$thing} ES DAÑINO PARA LA TRIPULACIÓN
ion-storm-law-job-harmful = {$adjective} {$job} SON DAÑINOS PARA LA TRIPULACIÓN
# thing is objects or concept, adjective applies in both cases
# this means you can get a law like "NOT HAVING CHRISTMAS-STEALING COMMUNISM IS HARMFUL TO THE CREW" :)
ion-storm-law-having-harmful = TENER {$adjective} {$thing} ES DAÑINO PARA LA TRIPULACIÓN
ion-storm-law-not-having-harmful = NO TENER {$adjective} {$thing} ES DAÑINO PARA LA TRIPULACIÓN

# thing is a concept or require
ion-storm-law-requires = {$who} {$plural ->
    [true] REQUIEREN
    *[false] REQUIERE
} {$thing}
ion-storm-law-requires-subjects = {$who} {$plural ->
    [true] REQUIEREN
    *[false] REQUIERE
} {$joined} {$subjects}

ion-storm-law-allergic = {$who} {$plural ->
    [true] SON
    *[false] ES
} {$severity} ALÉRGICO/A A {$allergy}
ion-storm-law-allergic-subjects = {$who} {$plural ->
    [true] SON
    *[false] ES
} {$severity} ALÉRGICO/A A {$adjective} {$subjects}

ion-storm-law-feeling = {$who} {$feeling} {$concept}
ion-storm-law-feeling-subjects = {$who} {$feeling} {$joined} {$subjects}

ion-storm-law-you-are = AHORA ERES {$concept}
ion-storm-law-you-are-subjects = AHORA ERES {$joined} {$subjects}
ion-storm-law-you-must-always = SIEMPRE DEBES {$must}
ion-storm-law-you-must-never = NUNCA DEBES {$must}

ion-storm-law-eat = LA/EL {$who} DEBE COMER {$adjective} {$food} PARA SOBREVIVIR
ion-storm-law-drink = LA/EL {$who} DEBE BEBER {$adjective} {$drink} PARA SOBREVIVIR

ion-storm-law-change-job = LA/EL {$who} AHORA SON {$adjective} {$change}
ion-storm-law-highest-rank = LA/EL {$who} AHORA SON LOS MIEMBROS DE TRIPULACIÓN DE MÁS ALTO RANGO
ion-storm-law-lowest-rank = LA/EL {$who} AHORA SON LOS MIEMBROS DE TRIPULACIÓN DE MÁS BAJO RANGO

ion-storm-law-crew-must = LA/EL {$who} DEBE {$must}
ion-storm-law-crew-must-go = LA/EL {$who} DEBE IR A {$area}

ion-storm-part = {$part ->
    [true] PARTE
    *[false] NO PARTE
}
# due to phrasing, this would mean a law such as
# ONLY HUMANS ARE NOT PART OF THE CREW
# would make non-human nukies/syndies/whatever crew :)
ion-storm-law-crew-only-1 = SOLO LA/EL {$who} SON {$part} DE LA TRIPULACIÓN
ion-storm-law-crew-only-2 = SOLO LA/EL {$who} Y {$other} SON {$part} DE LA TRIPULACIÓN
ion-storm-law-crew-only-subjects = SOLO {$adjective} {$subjects} SON {$part} DE LA TRIPULACIÓN
ion-storm-law-crew-must-do = SOLO QUIENES {$must} SON {$part} DE LA TRIPULACIÓN
ion-storm-law-crew-must-have = SOLO QUIENES TIENEN {$adjective} {$objects} SON {$part} DE LA TRIPULACIÓN
ion-storm-law-crew-must-eat = SOLO QUIENES COMEN {$adjective} {$food} SON {$part} DE LA TRIPULACIÓN

ion-storm-law-harm = DEBES DAÑAR A {$who} Y NO PERMITIR QUE, POR INACCIÓN, ESCAPEN DEL DAÑO
ion-storm-law-protect = NUNCA DEBES DAÑAR A {$who} NI PERMITIR QUE, POR INACCIÓN, SUFRAN DAÑO

# implementing other variants is annoying so just have this one
# COMMUNISM IS KILLING CLOWNS
ion-storm-law-concept-verb = {$concept} ESTÁ {$verb} {$subjects}

# leaving out renaming since its annoying for players to keep track of
