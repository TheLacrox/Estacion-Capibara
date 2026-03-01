# SPDX-FileCopyrightText: 2022 EmoGarbage404 <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Marat Gadzhiev <15rinkashikachi15@gmail.com>
# SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 Andrew <blackledgecreates@gmail.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 icekot8 <93311212+icekot8@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

## UI
cargo-console-menu-title = Consola de solicitud de carga
cargo-console-menu-flavor-left = ¡Pide aún más cajas de pizza de lo normal!
cargo-console-menu-flavor-right = v2.1
cargo-console-menu-account-name-label = Cuenta:{" "}
cargo-console-menu-account-name-none-text = Ninguna
cargo-console-menu-account-name-format = [bold][color={$color}]{$name}[/color][/bold] [font="Monospace"]\[{$code}\][/font]
cargo-console-menu-shuttle-name-label = Nombre de la lanzadera:{" "}
cargo-console-menu-shuttle-name-none-text = Ninguna
cargo-console-menu-points-label = Saldo:{" "}
cargo-console-menu-points-amount = ${$amount}
cargo-console-menu-shuttle-status-label = Estado de la lanzadera:{" "}
cargo-console-menu-shuttle-status-away-text = Ausente
cargo-console-menu-order-capacity-label = Capacidad de pedidos:{" "}
cargo-console-menu-call-shuttle-button = Activar telepad
cargo-console-menu-permissions-button = Permisos
cargo-console-menu-categories-label = Categorías:{" "}
cargo-console-menu-search-bar-placeholder = Buscar
cargo-console-menu-requests-label = Solicitudes
cargo-console-menu-orders-label = Pedidos
cargo-console-menu-order-row-title = {$productName} (x{$orderAmount} por {$orderPrice}$)
cargo-console-menu-populate-orders-cargo-order-row-product-name-text = Solicitado por: {$orderRequester} de [color={$accountColor}]{$account}[/color]
cargo-console-menu-order-row-product-description = Motivo: {$orderReason}
cargo-console-menu-order-row-button-approve = Aprobar
cargo-console-menu-order-row-button-cancel = Cancelar
cargo-console-menu-order-row-alerts-reason-absent = El motivo no está especificado
cargo-console-menu-order-row-alerts-requester-unknown = Desconocido
cargo-console-menu-populate-categories-all-text = Todo
cargo-console-menu-tab-title-orders = Pedidos
cargo-console-menu-tab-title-funds = Transferencias
cargo-console-menu-account-action-transfer-limit = Límite de transferencia:
cargo-console-menu-account-action-transfer-limit-amount = ${$amount}
cargo-console-menu-account-action-transfer-limit-unlimited-notifier = [color=gold](Ilimitado)[/color]
cargo-console-menu-account-action-select = [bold]Acción de cuenta:[/bold]
cargo-console-menu-account-action-amount = [bold]Cantidad:[/bold] $
cargo-console-menu-account-action-button = Transferir
cargo-console-menu-toggle-account-lock-button = Activar/desactivar límite de transferencia
cargo-console-menu-account-action-option-withdraw = Retirar efectivo
cargo-console-menu-account-action-option-transfer = Transferir fondos a {$code}

# Orders
cargo-console-order-not-allowed = Acceso no permitido
cargo-console-station-not-found = No hay estación disponible
cargo-console-invalid-product = ID de producto inválido
cargo-console-too-many = Demasiados pedidos aprobados
cargo-console-snip-snip = Pedido recortado a la capacidad
cargo-console-insufficient-funds = Fondos insuficientes (se requieren {$cost})
cargo-console-unfulfilled = No hay espacio para cumplir el pedido
cargo-console-trade-station = Enviado a {$destination}
cargo-console-unlock-approved-order-broadcast = [bold]{$productName} x{$orderAmount}[/bold], que costó [bold]{$cost}[/bold], fue aprobado por [bold]{$approver}[/bold]
cargo-console-fund-withdraw-broadcast = [bold]{$name} retiró {$amount} spesos de {$name1} \[{$code1}\]
cargo-console-fund-transfer-broadcast = [bold]{$name} transfirió {$amount} spesos de {$name1} \[{$code1}\] a {$name2} \[{$code2}\][/bold]
cargo-console-fund-transfer-user-unknown = Desconocido

# GoobStation - cooldown on Cargo Orders (specifically gamba)
cargo-console-cooldown-count = No se puede pedir más de un {$product} a la vez.
cargo-console-cooldown-active = Los pedidos de {$product} no se pueden realizar durante otros {$timeCount} {$timeUnits}.

cargo-console-paper-reason-default = Ninguno
cargo-console-paper-approver-default = Propio
cargo-console-paper-print-name = Pedido #{$orderNumber}
cargo-console-paper-print-text = [head=2]Pedido #{$orderNumber}[/head]
    {"[bold]Artículo:[/bold]"} {$itemName} (x{$orderQuantity})
    {"[bold]Solicitado por:[/bold]"} {$requester}

    {"[head=3]Información del pedido[/head]"}
    {"[bold]Pagador[/bold]:"} {$account} [font="Monospace"]\[{$accountcode}\][/font]
    {"[bold]Aprobado por:[/bold]"} {$approver}
    {"[bold]Motivo:[/bold]"} {$reason}

# Cargo shuttle console
cargo-shuttle-console-menu-title = Consola de la lanzadera de carga
cargo-shuttle-console-station-unknown = Desconocida
cargo-shuttle-console-shuttle-not-found = No encontrada
cargo-shuttle-console-organics = Se han detectado formas de vida orgánica en la lanzadera
cargo-no-shuttle = ¡No se encontró ninguna lanzadera de carga!

# Funding allocation console
cargo-funding-alloc-console-menu-title = Consola de asignación de fondos
cargo-funding-alloc-console-label-account = [bold]Cuenta[/bold]
cargo-funding-alloc-console-label-code = [bold] Código [/bold]
cargo-funding-alloc-console-label-balance = [bold] Saldo [/bold]
cargo-funding-alloc-console-label-cut = [bold] División de ingresos (%) [/bold]

cargo-funding-alloc-console-label-primary-cut = Porcentaje de carga de fondos de fuentes sin caja fuerte (%):
cargo-funding-alloc-console-label-lockbox-cut = Porcentaje de carga de fondos de ventas en caja fuerte (%):

cargo-funding-alloc-console-label-help-non-adjustible = Carga recibe {$percent}% de las ganancias de ventas sin caja fuerte. El resto se divide según lo especificado a continuación:
cargo-funding-alloc-console-label-help-adjustible = Los fondos restantes de fuentes sin caja fuerte se distribuyen según lo especificado a continuación:
cargo-funding-alloc-console-button-save = Guardar cambios
cargo-funding-alloc-console-label-save-fail = [bold]¡Divisiones de ingresos inválidas![/bold] [color=red]({$pos ->
    [1] +
    *[-1] -
}{$val}%)[/color]

# Slip template
cargo-acquisition-slip-body = [head=3]Detalle del activo[/head]
    {"[bold]Producto:[/bold]"} {$product}
    {"[bold]Descripción:[/bold]"} {$description}
    {"[bold]Coste unitario:[/bold"}] ${$unit}
    {"[bold]Cantidad:[/bold]"} {$amount}
    {"[bold]Coste:[/bold]"} ${$cost}

    {"[head=3]Detalle de la compra[/head]"}
    {"[bold]Comprador:[/bold]"} {$orderer}
    {"[bold]Motivo:[/bold]"} {$reason}
