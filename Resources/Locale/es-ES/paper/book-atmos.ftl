# SPDX-FileCopyrightText: 2023 Julian Giebel <juliangiebel@live.de>
# SPDX-FileCopyrightText: 2023 Vasilis <vascreeper@yahoo.com>
# SPDX-FileCopyrightText: 2023 Vasilis <vasilis@pikachu.systems>
# SPDX-FileCopyrightText: 2023 dontbetank <59025279+dontbetank@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

book-text-atmos-distro = La red de distribución, o "distro" para abreviar, es el sistema vital de la estación. Se encarga de transportar aire desde atmósferas por toda la estación.

        Las tuberías relevantes suelen estar pintadas de azul apagado vibrante, pero la manera más segura de identificarlas es usar un escáner de bandejas para rastrear qué tuberías están conectadas a los respiraderos activos de la estación.

        La mezcla de gas estándar de la red de distribución es de 20 grados Celsius, 78% nitrógeno, 22% oxígeno. Puedes verificarlo usando un analizador de gas en una tubería de distro o en cualquier respiradero conectado a ella. Las circunstancias especiales pueden requerir mezclas especiales.

        A la hora de decidir la presión de distro, hay varios factores a tener en cuenta. Los respiraderos activos regularán la presión de la estación, así que mientras todo funcione correctamente, no existe algo como una presión de distro demasiado alta.

        Una presión de distro mayor permitirá que la red funcione como un amortiguador entre los extractores de gas y los respiraderos, proporcionando una cantidad significativa de aire extra que puede utilizarse para repressurizar la estación tras una descompresión.

        Una presión de distro menor reducirá la cantidad de gas perdida en caso de que la distro sea espaciada, una solución rápida para lidiar con la contaminación del distro. También puede ayudar a ralentizar o prevenir la sobrepresurización de la estación en caso de problemas con los respiraderos.

        Las presiones de distro comunes están en el rango de 300-375 kPa, pero se pueden utilizar otras presiones con conocimiento de los riesgos y beneficios.

        La presión de la red está determinada por la última bomba que bombea hacia ella. Para evitar cuellos de botella, todas las demás bombas entre los extractores y la última bomba deben configurarse a su tasa máxima, y cualquier dispositivo innecesario debe ser eliminado.

        Puedes validar la presión de distro con un analizador de gas, pero ten en cuenta que una alta demanda por cosas como las descompresiones puede causar que la distro esté por debajo de la presión objetivo establecida durante períodos prolongados. Así que si ves una caída de presión, no te alarmes; puede ser temporal.

book-text-atmos-waste = La red de residuos es el sistema principal responsable de mantener el aire de la estación libre de contaminantes.

        Puedes identificar las tuberías relevantes por su color rojo apagado agradable o usando un escáner de bandejas para rastrear qué tuberías están conectadas a los depuradores de la estación.

        La red de residuos se utiliza para transportar gases de desecho para ser filtrados o espaciados. Lo ideal es mantener la presión en 0 kPa, aunque a veces puede estar a una baja presión no nula mientras está en uso.

        Los técnicos tienen la opción de filtrar o espaciar los gases de desecho. Aunque espaciar es más rápido, filtrar permite que los gases sean reutilizados para reciclaje o venta.

        La red de residuos también puede usarse para diagnosticar problemas atmosféricos en la estación. Niveles altos de un gas de desecho pueden sugerir una gran fuga, mientras que la presencia de gases no residuales puede indicar un problema de configuración del depurador o una conexión física incorrecta. Si los gases están a alta temperatura, podría indicar un incendio.

book-text-atmos-alarms = Las alarmas de aire están distribuidas por toda la estación para permitir la gestión y supervisión de la atmósfera local.

            La interfaz de alarma de aire proporciona a los técnicos una lista de sensores conectados, sus lecturas y la capacidad de ajustar umbrales. Estos umbrales se utilizan para determinar el estado de alarma de la alarma de aire. Los técnicos también pueden usar la interfaz para establecer presiones objetivo para los respiraderos y configurar las velocidades de operación y los gases objetivo para los depuradores.

            Aunque la interfaz permite el ajuste fino de los dispositivos bajo el control de la alarma de aire, también hay varios modos disponibles para la configuración rápida de la alarma. Estos modos se activan automáticamente cuando cambia el estado de alarma:
            - Filtrado: El modo predeterminado
            - Filtrado (amplio): Un modo de filtrado que modifica el funcionamiento de los depuradores para limpiar un área más amplia
            - Llenado: Desactiva los depuradores y configura los respiraderos a su presión máxima
            - Pánico: Desactiva los respiraderos y configura los depuradores en modo sifón

            Se puede usar una multitool o un configurador de red para vincular dispositivos a las alarmas de aire.

book-text-atmos-vents =
    A continuación hay una guía de referencia rápida sobre varios dispositivos atmosféricos:

                Respiraderos pasivos:
                Estos respiraderos no requieren energía; permiten que los gases fluyan libremente tanto hacia dentro como hacia fuera de la red de tuberías a la que están conectados.

                Respiraderos activos:
                Son los respiraderos más comunes de la estación. Tienen una bomba interna y requieren energía. Por defecto, solo bombean gases hacia fuera de las tuberías, y solo hasta 101 kPa. Sin embargo, pueden reconfigurarse usando una alarma de aire. También se bloquearán si la habitación está por debajo de 1 kPa, para evitar bombear gases al espacio.

                Depuradores de aire:
                Estos dispositivos permiten que los gases sean eliminados del entorno y enviados a la red de tuberías conectada. Pueden configurarse para seleccionar gases específicos cuando están conectados a una alarma de aire.

                Inyectores de aire:
                Los inyectores son similares a los respiraderos activos, pero no tienen bomba interna y no requieren energía. No pueden configurarse, pero pueden continuar bombeando gases hasta presiones mucho más altas.
