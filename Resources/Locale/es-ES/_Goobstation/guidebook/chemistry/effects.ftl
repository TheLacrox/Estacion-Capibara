# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

reagent-effect-guidebook-deal-stamina-damage =
    { $chance ->
        [1] { $deltasign ->
                [1] Inflige
                *[-1] Cura
            }
        *[other]
            { $deltasign ->
                [1] infligir
                *[-1] curar
            }
    } { $amount } de daño de stamina { $immediate ->
                    [true] inmediato
                    *[false] con el tiempo
                  }

reagent-effect-guidebook-stealth-entities = Camufla las criaturas vivas cercanas.

reagent-effect-guidebook-change-faction = Cambia la facción de la criatura a {$faction}.

reagent-effect-guidebook-mutate-plants-nearby = Muta aleatoriamente las plantas cercanas.

reagent-effect-guidebook-dnascramble = Revuelve el ADN de la persona.

reagent-effect-guidebook-change-species = Convierte al objetivo en un {$species}.

reagent-effect-guidebook-change-species-random = Convierte al objetivo en una especie completamente aleatoria.

reagent-effect-guidebook-sex-change = Cambia el género de la persona

reagent-effect-guidebook-immunity-modifier =
    { $chance ->
        [1] Modifica
        *[other] modificar
    } la tasa de ganancia de inmunidad en {NATURALFIXED($gainrate, 5)}, la fuerza en {NATURALFIXED($strength, 5)} durante al menos {NATURALFIXED($time, 3)} {MANY("segundo", $time)}

reagent-effect-guidebook-disease-progress-change =
    { $chance ->
        [1] Modifica
        *[other] modificar
    } el progreso de las enfermedades de tipo {$type} en {NATURALFIXED($amount, 5)}

reagent-effect-guidebook-disease-mutate = Muta enfermedades en {NATURALFIXED($amount, 4)}
