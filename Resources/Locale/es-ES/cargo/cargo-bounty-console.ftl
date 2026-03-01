# SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Killerqu00 <47712032+Killerqu00@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 BarryNorfolk <barrynorfolkman@protonmail.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

bounty-console-menu-title = Consola de recompensas de carga
bounty-console-label-button-text = Imprimir etiqueta
bounty-console-skip-button-text = Omitir
bounty-console-time-label = Tiempo: [color=orange]{$time}[/color]
bounty-console-reward-label = Recompensa: [color=limegreen]${$reward}[/color]
bounty-console-manifest-label = Manifiesto: [color=orange]{$item}[/color]
bounty-console-manifest-entry =
    { $amount ->
        [1] {$item}
        *[other] {$item} x{$amount}
    }
bounty-console-manifest-reward = Recompensa: ${$reward}
bounty-console-description-label = [color=gray]{$description}[/color]
bounty-console-id-label = ID#{$id}

bounty-console-flavor-left = Recompensas obtenidas de distribuidores locales sin escrúpulos.
bounty-console-flavor-right = v1.4

bounty-manifest-header = [font size=14][bold]Manifiesto oficial de recompensa de carga[/bold] (ID#{$id})[/font]
bounty-manifest-list-start = Manifiesto de artículos:

bounty-console-tab-available-label = Disponibles
bounty-console-tab-history-label = Historial
bounty-console-history-empty-label = No se encontró historial de recompensas
bounty-console-history-notice-completed-label = [color=limegreen]Completada[/color]
bounty-console-history-notice-skipped-label = [color=red]Omitida[/color] por {$id}
