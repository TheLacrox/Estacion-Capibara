# SPDX-FileCopyrightText: 2025 Capibara Station Contributors
# SPDX-License-Identifier: AGPL-3.0-or-later

# Paper
station-objective-paper-name = NT Station Directive
station-objective-paper-content =
    {"["}head=2]NANOTRASEN CENTRAL COMMAND[/head]
    {"["}bold]STATION DIRECTIVE — PRIORITY ORDERS[/bold]
    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]

    Captain,

    By order of Nanotrasen Central Command, your station has been assigned
    the following priority objectives for this shift:

    {$objectives}

    Completion of these objectives is mandatory. Status updates will be
    transmitted via secure fax upon verification.

    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]
    {"["}italic]Nanotrasen Central Command — Glory to the Corporation[/italic]

# Objective names
station-objective-power-production-name = Power Production
station-objective-cargo-export-name = Cargo Exports
station-objective-research-points-name = Research Points
station-objective-botany-harvest-name = Botany Harvest
station-objective-cargo-bounty-name = Cargo Bounties
station-objective-ore-processing-name = Ore Processing
station-objective-medicine-production-name = Medicine Production
station-objective-patient-healing-name = Patient Recovery

# Objective descriptions
station-objective-power-production-desc = Produce 10 GW of sustained power for at least 1 minute.
station-objective-cargo-export-desc = Earn at least 50,000 credits in net cargo exports.
station-objective-research-points-desc = Accumulate at least 200,000 research points.
station-objective-botany-harvest-desc = Harvest at least 200 plants.
station-objective-cargo-bounty-desc = Fulfill at least 20 cargo bounties.
station-objective-ore-processing-desc = Process at least {$targetMaterialVolume} units of ore into materials.
station-objective-medicine-production-desc = Produce at least {$targetMedicineUnits} units of medicine.
station-objective-patient-healing-desc = Medical staff must heal at least {$targetPatientsHealed} patients from critical condition.

# Fax completion
station-objective-fax-completion-name = Objective Status Update
station-objective-fax-completion =
    {"["}head=2]NANOTRASEN CENTRAL COMMAND[/head]
    {"["}bold]OBJECTIVE STATUS UPDATE[/bold]
    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]

    Captain,

    Central Command confirms that {"["}bold]{$objective}[/bold] has been
    verified as {"["}color=green]{"["}bold]COMPLETED[/bold][/color].

    {"["}bold]Current Status:[/bold]
    {$statuses}

    {"["}color=#BB3232]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━[/color]
    {"["}italic]Nanotrasen Central Command — Glory to the Corporation[/italic]

# Fax status labels
station-objective-fax-status-completed = {"["}color=green]COMPLETED[/color]
station-objective-fax-status-pending = {"["}color=yellow]PENDING[/color]

# Round end
station-objective-round-end-header = {"["}bold]Station Objectives:[/bold]
station-objective-round-end-completed = {"["}color=green]COMPLETED[/color]
station-objective-round-end-failed = {"["}color=red]FAILED[/color]
