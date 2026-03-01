# SPDX-FileCopyrightText: 2021 20kdc <asdd2808@gmail.com>
# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

cable-multitool-system-internal-error-no-power-node = Tu multiherramienta muestra: "ERROR INTERNO: NO ES UN CABLE DE ALIMENTACIÓN".
cable-multitool-system-internal-error-missing-component = Tu multiherramienta muestra: "ERROR INTERNO: CABLE ANÓMALO".
cable-multitool-system-verb-name = Energía
cable-multitool-system-verb-tooltip = Usa una multiherramienta para examinar las estadísticas de energía.

cable-multitool-system-statistics = Tu multiherramienta muestra una lista de estadísticas:
                                    Suministro actual: { POWERWATTS($supplyc) }
                                    De baterías: { POWERWATTS($supplyb) }
                                    Suministro teórico: { POWERWATTS($supplym) }
                                    Consumo ideal: { POWERWATTS($consumption) }
                                    Almacenamiento de entrada: { POWERJOULES($storagec) } / { POWERJOULES($storagem) } ({ TOSTRING($storager, "P1") })
                                    Almacenamiento de salida: { POWERJOULES($storageoc) } / { POWERJOULES($storageom) } ({ TOSTRING($storageor, "P1") })
