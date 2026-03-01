# SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 PrPleGoo <PrPleGoo@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

agent-id-new = { $number ->
    [0] No obtuviste ningún acceso nuevo de {THE($card)}.
    [one] Obtuviste un nuevo acceso de {THE($card)}.
   *[other] Obtuviste {$number} nuevos accesos de {THE($card)}.
}

agent-id-card-current-name = Nombre:
agent-id-card-current-job = Trabajo:
agent-id-card-job-icon-label = Ícono de trabajo:
agent-id-menu-title = Tarjeta ID de Agente
