# SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 AJCM-git <60196617+AJCM-git@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Brandon Hu <103440971+Brandon-Huu@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 RiceMar1244 <138547931+RiceMar1244@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Locale for wielding items; i.e. two-handing them

wieldable-verb-text-wield = Empuñar
wieldable-verb-text-unwield = Soltar

wieldable-component-successful-wield = Empuñas { THE($item) }.
wieldable-component-failed-wield = Sueltas { THE($item) }.
wieldable-component-successful-wield-other = { CAPITALIZE(THE($user)) } empuña { THE($item) }.
wieldable-component-failed-wield-other = { CAPITALIZE(THE($user)) } suelta { THE($item) }.
wieldable-component-blocked-wield = { CAPITALIZE(THE($blocker)) } te impide empuñar { THE($item) }.

wieldable-component-no-hands = ¡No tienes suficientes manos!
wieldable-component-not-enough-free-hands = {$number ->
    [one] Necesitas una mano libre para empuñar { THE($item) }.
    *[other] Necesitas { $number } manos libres para empuñar { THE($item) }.
}
wieldable-component-not-in-hands = ¡{ CAPITALIZE(THE($item)) } no está en tus manos!

wieldable-component-requires = ¡{ CAPITALIZE(THE($item))} debe ser empuñado!

gunwieldbonus-component-examine = Esta arma tiene mayor precisión cuando se empuña con ambas manos.

gunrequireswield-component-examine = Esta arma solo puede dispararse cuando se empuña con ambas manos.
