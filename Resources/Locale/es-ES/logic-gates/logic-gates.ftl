# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

logic-gate-examine = Actualmente es {INDEFINITE($gate)} puerta {$gate}.

logic-gate-cycle = Cambiado a {INDEFINITE($gate)} puerta {$gate}

power-sensor-examine = Actualmente está comprobando la batería de {$output ->
    [true] salida
    *[false] entrada
} de la red.
power-sensor-voltage-examine = Está comprobando la red eléctrica de {$voltage}.

power-sensor-switch = Cambiado a comprobar la batería de {$output ->
    [true] salida
    *[false] entrada
} de la red.
power-sensor-voltage-switch = ¡Red cambiada a {$voltage}!
