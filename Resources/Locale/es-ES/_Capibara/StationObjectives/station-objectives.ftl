# SPDX-FileCopyrightText: 2025 Capibara Station Contributors
# SPDX-License-Identifier: AGPL-3.0-or-later

# Paper
station-objective-paper-name = Directiva NT de Estación
station-objective-paper-content =
    {"["}head=2]NANOTRASEN COMANDO CENTRAL[/head]
    {"["}bold]DIRECTIVA DE ESTACIÓN — ÓRDENES PRIORITARIAS[/bold]
    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]

    Capitán,

    Por orden del Comando Central de Nanotrasen, su estación ha sido asignada
    con los siguientes objetivos prioritarios para este turno:

    {$objectives}

    El cumplimiento de estos objetivos es obligatorio. Se transmitirán
    actualizaciones de estado por fax seguro tras su verificación.

    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]
    {"["}italic]Nanotrasen Comando Central — Gloria a la Corporación[/italic]

# Objective names
station-objective-power-production-name = Producción de Energía
station-objective-cargo-export-name = Exportaciones de Carga
station-objective-research-points-name = Puntos de Investigación
station-objective-botany-harvest-name = Cosecha de Botánica

# Objective descriptions
station-objective-power-production-desc = Producir 10 GW de energía sostenida durante al menos 1 minuto.
station-objective-cargo-export-desc = Ganar al menos 50,000 créditos en exportaciones netas de carga.
station-objective-research-points-desc = Acumular al menos 25,000 puntos de investigación.
station-objective-botany-harvest-desc = Cosechar al menos 30 plantas.

# Fax completion
station-objective-fax-completion-name = Actualización de Estado de Objetivo
station-objective-fax-completion =
    {"["}head=2]NANOTRASEN COMANDO CENTRAL[/head]
    {"["}bold]ACTUALIZACIÓN DE ESTADO DE OBJETIVO[/bold]
    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]

    Capitán,

    El Comando Central confirma que {"["}bold]{$objective}[/bold] ha sido
    verificado como {"["}color=green]{"["}bold]COMPLETADO[/bold][/color].

    {"["}bold]Estado Actual:[/bold]
    {$statuses}

    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]
    {"["}italic]Nanotrasen Comando Central — Gloria a la Corporación[/italic]

# Fax status labels
station-objective-fax-status-completed = {"["}color=green]COMPLETADO[/color]
station-objective-fax-status-pending = {"["}color=yellow]PENDIENTE[/color]

# Round end
station-objective-round-end-header = {"["}bold]Objetivos de Estación:[/bold]
station-objective-round-end-completed = {"["}color=green]COMPLETADO[/color]
station-objective-round-end-failed = {"["}color=red]FALLIDO[/color]
