# SPDX-FileCopyrightText: 2024 PrPleGoo <PrPleGoo@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

cmd-atvrange-desc = Establece el rango de depuración de atmos (como dos flotantes, inicio [rojo] y fin [azul])
cmd-atvrange-help = Uso: {$command} <inicio> <fin>
cmd-atvrange-error-start = Flotante INICIO incorrecto
cmd-atvrange-error-end = Flotante FIN incorrecto
cmd-atvrange-error-zero = La escala no puede ser cero, ya que causaría una división por cero en AtmosDebugOverlay.

cmd-atvmode-desc = Establece el modo de depuración de atmos. Esto reiniciará automáticamente la escala.
cmd-atvmode-help = Uso: {$command} <TotalMoles/GasMoles/Temperature> [<ID de gas (para GasMoles)>]
cmd-atvmode-error-invalid = Modo inválido
cmd-atvmode-error-target-gas = Se debe proporcionar un gas objetivo para este modo.
cmd-atvmode-error-out-of-range = ID de gas no analizable o fuera de rango.
cmd-atvmode-error-info = No se requiere información adicional para este modo.

cmd-atvcbm-desc = Cambia de rojo/verde/azul a escala de grises
cmd-atvcbm-help = Uso: {$command} <verdadero/falso>
cmd-atvcbm-error = Indicador inválido
