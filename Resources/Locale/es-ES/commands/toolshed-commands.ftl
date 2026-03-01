# SPDX-FileCopyrightText: 2023 AJCM-git <60196617+AJCM-git@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Moony <moony@hellomouse.net>
# SPDX-FileCopyrightText: 2024 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 dffdff2423 <dffdff2423@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

command-description-visualize =
    Toma la lista de entidades de entrada y las coloca en una ventana de UI para fácil navegación.
command-description-runverbas =
    Ejecuta un verbo sobre las entidades de entrada con el usuario dado.
command-description-acmd-perms =
    Devuelve los permisos de administrador del comando dado, si los tiene.
command-description-acmd-caninvoke =
    Comprueba si el jugador dado puede invocar el comando dado.
command-description-jobs-jobs =
    Devuelve todos los trabajos de una estación.
command-description-jobs-job =
    Devuelve un trabajo dado de una estación.
command-description-jobs-isinfinite =
    Devuelve verdadero si el trabajo de entrada es infinito, de lo contrario falso.
command-description-jobs-adjust =
    Ajusta el número de plazas para el trabajo dado.
command-description-jobs-set =
    Establece el número de plazas para el trabajo dado.
command-description-jobs-amount =
    Devuelve el número de plazas para el trabajo dado.
command-description-laws-list =
    Devuelve una lista de todas las entidades vinculadas por leyes.
command-description-laws-get =
    Devuelve todas las leyes de una entidad dada.
command-description-stations-list =
    Devuelve una lista de todas las estaciones.
command-description-stations-get =
    Obtiene la estación activa, si y solo si hay una única.
command-description-stations-getowningstation =
    Obtiene la estación que "posee" (contiene) una entidad dada.
command-description-stations-grids =
    Devuelve todas las cuadrículas asociadas con la estación de entrada.
command-description-stations-config =
    Devuelve la configuración asociada con la estación de entrada, si la tiene.
command-description-stations-addgrid =
    Añade una cuadrícula a la estación dada.
command-description-stations-rmgrid =
    Elimina una cuadrícula de la estación dada.
command-description-stations-rename =
    Renombra la estación dada.
command-description-stations-largestgrid =
    Devuelve la cuadrícula más grande que tiene la estación dada, si la tiene.
command-description-stations-rerollBounties =
    Borra todas las recompensas actuales de la estación y obtiene una nueva selección.
command-description-stationevent-lsprob =
    Dado un prototipo BasicStationEventScheduler, lista la probabilidad de que ocurran diferentes eventos de estación del conjunto completo con las condiciones actuales.
command-description-stationevent-lsprobtheoretical =
    Dado un prototipo BasicStationEventScheduler, número de jugadores y tiempo de ronda, lista la probabilidad de que ocurran diferentes eventos de estación basándose en el número especificado de jugadores y tiempo de ronda.
command-description-stationevent-prob =
    Dado un prototipo BasicStationEventScheduler y un prototipo de evento, devuelve la probabilidad de que ocurra un único evento de estación del conjunto completo con las condiciones actuales.
command-description-admins-active =
    Devuelve una lista de administradores activos.
command-description-admins-all =
    Devuelve una lista de TODOS los administradores, incluyendo los que se han quitado los privilegios.
command-description-marked =
    Devuelve el valor de $marked como List<EntityUid>.
command-description-rejuvenate =
    Rejuvenece las entidades dadas, restaurándolas a plena salud, eliminando efectos de estado, etc.
command-description-tag-list =
    Lista las etiquetas de las entidades dadas.
command-description-tag-with =
    Devuelve solo las entidades con la etiqueta dada de la lista de entidades canalizada.
command-description-tag-add =
    Añade una etiqueta a las entidades dadas.
command-description-tag-rm =
    Elimina una etiqueta de las entidades dadas.
command-description-tag-addmany =
    Añade una lista de etiquetas a las entidades dadas.
command-description-tag-rmmany =
    Elimina una lista de etiquetas de las entidades dadas.
command-description-polymorph =
    Transforma la entidad de entrada con el prototipo dado.
command-description-unpolymorph =
    Revierte una transformación.
command-description-solution-get =
    Obtiene la solución dada de la entidad dada.
command-description-solution-adjreagent =
    Ajusta el reactivo dado en la solución dada.
command-description-mind-get =
    Obtiene la mente de la entidad, si la tiene.
command-description-mind-control =
    Asume el control de una entidad con el jugador dado.
command-description-addaccesslog =
    Añade un registro de acceso a esta entidad. Ten en cuenta que esto omite el límite predeterminado del registro y la comprobación de pausa.
command-description-stationevent-simulate =
    Dado un prototipo BasicStationEventScheduler, N rondas, N jugadores, media de fin de ronda y desviación estándar de fin de ronda, simula N rondas en las que ocurrirán eventos e imprime las ocurrencias de cada evento al final.
command-description-xenoartifact-list =
    Lista todos los EntityUids de los artefactos generados.
command-description-xenoartifact-printMatrix =
    Imprime la matriz que muestra todas las conexiones entre nodos.
command-description-xenoartifact-totalResearch =
    Obtiene todos los puntos de investigación que se pueden extraer del artefacto actualmente.
command-description-xenoartifact-averageResearch =
    Calcula la cantidad de puntos de investigación que generará de media un xenoartefacto cuando esté completamente activado.
command-description-xenoartifact-unlockAllNodes =
    Desbloquea todos los nodos del artefacto.
command-description-jobboard-completeJob =
    Completa un trabajo de tablero de salvamento dado para la estación.
command-description-scale-set =
    Establece el tamaño del sprite de una entidad a una escala determinada (sin cambiar su fixture).
command-description-scale-get =
    Obtiene la escala del sprite de una entidad tal como la establece ScaleVisualsComponent. No incluye cambios realizados directamente en el SpriteComponent.
command-description-scale-multiply =
    Multiplica el tamaño del sprite de una entidad por un factor determinado (sin cambiar su fixture).
command-description-scale-multiplyvector =
    Multiplica el tamaño del sprite de una entidad por un vector 2D determinado (sin cambiar su fixture).
command-description-scale-multiplywithfixture =
    Multiplica el tamaño del sprite de una entidad por un factor determinado (incluyendo su fixture).
