# SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Loc for the pneumatic cannon.

pneumatic-cannon-component-itemslot-name = Tanque de gas

## Shown when trying to fire, but no gas

pneumatic-cannon-component-fire-no-gas = { CAPITALIZE(THE($cannon)) } hace clic, pero no sale gas.

## Shown when changing power.

pneumatic-cannon-component-change-power = { $power ->
    [High] Ajustas el limitador a potencia máxima. Se siente un poco demasiado potente...
    [Medium] Ajustas el limitador a potencia media.
    *[Low] Ajustas el limitador a potencia baja.
}

## Shown when being stunned by having the power too high.

pneumatic-cannon-component-power-stun = ¡La pura fuerza de { THE($cannon) } te derriba!
