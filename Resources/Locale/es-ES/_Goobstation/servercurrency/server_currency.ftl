# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 SX-7 <92227810+SX-7@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

server-currency-name-singular = Goob Coin
server-currency-name-plural = Goob Coins

## Commands

server-currency-gift-command = gift
server-currency-gift-command-description = Regala parte de tu saldo a otro jugador.
server-currency-gift-command-help = Uso: gift <jugador> <cantidad>
server-currency-gift-command-error-1 = ¡No puedes regalarte a ti mismo!
server-currency-gift-command-error-2 = ¡No tienes suficiente saldo para este regalo! Tienes un saldo de {$balance}.
server-currency-gift-command-giver = Le diste {$amount} a {$player}.
server-currency-gift-command-reciever = {$player} te dio {$amount}.

server-currency-balance-command = balance
server-currency-balance-command-description = Devuelve tu saldo.
server-currency-balance-command-help = Uso: balance
server-currency-balance-command-return = Tienes {$balance}.

server-currency-add-command = balance:add
server-currency-add-command-description = Añade moneda al saldo de un jugador.
server-currency-add-command-help = Uso: balance:add <jugador> <cantidad>

server-currency-remove-command = balance:rem
server-currency-remove-command-description = Elimina moneda del saldo de un jugador.
server-currency-remove-command-help = Uso: balance:rem <jugador> <cantidad>

server-currency-set-command = balance:set
server-currency-set-command-description = Establece el saldo de un jugador.
server-currency-set-command-help = Uso: balance:set <jugador> <cantidad>

server-currency-get-command = balance:get
server-currency-get-command-description = Obtiene el saldo de un jugador.
server-currency-get-command-help = Uso: balance:get <jugador>

server-currency-command-completion-1 = Nombre de usuario
server-currency-command-completion-2 = Cantidad
server-currency-command-error-1 = No se puede encontrar un jugador con ese nombre.
server-currency-command-error-2 = El valor debe ser un número entero.
server-currency-command-return = {$player} tiene {$balance}.

# 65% Update

gs-balanceui-title = Tienda
gs-balanceui-confirm = Confirmar

gs-balanceui-gift-label = Transferir:
gs-balanceui-gift-player = Jugador
gs-balanceui-gift-player-tooltip = Ingresa el nombre del jugador al que quieres enviarle dinero
gs-balanceui-gift-value = Cantidad
gs-balanceui-gift-value-tooltip = Cantidad de dinero a transferir

gs-balanceui-shop-label = Tienda de fichas
gs-balanceui-shop-empty = ¡Sin existencias!
gs-balanceui-shop-buy = Comprar
gs-balanceui-shop-footer = ⚠ Ahelp para usar tu ficha. Solo 1 uso por día.

gs-balanceui-shop-token-label = Fichas
gs-balanceui-shop-tittle-label = Títulos

gs-balanceui-shop-buy-token-antag = Comprar una Ficha de Antagonista - {$price} Goob Coins
gs-balanceui-shop-buy-token-admin-abuse = Comprar una Ficha de Abuso de Admin - {$price} Goob Coins
gs-balanceui-shop-buy-token-hat = Comprar una Ficha de Sombrero - {$price} Goob Coins

gs-balanceui-shop-token-antag = Ficha de Antagonista de Alto Nivel
gs-balanceui-shop-token-admin-abuse = Ficha de Abuso de Admin
gs-balanceui-shop-token-hat = Ficha de Sombrero

gs-balanceui-shop-buy-token-antag-desc = Te permite convertirte en cualquier antagonista. (Excluyendo a los Magos)
gs-balanceui-shop-buy-token-admin-abuse-desc = Te permite pedir a un admin que abuse de sus poderes contra ti. Se anima a los admins a desatar su imaginación.
gs-balanceui-shop-buy-token-hat-desc = Un admin te dará un sombrero aleatorio.

gs-balanceui-admin-add-label = Añadir (o restar) dinero:
gs-balanceui-admin-add-player = Nombre del jugador
gs-balanceui-admin-add-value = Cantidad

gs-balanceui-remark-token-antag = Compró una ficha de antagonista.
gs-balanceui-remark-token-admin-abuse = Compró una ficha de abuso de admin.
gs-balanceui-remark-token-hat = Compró una ficha de sombrero.
gs-balanceui-shop-click-confirm = Haz clic de nuevo para confirmar
gs-balanceui-shop-purchased = Comprado {$item}
