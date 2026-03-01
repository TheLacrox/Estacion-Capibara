# SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Simon <63975668+Simyon264@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# Loading Screen

replay-loading = Cargando ({$cur}/{$total})
replay-loading-reading = Leyendo Archivos
replay-loading-processing = Procesando Archivos
replay-loading-spawning = Generando Entidades
replay-loading-initializing = Inicializando Entidades
replay-loading-starting= Iniciando Entidades
replay-loading-failed = No se pudo cargar la repetición. Error:
                        {$reason}
replay-loading-retry = Intentar cargar con mayor tolerancia a excepciones - ¡PUEDE CAUSAR ERRORES!
replay-loading-cancel = Cancelar

# Main Menu
replay-menu-subtext = Cliente de Repeticiones
replay-menu-load = Cargar Repetición Seleccionada
replay-menu-select = Seleccionar una Repetición
replay-menu-open = Abrir Carpeta de Repeticiones
replay-menu-none = No se encontraron repeticiones.

# Main Menu Info Box
replay-info-title = Información de la Repetición
replay-info-none-selected = No hay repetición seleccionada
replay-info-invalid = [color=red]Repetición inválida seleccionada[/color]
replay-info-info = {"["}color=gray]Seleccionado:[/color]  {$name} ({$file})
                   {"["}color=gray]Hora:[/color]   {$time}
                   {"["}color=gray]ID de ronda:[/color]   {$roundId}
                   {"["}color=gray]Duración:[/color]   {$duration}
                   {"["}color=gray]ForkId:[/color]   {$forkId}
                   {"["}color=gray]Versión:[/color]   {$version}
                   {"["}color=gray]Motor:[/color]   {$engVersion}
                   {"["}color=gray]Hash de Tipo:[/color]   {$hash}
                   {"["}color=gray]Hash de Comp:[/color]   {$compHash}

# Replay selection window
replay-menu-select-title = Seleccionar Repetición

# Replay related verbs
replay-verb-spectate = Espectador

# command
cmd-replay-spectate-help = replay_spectate [entidad opcional]
cmd-replay-spectate-desc = Conecta o desconecta al jugador local a un uid de entidad dado.
cmd-replay-spectate-hint = EntityUid Opcional

cmd-replay-toggleui-desc = Activa o desactiva la interfaz de control de repeticiones.
