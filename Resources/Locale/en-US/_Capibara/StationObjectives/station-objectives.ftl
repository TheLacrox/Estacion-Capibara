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

# Objective descriptions
station-objective-power-production-desc = Produce 10 GW of sustained power for at least 1 minute.
station-objective-cargo-export-desc = Earn at least 50,000 credits in net cargo exports.
station-objective-research-points-desc = Accumulate at least 25,000 research points.
station-objective-botany-harvest-desc = Harvest at least 30 plants.

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
