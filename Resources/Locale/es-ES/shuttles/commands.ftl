# SPDX-FileCopyrightText: 2024 IProduceWidgets <107586145+IProduceWidgets@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# FTLdiskburner
cmd-ftldisk-desc = Crea un disco de coordenadas FTL para viajar al mapa en el que se encuentra el EntityID indicado
cmd-ftldisk-help = ftldisk [EntityID]

cmd-ftldisk-no-transform = ¡La entidad {$destination} no tiene componente Transform!
cmd-ftldisk-no-map = ¡La entidad {$destination} no tiene mapa!
cmd-ftldisk-no-map-comp = La entidad {$destination} está de algún modo en el mapa {$map} sin componente de mapa.
cmd-ftldisk-map-not-init = ¡La entidad {$destination} está en el mapa {$map} que no está inicializado! Comprueba que es seguro inicializarlo, luego inicializa el mapa primero o los jugadores quedarán atascados.
cmd-ftldisk-map-paused = ¡La entidad {$desintation} está en el mapa {$map} que está en pausa! Por favor, despausa el mapa primero o los jugadores quedarán atascados.
cmd-ftldisk-planet = La entidad {$desintation} está en el mapa de planeta {$map} y requerirá un punto FTL. Es posible que ya exista.
cmd-ftldisk-already-dest-not-enabled = La entidad {$destination} está en el mapa {$map} que ya tiene un FTLDestinationComponent, ¡pero no está habilitado! Configúralo manualmente por seguridad.
cmd-ftldisk-requires-ftl-point = La entidad {$destination} está en el mapa {$map} que requiere un punto FTL para viajar a él. Es posible que ya exista.

cmd-ftldisk-hint = ID de red del mapa
