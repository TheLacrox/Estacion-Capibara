# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Swept <sweptwastaken@protonmail.com>
# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

shared-solution-container-component-on-examine-main-text = Contiene {INDEFINITE($desc)} [color={$color}]{$desc}[/color] { $chemCount ->
    [1] producto químico.
   *[other] mezcla de productos químicos.
    }

examinable-solution-has-recognizable-chemicals = Puedes reconocer {$recognizedString} en la solución.
examinable-solution-recognized = [color={$color}]{$chemical}[/color]

examinable-solution-on-examine-volume = La solución contenida está { $fillLevel ->
    [exact] con [color=white]{$current}/{$max}u[/color].
   *[other] [bold]{ -solution-vague-fill-level(fillLevel: $fillLevel) }[/bold].
}

examinable-solution-on-examine-volume-no-max = La solución contenida está { $fillLevel ->
    [exact] con [color=white]{$current}u[/color].
   *[other] [bold]{ -solution-vague-fill-level(fillLevel: $fillLevel) }[/bold].
}

examinable-solution-on-examine-volume-puddle = El charco está { $fillLevel ->
    [exact] [color=white]{$current}u[/color].
    [full] ¡enorme y desbordándose!
    [mostlyfull] ¡enorme y desbordándose!
    [halffull] profundo y fluyendo.
    [halfempty] muy profundo.
   *[mostlyempty] acumulándose.
    [empty] formando varios charcos pequeños.
}

-solution-vague-fill-level =
    { $fillLevel ->
        [full] [color=white]Lleno[/color]
        [mostlyfull] [color=#DFDFDF]Casi lleno[/color]
        [halffull] [color=#C8C8C8]Medio lleno[/color]
        [halfempty] [color=#C8C8C8]Medio vacío[/color]
        [mostlyempty] [color=#A4A4A4]Casi vacío[/color]
       *[empty] [color=gray]Vacío[/color]
    }
