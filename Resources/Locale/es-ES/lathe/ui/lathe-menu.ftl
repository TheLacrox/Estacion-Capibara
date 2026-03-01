# SPDX-FileCopyrightText: 2022 Eoin Mcloughlin <helloworld@eoinrul.es>
# SPDX-FileCopyrightText: 2022 Rinkashikachi <15rinkashikachi15@gmail.com>
# SPDX-FileCopyrightText: 2022 eoineoineoin <eoin.mcloughlin+gh@gmail.com>
# SPDX-FileCopyrightText: 2023 Justin <justinly@usc.edu>
# SPDX-FileCopyrightText: 2023 Thom <119594676+ItsMeThom@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 chromiumboy <50505512+chromiumboy@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Crotalus <Crotalus@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

lathe-menu-title = Menú del Torno
lathe-menu-queue = Cola
lathe-menu-server-list = Lista de servidores
lathe-menu-sync = Sincronizar
lathe-menu-search-designs = Buscar diseños
lathe-menu-category-all = Todos
lathe-menu-search-filter = Filtrar:
lathe-menu-amount = Cantidad:
lathe-menu-recipe-count = { $count ->
    [1] {$count} Receta
    *[other] {$count} Recetas
}
lathe-menu-reagent-slot-examine = Tiene una ranura para un beaker en el lateral.
lathe-reagent-dispense-no-container = ¡Líquido sale de {THE($name)} al suelo!
lathe-menu-result-reagent-display = {$reagent} ({$amount}u)
lathe-menu-material-display = {$material} ({$amount})
lathe-menu-tooltip-display = {$amount} de {$material}
lathe-menu-description-display = [italic]{$description}[/italic]
lathe-menu-material-amount = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit}
    *[other] {NATURALFIXED($amount, 2)} {MAKEPLURAL($unit)}
}
lathe-menu-material-amount-missing = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit} de {$material} ([color=red]{NATURALFIXED($missingAmount, 2)} {$unit} faltante[/color])
    *[other] {NATURALFIXED($amount, 2)} {MAKEPLURAL($unit)} de {$material} ([color=red]{NATURALFIXED($missingAmount, 2)} {MAKEPLURAL($unit)} faltantes[/color])
}
lathe-menu-no-materials-message = No hay materiales cargados.
lathe-menu-fabricating-message = Fabricando...
lathe-menu-materials-title = Materiales
lathe-menu-queue-title = Cola de Construcción
lathe-menu-queue-reset-title = Reiniciar Cola
lathe-menu-queue-reset-material-overflow = Notas que el autotorno está lleno.
