# SPDX-FileCopyrightText: 2023 chromiumboy <50505512+chromiumboy@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

power-radiation-collector-gas-tank-missing = La ranura del depósito de plasma está [color=darkred]vacía[/color].
power-radiation-collector-gas-tank-present = La ranura del depósito de plasma está [color=darkgreen]llena[/color] y el indicador del depósito muestra [color={$fullness ->
    *[0]red]vacío
    [1]red]bajo
    [2]yellow]medio
    [3]lime]lleno
}[/color].
power-radiation-collector-enabled = Está [color={$state ->
    [true] darkgreen]encendido
    *[false] darkred]apagado
}[/color].
