# SPDX-FileCopyrightText: 2022 Aru Moon <anton17082003@gmail.com>
# SPDX-FileCopyrightText: 2022 Julian Giebel <juliangiebel@live.de>
# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 MishaUnity <81403616+MishaUnity@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Phill101 <28949487+Phill101@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Phill101 <holypics4@gmail.com>
# SPDX-FileCopyrightText: 2024 ArchRBX <5040911+ArchRBX@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Kot <1192090+koteq@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lapatison <100279397+lapatison@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Эдуард <36124833+Ertanic@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

device-pda-slot-component-slot-name-cartridge = Cartucho

default-program-name = Programa
notekeeper-program-name = Bloc de notas
nano-task-program-name = NanoTarea
news-read-program-name = Noticias de la estación

crew-manifest-program-name = Lista de tripulación
crew-manifest-cartridge-loading = Cargando ...

net-probe-program-name = SondaNet
net-probe-scan = ¡Escaneado {$device}!
net-probe-label-name = Nombre
net-probe-label-address = Dirección
net-probe-label-frequency = Frecuencia
net-probe-label-network = Red

log-probe-program-name = SondaLog
log-probe-scan = ¡Registros descargados de {$device}!
log-probe-label-time = Hora
log-probe-label-accessor = Accedido por
log-probe-label-number = #
log-probe-print-button = Imprimir registros
log-probe-printout-device = Dispositivo escaneado: {$name}
log-probe-printout-header = Últimos registros:
log-probe-printout-entry = #{$number} / {$time} / {$accessor}

astro-nav-program-name = AstroNav

med-tek-program-name = MedTek

# Cartucho NanoTarea

nano-task-ui-heading-high-priority-tasks =
    { $amount ->
        [zero] Sin tareas de alta prioridad
        [one] 1 tarea de alta prioridad
       *[other] {$amount} tareas de alta prioridad
    }
nano-task-ui-heading-medium-priority-tasks =
    { $amount ->
        [zero] Sin tareas de prioridad media
        [one] 1 tarea de prioridad media
       *[other] {$amount} tareas de prioridad media
    }
nano-task-ui-heading-low-priority-tasks =
    { $amount ->
        [zero] Sin tareas de baja prioridad
        [one] 1 tarea de baja prioridad
       *[other] {$amount} tareas de baja prioridad
    }
nano-task-ui-done = Hecho
nano-task-ui-revert-done = Deshacer
nano-task-ui-priority-low = Baja
nano-task-ui-priority-medium = Media
nano-task-ui-priority-high = Alta
nano-task-ui-cancel = Cancelar
nano-task-ui-print = Imprimir
nano-task-ui-delete = Eliminar
nano-task-ui-save = Guardar
nano-task-ui-new-task = Nueva tarea
nano-task-ui-description-label = Descripción:
nano-task-ui-description-placeholder = Consigue algo importante
nano-task-ui-requester-label = Solicitante:
nano-task-ui-requester-placeholder = Juan Nanotrasen
nano-task-ui-item-title = Editar tarea
nano-task-printed-description = [bold]Descripción[/bold]: {$description}
nano-task-printed-requester = [bold]Solicitante[/bold]: {$requester}
nano-task-printed-high-priority = [bold]Prioridad[/bold]: [color=red]Alta[/color]
nano-task-printed-medium-priority = [bold]Prioridad[/bold]: Media
nano-task-printed-low-priority = [bold]Prioridad[/bold]: Baja

# Cartucho de lista de buscados
wanted-list-program-name = Lista de buscados
wanted-list-label-no-records = Todo está bien, vaquero
wanted-list-search-placeholder = Buscar por nombre y estado

wanted-list-age-label = [color=darkgray]Edad:[/color] [color=white]{$age}[/color]
wanted-list-job-label = [color=darkgray]Trabajo:[/color] [color=white]{$job}[/color]
wanted-list-species-label = [color=darkgray]Especie:[/color] [color=white]{$species}[/color]
wanted-list-gender-label = [color=darkgray]Género:[/color] [color=white]{$gender}[/color]

wanted-list-reason-label = [color=darkgray]Razón:[/color] [color=white]{$reason}[/color]
wanted-list-unknown-reason-label = razón desconocida

wanted-list-initiator-label = [color=darkgray]Iniciador:[/color] [color=white]{$initiator}[/color]
wanted-list-unknown-initiator-label = iniciador desconocido

wanted-list-status-label = [color=darkgray]estado:[/color] {$status ->
        [suspected] [color=yellow]sospechoso[/color]
        [wanted] [color=red]buscado[/color]
        [detained] [color=#b18644]detenido[/color]
        [paroled] [color=green]en libertad condicional[/color]
        [discharged] [color=green]liberado[/color]
        *[other] ninguno
    }

wanted-list-history-table-time-col = Hora
wanted-list-history-table-reason-col = Delito
wanted-list-history-table-initiator-col = Iniciador
