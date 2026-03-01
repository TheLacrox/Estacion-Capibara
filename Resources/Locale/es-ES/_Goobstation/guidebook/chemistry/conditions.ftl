# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 SX-7 <92227810+SX-7@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

reagent-effect-condition-guidebook-stamina-damage-threshold =
    { $max ->
        [2147483648] el objetivo tiene al menos {NATURALFIXED($min, 2)} de daño de stamina
        *[other] { $min ->
                    [0] el objetivo tiene como máximo {NATURALFIXED($max, 2)} de daño de stamina
                    *[other] el objetivo tiene entre {NATURALFIXED($min, 2)} y {NATURALFIXED($max, 2)} de daño de stamina
                 }
    }

reagent-effect-condition-guidebook-unique-bloodstream-chem-threshold =
    { $max ->
        [2147483648] { $min ->
                        [1] hay al menos {$min} reactivo
                        *[other] hay al menos {$min} reactivos
                     }
        [1] { $min ->
               [0] hay como máximo {$max} reactivo
               *[other] hay entre {$min} y {$max} reactivos
            }
        *[other] { $min ->
                    [-1] hay como máximo {$max} reactivos
                    *[other] hay entre {$min} y {$max} reactivos
                 }
    }

reagent-effect-condition-guidebook-typed-damage-threshold =
    { $inverse ->
        [true] el objetivo tiene como máximo
        *[false] el objetivo tiene al menos
    } { $changes } de daño
