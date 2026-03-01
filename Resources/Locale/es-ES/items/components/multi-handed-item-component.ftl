# SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

multi-handed-item-pick-up-fail = {$number ->
    [one] Necesitas una mano libre más para recoger { THE($item) }.
    *[other] Necesitas { $number } manos libres más para recoger { THE($item) }.
}
