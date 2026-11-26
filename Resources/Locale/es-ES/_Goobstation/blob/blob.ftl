# SPDX-FileCopyrightText: 2024 Fishbait <Fishbait@git.ml>
# SPDX-FileCopyrightText: 2024 fishbait <gnesse@gmail.com>
# SPDX-FileCopyrightText: 2024 lanse12 <cloudability.ez@gmail.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 GitHubUser53123 <110841413+GitHubUser53123@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Ilya246 <57039557+Ilya246@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 JohnOakman <sremy2012@hotmail.fr>
# SPDX-FileCopyrightText: 2025 Panela <107573283+AgentePanela@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

ent-SpawnPointGhostBlob = Generador de blob
    .suffix = DEPURACIÓN, Generador de Rol Fantasma
    .desc = { ent-MarkerBase.desc }
ent-MobBlobPod = Gota de Blob
    .desc = Un luchador blob ordinario.
ent-MobBlobBlobbernaut = Blobbernaut
    .desc = Un luchador blob de élite.
ent-BaseBlob = blob básico.
    .desc = { "" }
ent-NormalBlobTile = Loseta Blob Normal
    .desc = Una parte ordinaria del blob necesaria para la construcción de losetas más avanzadas.
ent-CoreBlobTile = Núcleo del Blob
    .desc = El órgano más importante del blob. Al destruir el núcleo, la infección cesará.
ent-FactoryBlobTile = Fábrica de Blob
    .desc = Genera Gotas de Blob y Blobbernauts con el tiempo.
ent-ResourceBlobTile = Blob de Recursos
    .desc = Produce recursos para el blob.
ent-NodeBlobTile = Nodo de Blob
    .desc = Una versión miniatura del núcleo que permite colocar losetas blob especiales a su alrededor.
ent-StrongBlobTile = Loseta Blob Reforzada
    .desc = Una versión reforzada de la loseta normal. No permite el paso del aire y protege contra el daño por contusión.
ent-ReflectiveBlobTile = Loseta Blob Reflectante
    .desc = Refleja los láser, pero no protege tan bien contra el daño por contusión.
    .desc = { "" }
objective-issuer-blob = Blob


ghost-role-information-blobbernaut-name = Blobbernaut
ghost-role-information-blobbernaut-description = Eres un Blobbernaut. Debes defender el núcleo del blob. Usa + o +e en el chat para hablar en la Mente Blob.

ghost-role-information-blob-name = Blob
ghost-role-information-blob-description = Eres la Infección Blob. Consume la estación.

roles-antag-blob-name = Blob
roles-antag-blob-objective = Alcanza masa crítica.

guide-entry-blob = Blob

# Popups
blob-target-normal-blob-invalid = Tipo de blob incorrecto, selecciona un blob normal.
blob-target-factory-blob-invalid = Tipo de blob incorrecto, selecciona un blob fábrica.
blob-target-node-blob-invalid = Tipo de blob incorrecto, selecciona un blob nodo.
blob-target-close-to-resource = Demasiado cerca de otro blob de recursos.
blob-target-nearby-not-node = No hay ningún blob nodo o de recursos cercano.
blob-target-close-to-node = Demasiado cerca de otro nodo.
blob-target-already-produce-blobbernaut = Esta fábrica ya ha producido un blobbernaut.
blob-cant-split = No puedes dividir el núcleo del blob.
blob-not-have-nodes = No tienes nodos.
blob-not-enough-resources = No hay suficientes recursos.
blob-help = Solo Dios puede ayudarte.
blob-swap-chem = En desarrollo.
blob-mob-attack-blob = No puedes atacar a un blob.
blob-get-resource = +{ $point }
blob-spent-resource = -{ $point }
blobberaut-not-on-blob-tile = Te estás muriendo al no estar sobre losetas blob.
carrier-blob-alert = Te quedan { $second } segundos antes de la transformación.

blob-mob-zombify-second-start = { $pod } comienza a convertirte en un zombi.
blob-mob-zombify-third-start = { $pod } comienza a convertir a { $target } en un zombi.

blob-mob-zombify-second-end = { $pod } te convierte en un zombi.
blob-mob-zombify-third-end = { $pod } convierte a { $target } en un zombi.

blobberaut-factory-destroy = fábrica destruida
blob-target-already-connected = ya conectado


# UI
blob-chem-swap-ui-window-name = Cambiar productos químicos
blob-chem-reactivespines-info = Espinas Reactivas
                                Inflige 8 de daño bruto, 8 cortante, 8 perforante y 150 de daño estructural.
blob-chem-blazingoil-info = Aceite Ardiente
                            Inflige 15 de daño por quemadura y 150 de daño estructural, prende fuego a los objetivos.
                            Te hace vulnerable al agua.
blob-chem-regenerativemateria-info = Materia Regenerativa
                                    Inflige 150 de daño estructural y 15 de daño por toxinas.
                                    El núcleo del blob regenera salud 10 veces más rápido de lo normal y los blobs de recursos generan 1 recurso extra.
blob-chem-explosivelattice-info = Retícula Explosiva
                                    Inflige 5 de daño por quemadura y 150 de daño estructural, hace explotar al objetivo e inflige 10 de daño bruto.
                                    Las esporas explotan al morir.
                                    Te vuelves inmune a las explosiones.
                                    Recibes un 50% más de daño por quemaduras y descargas eléctricas.
blob-chem-electromagneticweb-info = Red Electromagnética
                                    Inflige 20 de daño por quemadura y 150 de daño estructural, con un 20% de probabilidad de provocar un pulso EMP al atacar.
                                    Las casillas del blob provocan un pulso EMP al ser destruidas.
                                    Recibes un 25% más de daño bruto y por calor.

blob-alert-out-off-station = ¡El blob fue eliminado porque se encontraba fuera de la estación!

# Announcment
blob-alert-recall-shuttle = El transbordador de emergencia no puede ser enviado mientras haya un biorriesgo de nivel 5 en la estación.
blob-alert-detect = Brote confirmado de biorriesgo nivel 5 a bordo de la estación. Todo el personal debe contener el brote.
blob-alert-critical = Nivel de biorriesgo crítico. Los códigos de autenticación nuclear han sido enviados a la estación. El Mando Central ordena a todo el personal restante que active el mecanismo de autodestrucción.
blob-alert-critical-NoNukeCode = Nivel de biorriesgo crítico. El Mando Central ordena a todo el personal restante que busque refugio y espere el rescate.
blob-alert-shuttle-arrived = Biorriesgo detectado a bordo. Todo el personal debe evacuar inmediatamente.

# Actions
blob-teleport-to-node-action-name = Saltar a Nodo (0)
blob-teleport-to-node-action-desc = Te teletransporta a un nodo blob aleatorio.
blob-help-action-name = Ayuda
blob-help-action-desc = Obtén información básica sobre cómo jugar como blob.

# Ghost role
blob-carrier-role-name = Portador de blob
blob-carrier-role-desc = Una criatura infectada por el blob.
blob-carrier-role-rules = Eres un antagonista. Tienes 10 minutos antes de transformarte en un blob.
                        Usa este tiempo para encontrar un lugar seguro en la estación. Ten en cuenta que serás muy débil justo después de la transformación.
blob-carrier-role-greeting = Eres portador del Blob. Encuentra un lugar apartado en la estación y transfórmate en Blob. Convierte la estación en una masa y a sus habitantes en tus siervos. Todos somos Blobs.

# Verbs
blob-pod-verb-zombify = Zombificar
blob-verb-upgrade-to-strong = Mejorar a Blob Reforzado
blob-verb-upgrade-to-reflective = Mejorar a Blob Reflectante
blob-verb-remove-blob-tile = Eliminar Blob

# Alerts
blob-resource-alert-name = Recursos del Núcleo
blob-resource-alert-desc = Tus recursos producidos por el núcleo y los blobs de recursos. Úsalos para expandirte y crear blobs especiales.
blob-health-alert-name = Salud del Núcleo
blob-health-alert-desc = La salud de tu núcleo. Morirás si llega a cero.

# Greeting
blob-role-greeting =
    Eres un blob - una criatura espacial parasitaria capaz de destruir estaciones enteras.
        Tu objetivo es sobrevivir y crecer lo máximo posible.
        Eres casi invulnerable al daño físico, pero el calor aún puede hacerte daño.
        Usa Alt+BIR para mejorar las losetas blob normales a reforzadas y las reforzadas a reflectantes.
        Asegúrate de colocar blobs de recursos para generar recursos.
        Ten en cuenta que los blobs de recursos y las fábricas solo funcionan cuando están junto a blobs nodo o núcleos.
        Puedes usar + o +e en el chat para usar la Mente Blob y hablar con tus lacayos.
blob-zombie-greeting = Fuiste infectado y resucitado por una espora blob. Ahora debes ayudar al blob a tomar el control de la estación. Usa +e en el chat para hablar en la Mente Blob.

# End round
blob-round-end-result =
    { $blobCount ->
        [one] Hubo una infección de blob.
        *[other] Hubo {$blobCount} blobs.
    }

blob-user-was-a-blob = [color=gray]{$user}[/color] era un blob.
blob-user-was-a-blob-named = [color=White]{$name}[/color] ([color=gray]{$user}[/color]) era un blob.
blob-was-a-blob-named = [color=White]{$name}[/color] era un blob.

preset-blob-objective-issuer-blob = [color=#33cc00]Blob[/color]

blob-user-was-a-blob-with-objectives = [color=gray]{$user}[/color] era un blob con los siguientes objetivos:
blob-user-was-a-blob-with-objectives-named = [color=White]{$name}[/color] ([color=gray]{$user}[/color]) era un blob con los siguientes objetivos:
blob-was-a-blob-with-objectives-named = [color=White]{$name}[/color] era un blob con los siguientes objetivos:

# Objectivies
objective-condition-blob-capture-title = Tomar el control de la estación
objective-condition-blob-capture-description = Tu único objetivo es tomar el control de toda la estación. Necesitas tener al menos {$count} losetas blob.
objective-condition-success = { $condition } | [color={ $markupColor }]¡Éxito![/color]
objective-condition-fail = { $condition } | [color={ $markupColor }]¡Fracaso![/color] ({ $progress }%)

# Admin Verbs

admin-verb-make-blob = Convierte al objetivo en un portador de blob.
admin-verb-text-make-blob = Convertir en Portador de Blob

# Language
language-Blob-name = Blob
chat-language-Blob-name = Blob
language-Blob-description = ¡Bleeb bob! ¡Blob blob!

blob-chem-comatosefiber-info = Fibra Comatosa
                                Inflige 22 de daño por asfixia y 150 de daño estructural.

blob-chem-chaincoating-info = Revestimiento en Cadena
                                Inflige 12 de daño cortante y contundente, y 150 de daño estructural.
                                El blob se vuelve más resistente a todos los tipos de daño.
                                La producción de recursos se reduce.

blob-chem-sinewytendons-info = Tendones Fibrosos
                                Cura 8 de daño contundente, cortante, por veneno, perforante, por asfixia, por frío y por quemaduras con cada golpe.
                                La frecuencia de pulso aumenta, el número máximo de vainas aumenta de 3 a 5.
                                Las fábricas y los blobs de recursos son más baratos.

blob-chem-corrosiveslime-info = Baba Corrosiva
                                Inflige 320 de daño estructural, 2 de daño celular y 13 de daño por ácido.
                                El precio de los nodos del blob aumenta de 50 a 100.

blob-chem-cryogenicpoison-info = Veneno Criogénico
                                Inflige 16 de daño por frío, 4 de daño cortante y 100 de daño estructural.

blob-alert-block-shuttle = No se puede enviar el transbordador de emergencia mientras haya un riesgo biológico de nivel 5 en la estación.
