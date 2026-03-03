<!--
SPDX-FileCopyrightText: 2017 PJB3005 <pieterjan.briers@gmail.com>
SPDX-FileCopyrightText: 2018 Pieter-Jan Briers <pieterjan.briers@gmail.com>
SPDX-FileCopyrightText: 2019 Ivan <silvertorch5@gmail.com>
SPDX-FileCopyrightText: 2019 Silver <silvertorch5@gmail.com>
SPDX-FileCopyrightText: 2020 Injazz <43905364+Injazz@users.noreply.github.com>
SPDX-FileCopyrightText: 2020 RedlineTriad <39059512+RedlineTriad@users.noreply.github.com>
SPDX-FileCopyrightText: 2020 Víctor Aguilera Puerto <zddm@outlook.es>
SPDX-FileCopyrightText: 2021 Paul Ritter <ritter.paul1@googlemail.com>
SPDX-FileCopyrightText: 2021 Swept <sweptwastaken@protonmail.com>
SPDX-FileCopyrightText: 2021 mirrorcult <lunarautomaton6@gmail.com>
SPDX-FileCopyrightText: 2022 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
SPDX-FileCopyrightText: 2022 ike709 <ike709@users.noreply.github.com>
SPDX-FileCopyrightText: 2023 iglov <iglov@avalon.land>
SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
SPDX-FileCopyrightText: 2024 Kira Bridgeton <161087999+Verbalase@users.noreply.github.com>
SPDX-FileCopyrightText: 2024 Rares Popa <2606875+rarepops@users.noreply.github.com>
SPDX-FileCopyrightText: 2024 router <messagebus@vk.com>
SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
SPDX-FileCopyrightText: 2025 Piras314 <p1r4s@proton.me>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<p align="center"> <img alt="Capibara Station" width="880" height="300" src="https://github.com/Goob-Station/Goob-Station/blob/master/Resources/Textures/Logo/logo.png" /></p>

Este es un fork de **Goob Station**, que a su vez es un fork de Space Station 14. Para evitar que se haga fork de RobustToolbox directamente, el cliente y el servidor cargan un paquete de contenido ("content pack"). Este repositorio contiene el paquete de contenido de **Capibara Station**.

Si quieres montar un servidor o crear contenido para SS14, visita el [repositorio de Space Station 14](https://github.com/space-wizards/space-station-14), que incluye tanto RobustToolbox como el paquete de contenido base para el desarrollo de nuevos forks.

## Construcción del proyecto

1. Clona este repositorio.
2. Ejecuta `RUN_THIS.py` para inicializar los submódulos y descargar el motor.
3. Compila la solución.

```bash
dotnet build
```

Para más detalles, consulta la [documentación de Goob Station](https://docs.goobstation.com/en/general-development/setup.html).

## Contribuir

¡Aceptamos contribuciones de cualquier persona! Si quieres ayudar, únete a nuestra comunidad. Puedes revisar la lista de issues pendientes y tomar cualquiera que te interese. No dudes en pedir ayuda.

Aunque no es obligatorio, recomendamos revisar las [guías de contribución de Space Station 14](https://docs.spacestation14.com/en/general-development/codebase-info/pull-request-guidelines.html) como referencia de buenas prácticas.

## Licencia

Todo el código en este repositorio se distribuye bajo la licencia **AGPL-3.0-or-later**. Cada archivo incluye cabeceras de especificación REUSE o archivos `.license` separados que especifican una opción de doble licencia. Puedes revisar los textos completos de estas licencias en el directorio `LICENSES/`.

La mayoría de los recursos multimedia están licenciados bajo [CC-BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/) salvo que se indique lo contrario. Los recursos incluyen su licencia y copyright en el archivo de metadatos. [Ejemplo](https://github.com/space-wizards/space-station-14/blob/master/Resources/Textures/Objects/Tools/crowbar.rsi/meta.json).

Ten en cuenta que algunos recursos están licenciados bajo la licencia no comercial [CC-BY-NC-SA 3.0](https://creativecommons.org/licenses/by-nc-sa/3.0/) o licencias similares, y deberán eliminarse si deseas utilizar este proyecto con fines comerciales.
