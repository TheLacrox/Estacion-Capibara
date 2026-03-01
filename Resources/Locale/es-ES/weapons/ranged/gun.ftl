# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 PixelTK <85175107+PixelTheKermit@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Errant <35878406+errant@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 MendaxxDev <153332064+MendaxxDev@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 TaralGit <76408146+TaralGit@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Vordenburg <114301317+Vordenburg@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 and_a <and_a@DESKTOP-RJENGIR>
# SPDX-FileCopyrightText: 2023 chromiumboy <50505512+chromiumboy@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Errant <35878406+Errant-4@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later


gun-selected-mode-examine = El modo de disparo seleccionado es [color={$color}]{$mode}[/color].
gun-fire-rate-examine = La cadencia de disparo es [color={$color}]{$fireRate}[/color] por segundo.
gun-selector-verb = Cambiar a {$mode}
gun-selected-mode = Seleccionado {$mode}
gun-disabled = ¡No puedes usar armas de fuego!
gun-set-fire-mode = Establecido a {$mode}
gun-magazine-whitelist-fail = ¡Eso no cabe en el arma!
gun-magazine-fired-empty = ¡Sin munición!

# SelectiveFire
gun-SemiAuto = semiautomático
gun-Burst = ráfaga
gun-FullAuto = automático

# BallisticAmmoProvider
gun-ballistic-cycle = Cargar
gun-ballistic-cycled = Cargado
gun-ballistic-cycled-empty = Cargado (vacío)
gun-ballistic-transfer-invalid = ¡{CAPITALIZE(THE($ammoEntity))} no cabe dentro de {THE($targetEntity)}!
gun-ballistic-transfer-empty = {CAPITALIZE(THE($entity))} está vacío/a.
gun-ballistic-transfer-target-full = {CAPITALIZE(THE($entity))} ya está completamente cargado/a.

# CartridgeAmmo
gun-cartridge-spent = Está [color=red]gastado[/color].
gun-cartridge-unspent = [color=lime]No está gastado[/color].

# BatteryAmmoProvider
gun-battery-examine = Tiene carga suficiente para [color={$color}]{$count}[/color] disparos.

# CartridgeAmmoProvider
gun-chamber-bolt-ammo = Arma sin cerrojo
gun-chamber-bolt = El cerrojo está [color={$color}]{$bolt}[/color].
gun-chamber-bolt-closed = Cerrojo cerrado
gun-chamber-bolt-opened = Cerrojo abierto
gun-chamber-bolt-close = Cerrar cerrojo
gun-chamber-bolt-open = Abrir cerrojo
gun-chamber-bolt-closed-state = abierto
gun-chamber-bolt-open-state = cerrado
gun-chamber-rack = Montar

# MagazineAmmoProvider
gun-magazine-examine = Tiene [color={$color}]{$count}[/color] disparos restantes.

# RevolverAmmoProvider
gun-revolver-empty = Revólver vacío
gun-revolver-full = Revólver lleno
gun-revolver-insert = Insertado
gun-revolver-spin = Girar el cilindro
gun-revolver-spun = Girado
gun-speedloader-empty = Cargador rápido vacío

# GunSpreadModifier
examine-gun-spread-modifier-reduction = La dispersión ha sido reducida un [color=yellow]{$percentage}%[/color].
examine-gun-spread-modifier-increase = La dispersión ha sido incrementada un [color=yellow]{$percentage}%[/color].
