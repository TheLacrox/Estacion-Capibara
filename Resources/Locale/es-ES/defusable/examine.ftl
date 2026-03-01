# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 eclips_e <67359748+Just-a-Unity-Dev@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

defusable-examine-defused = {CAPITALIZE(THE($name))} está [color=lime]desactivado[/color].
defusable-examine-live = {CAPITALIZE(THE($name))} está [color=red]en marcha[/color] y le quedan [color=red]{$time}[/color] segundos.
defusable-examine-live-display-off = {CAPITALIZE(THE($name))} está [color=red]en marcha[/color], y el temporizador parece estar apagado.
defusable-examine-inactive = {CAPITALIZE(THE($name))} está [color=lime]inactivo[/color], pero aún puede ser armado.
defusable-examine-bolts = Los pernos están {$down ->
[true] [color=red]abajo[/color]
*[false] [color=green]arriba[/color]
}.
