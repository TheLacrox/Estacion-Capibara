# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

generator-clogged = {CAPITALIZE(THE($generator))} se apaga abruptamente!

portable-generator-verb-start = Arrancar generador
portable-generator-verb-start-msg-unreliable = Arranca el generador. Puede requerir varios intentos.
portable-generator-verb-start-msg-reliable = Arranca el generador.
portable-generator-verb-start-msg-unanchored = ¡El generador debe estar anclado primero!
portable-generator-verb-stop = Detener generador
portable-generator-start-fail = Jalas el cordón, pero no arrancó.
portable-generator-start-success = Jalas el cordón, y el motor cobra vida.

portable-generator-ui-title = Generador Portátil
portable-generator-ui-status-stopped = Detenido:
portable-generator-ui-status-starting = Arrancando:
portable-generator-ui-status-running = En marcha:
portable-generator-ui-start = Arrancar
portable-generator-ui-stop = Detener
portable-generator-ui-target-power-label = Potencia objetivo (kW):
portable-generator-ui-efficiency-label = Eficiencia:
portable-generator-ui-fuel-use-label = Consumo de combustible:
portable-generator-ui-fuel-left-label = Combustible restante:
portable-generator-ui-clogged = ¡Contaminantes detectados en el depósito de combustible!
portable-generator-ui-eject = Expulsar
portable-generator-ui-eta = (~{ $minutes } min)
portable-generator-ui-unanchored = Sin anclar
portable-generator-ui-current-output = Salida actual: {$voltage}
portable-generator-ui-network-stats = Red:
portable-generator-ui-network-stats-value = { POWERWATTS($supply) } / { POWERWATTS($load) }
portable-generator-ui-network-stats-not-connected = Sin conexión

power-switchable-generator-examine = La salida de energía está configurada a {$voltage}.
power-switchable-generator-switched = ¡Salida cambiada a {$voltage}!

power-switchable-voltage = { $voltage ->
    [HV] [color=orange]HV[/color]
    [MV] [color=yellow]MV[/color]
    *[LV] [color=green]LV[/color]
}
power-switchable-switch-voltage = Cambiar a {$voltage}

fuel-generator-verb-disable-on = ¡Primero apaga el generador!
