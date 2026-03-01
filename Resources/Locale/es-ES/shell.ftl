# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 moonheart08 <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 20kdc <asdd2808@gmail.com>
# SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2022 Moony <moonheart08@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 Morber <14136326+Morb0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Moony <moony@hellomouse.net>
# SPDX-FileCopyrightText: 2023 crazybrain23 <44417085+crazybrain23@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Brandon Hu <103440971+Brandon-Huu@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Simon <63975668+Simyon264@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### for technical and/or system messages

## General

shell-command-success = Comando ejecutado con éxito
shell-invalid-command = Comando inválido.
shell-invalid-command-specific = Comando {$commandName} inválido.
shell-cannot-run-command-from-server = No puedes ejecutar este comando desde el servidor.
shell-only-players-can-run-this-command = Solo los jugadores pueden ejecutar este comando.
shell-must-be-attached-to-entity = Debes estar vinculado a una entidad para ejecutar este comando.
shell-must-have-body = Debes tener un cuerpo para ejecutar este comando.

## Arguments

shell-need-exactly-one-argument = Se necesita exactamente un argumento.
shell-wrong-arguments-number-need-specific = Se necesitan {$properAmount} argumentos, se recibieron {$currentAmount}.
shell-argument-must-be-number = El argumento debe ser un número.
shell-argument-must-be-boolean = El argumento debe ser un booleano.
shell-wrong-arguments-number = Número incorrecto de argumentos.
shell-need-between-arguments = ¡Se necesitan entre {$lower} y {$upper} argumentos!
shell-need-minimum-arguments = ¡Se necesitan al menos {$minimum} argumentos!
shell-need-minimum-one-argument = ¡Se necesita al menos un argumento!
shell-need-exactly-zero-arguments = Este comando no toma argumentos.

shell-argument-uid = EntityUid

## Guards

shell-missing-required-permission = ¡Necesitas {$perm} para este comando!
shell-entity-is-not-mob = ¡La entidad objetivo no es un personaje!
shell-invalid-entity-id = ID de entidad inválido.
shell-invalid-grid-id = ID de cuadrícula inválido.
shell-invalid-map-id = ID de mapa inválido.
shell-invalid-entity-uid = {$uid} no es un uid de entidad válido
shell-invalid-bool = Booleano inválido.
shell-entity-uid-must-be-number = El EntityUid debe ser un número.
shell-could-not-find-entity = No se pudo encontrar la entidad {$entity}
shell-could-not-find-entity-with-uid = No se pudo encontrar la entidad con uid {$uid}
shell-entity-with-uid-lacks-component = La entidad con uid {$uid} no tiene un componente {$componentName}
shell-entity-target-lacks-component = La entidad objetivo no tiene un componente {$componentName}
shell-invalid-color-hex = ¡Código de color hexadecimal inválido!
shell-target-player-does-not-exist = ¡El jugador objetivo no existe!
shell-target-entity-does-not-have-message = ¡La entidad objetivo no tiene {INDEFINITE($missing)} {$missing}!
shell-timespan-minutes-must-be-correct = {$span} no es un tiempo en minutos válido.
shell-argument-must-be-prototype = ¡El argumento {$index} debe ser un {LOC($prototypeName)}!
shell-argument-number-must-be-between = ¡El argumento {$index} debe ser un número entre {$lower} y {$upper}!
shell-argument-station-id-invalid = ¡El argumento {$index} debe ser un id de estación válido!
shell-argument-map-id-invalid = ¡El argumento {$index} debe ser un id de mapa válido!
shell-argument-number-invalid = ¡El argumento {$index} debe ser un número válido!

# Hints
shell-argument-username-hint = <usuario>
shell-argument-username-optional-hint = [usuario]
