# SPDX-FileCopyrightText: 2024 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 Plykiya <58439124+Plykiya@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 ThatGuyUSA <thatguyusa123@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Ignaz "Ian" Kraft <ignaz.k@live.de>
# SPDX-FileCopyrightText: 2025 Killerqu00 <47712032+Killerqu00@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

contraband-examine-text-Minor =
    { $type ->
        *[item] [color=yellow]Este objeto se considera contrabando menor.[/color]
        [reagent] [color=yellow]Este reactivo se considera contrabando menor.[/color]
    }
contraband-examine-text-Restricted =
    { $type ->
        *[item] [color=yellow]Este objeto está restringido por departamento.[/color]
        [reagent] [color=yellow]Este reactivo está restringido por departamento.[/color]
    }
contraband-examine-text-Restricted-department =
    { $type ->
        *[item] [color=yellow]Este objeto está restringido a {$departments} y puede considerarse contrabando.[/color]
        [reagent] [color=yellow]Este reactivo está restringido a {$departments} y puede considerarse contrabando.[/color]
    }
contraband-examine-text-Major =
    { $type ->
        *[item] [color=red]Este objeto se considera contrabando mayor.[/color]
        [reagent] [color=red]Este reactivo se considera contrabando mayor.[/color]
    }
contraband-examine-text-GrandTheft =
    { $type ->
        *[item] [color=red]¡Este objeto es un objetivo muy valioso para los agentes del Sindicato![/color]
        [reagent] [color=red]¡Este reactivo es un objetivo muy valioso para los agentes del Sindicato![/color]
    }
contraband-examine-text-Syndicate =
    { $type ->
        *[item] [color=crimson]¡Este objeto es contrabando del Sindicato altamente ilegal![/color]
        [reagent] [color=crimson]¡Este reactivo es contrabando del Sindicato altamente ilegal![/color]
    }
contraband-examine-text-Magical =
    { $type ->
        *[item] [color=#b337b3]¡Este objeto es contrabando mágico altamente ilegal![/color]
        [reagent] [color=#b337b3]¡Este reactivo es contrabando mágico altamente ilegal![/color]
    }
contraband-examine-text-Clown = [color=yellow]Este objeto le pertenece al payaso, devuélvelo antes de que se ponga triste.[/color]

contraband-examine-text-avoid-carrying-around = [color=red][italic]Probablemente quieras evitar llevarlo visiblemente sin una buena razón.[/italic][/color]
contraband-examine-text-in-the-clear = [color=green][italic]Deberías poder llevarlo visiblemente sin problemas.[/italic][/color]

contraband-examinable-verb-text = Legalidad
contraband-examinable-verb-message = Comprobar la legalidad de este objeto.

contraband-department-plural = {$department}
contraband-job-plural = {MAKEPLURAL($job)}

contraband-examine-text-Highly-Illegal =
    { $type ->
        *[item] [color=crimson]¡Este objeto es contrabando altamente ilegal![/color]
        [reagent] [color=crimson]¡Este reactivo es contrabando altamente ilegal![/color]
    }
