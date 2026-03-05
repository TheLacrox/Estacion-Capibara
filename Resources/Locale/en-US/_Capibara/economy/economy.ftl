# SPDX-FileCopyrightText: 2025 Capibara Station Contributors
# SPDX-License-Identifier: AGPL-3.0-or-later

## Entity Names
ent-CapibaraATM = ATM
    .desc = A machine for managing your bank account. Withdraw and deposit Spesos.
ent-CapibaraSalaryConsole = salary management console
    .desc = A console for managing crew salaries. Requires HOP or Captain authorization.

## ATM Machine UI
capibara-atm-title = ATM
capibara-atm-balance-label = Balance:
capibara-atm-amount-label = Amount:
capibara-atm-withdraw = Withdraw
capibara-atm-deposit = Deposit
capibara-atm-deposit-all = Deposit All Cash In Hands
capibara-atm-deposit-hint = You can also click cash directly on the ATM.
capibara-atm-insert = Insert ID
capibara-atm-eject = Eject ID
capibara-atm-no-id = No ID card detected
capibara-atm-id-slot = ID Slot
capibara-atm-header-account = Account
capibara-atm-header-balance = Balance
capibara-atm-flavor-left = Nanotrasen Banking Services v2.4
capibara-atm-flavor-right = NT-ATM

## ATM Popup Messages
capibara-atm-invalid-amount = Invalid amount.
capibara-atm-insufficient-funds = Insufficient funds.
capibara-atm-no-cash = You are not holding any cash.
capibara-atm-withdraw-success = Withdrew {$amount} Spesos.
capibara-atm-deposit-success = Deposited {$amount} Spesos.
capibara-atm-no-account = This ID card has no bank account.
capibara-atm-create-account = Create Bank Account
capibara-atm-account-created = Bank account created successfully!

## Vending Machine Prices
capibara-vending-balance = Balance: {$balance} Sp
capibara-vending-insufficient-funds = Insufficient funds! Costs {$price} Spesos.
capibara-vending-purchased = Purchased for {$price} Spesos.

## Payroll
capibara-payroll-received = Salary received: {$amount} Spesos. Balance: {$balance} Sp.

## Salary Management Console
capibara-salary-console-title = Salary Management Console
capibara-salary-console-insert-id = Insert ID
capibara-salary-console-eject-id = Eject ID
capibara-salary-console-id-slot = Authorization ID Slot
capibara-salary-console-not-authorized = Access denied. Insert a Head of Personnel or Captain ID card.
capibara-salary-console-insert-id-prompt = Insert HOP or Captain ID to authorize.
capibara-salary-console-authorized = Authorized: {$name}
capibara-salary-console-invalid-salary = Invalid salary amount.
capibara-salary-console-salary-changed = Your salary has been updated to {$amount} Spesos per cycle.
capibara-salary-console-set = Set
capibara-salary-console-frozen-warning = SALARIES FROZEN — Station objectives not completed.
capibara-salary-console-header-account = Acct
capibara-salary-console-header-name = Name
capibara-salary-console-header-job = Job
capibara-salary-console-header-salary = Salary
capibara-salary-console-header-balance = Balance
capibara-salary-console-header-auth = Authorization
capibara-salary-console-header-payroll = Payroll Status
capibara-salary-console-payroll-active = Active
capibara-salary-console-payroll-frozen = FROZEN
capibara-salary-console-dept-unknown = Unassigned
capibara-salary-console-dept-total = Total: {$total} Sp/cycle
capibara-salary-console-flavor-left = Nanotrasen Payroll Management System v3.7
capibara-salary-console-flavor-right = NT-PAYROLL

## Salary Objectives Integration
capibara-salary-objectives-frozen = Attention all crew: Due to failure to complete station objectives within the allotted time, all salary payments have been suspended effective immediately. No salaries will be paid until all objectives are completed.
capibara-salary-objectives-restored = Attention all crew: Station objectives have been completed. Salary payments are now restored. Thank you for your service.
capibara-salary-objectives-sender = Nanotrasen Payroll Department
