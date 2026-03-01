# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

cmd-mapping-desc = Crea o carga un mapa y te teletransporta a él.
cmd-mapping-help = Uso: mapping [IDMapa] [Ruta] [Cuadrícula]
cmd-mapping-server = Solo los jugadores pueden usar este comando.
cmd-mapping-error = Se produjo un error al crear el nuevo mapa.
cmd-mapping-try-grid = No se pudo cargar el archivo como mapa. Intentando cargar el archivo como cuadrícula...
cmd-mapping-success-load = Se creó un mapa no inicializado desde el archivo {$path} con id {$mapId}.
cmd-mapping-success-load-grid = Se cargó una cuadrícula no inicializada desde el archivo {$path} en un nuevo mapa con id {$mapId}.
cmd-mapping-success = Se creó un mapa no inicializado con id {$mapId}.
cmd-mapping-warning = ADVERTENCIA: El servidor usa una versión de depuración. Corres el riesgo de perder tus cambios.


# duplicate text from engine load/save map commands.
# I CBF making this PR depend on that one.
cmd-mapping-failure-integer = {$arg} no es un entero válido.
cmd-mapping-failure-float = {$arg} no es un número decimal válido.
cmd-mapping-failure-bool = {$arg} no es un booleano válido.
cmd-mapping-nullspace = No puedes cargar en el mapa 0.
cmd-hint-mapping-id = [IDMapa]
cmd-mapping-hint-grid = [Cuadrícula]
cmd-hint-mapping-path = [Ruta]
cmd-mapping-exists = El mapa {$mapId} ya existe.
