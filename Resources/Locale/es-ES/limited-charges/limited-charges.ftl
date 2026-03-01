# SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

limited-charges-charges-remaining = {$charges ->
    [one] Tiene [color=fuchsia]{$charges}[/color] carga restante.
    *[other] Tiene [color=fuchsia]{$charges}[/color] cargas restantes.
}

limited-charges-max-charges = Está a cargas [color=green]máximas[/color].
limited-charges-recharging = {$seconds ->
    [one] Queda [color=yellow]{$seconds}[/color] segundo hasta la próxima carga.
    *[other] Quedan [color=yellow]{$seconds}[/color] segundos hasta la próxima carga.
}
