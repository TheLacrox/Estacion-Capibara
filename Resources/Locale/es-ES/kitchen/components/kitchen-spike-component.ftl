# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 FoLoKe <36813380+FoLoKe@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 ShadowCommander <10494922+ShadowCommander@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 mirrorcult <notzombiedude@gmail.com>
# SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 yglop <95057024+yglop@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

comp-kitchen-spike-deny-collect = { CAPITALIZE(THE($this)) } ya tiene algo en él, ¡termina de recoger su carne primero!
comp-kitchen-spike-deny-butcher = { CAPITALIZE(THE($victim)) } no puede ser despiezado en { THE($this) }.
comp-kitchen-spike-deny-changeling = { CAPITALIZE(THE($victim)) } se resiste a ser colocado en { THE($this) }.
comp-kitchen-spike-deny-absorbed = { CAPITALIZE(THE($victim)) } no tiene nada que despiezar.
comp-kitchen-spike-deny-butcher-knife = { CAPITALIZE(THE($victim)) } no puede ser despiezado en { THE($this) }, necesitas despiezarlo con un cuchillo.
comp-kitchen-spike-deny-not-dead = { CAPITALIZE(THE($victim)) } no puede ser despiezado. ¡{ CAPITALIZE(SUBJECT($victim)) } { CONJUGATE-BE($victim) } no está muerto!

comp-kitchen-spike-begin-hook-victim = ¡{ CAPITALIZE(THE($user)) } empieza a arrastrarte hacia { THE($this) }!
comp-kitchen-spike-begin-hook-self = ¡Empiezas a arrastrarte hacia { THE($this) }!

comp-kitchen-spike-kill = ¡{ CAPITALIZE(THE($user)) } ha forzado a { THE($victim) } sobre { THE($this) }, matándolo instantáneamente!

comp-kitchen-spike-suicide-other = ¡{ CAPITALIZE(THE($victim)) } se lanzó sobre { THE($this) }!
comp-kitchen-spike-suicide-self = ¡Te lanzas sobre { THE($this) }!

comp-kitchen-spike-knife-needed = Necesitas un cuchillo para hacer esto.
comp-kitchen-spike-remove-meat = Retiras algo de carne de { THE($victim) }.
comp-kitchen-spike-remove-meat-last = ¡Retiras el último trozo de carne de { THE($victim) }!

comp-kitchen-spike-meat-name = { $name } ({ $victim })
