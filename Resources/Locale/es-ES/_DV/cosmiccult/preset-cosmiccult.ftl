## TEXTO DE RONDA, ANTAGONISTA Y MODO DE JUEGO DEL CULTO CÓSMICO

cosmiccult-announcement-sender = ???

cosmiccult-title = Culto Cósmico
cosmiccult-description = Los cultistas acechan entre la tripulación.

roles-antag-cosmiccult-name = Cultista Cósmico
roles-antag-cosmiccult-description = Inicia el fin de todas las cosas mediante el engaño y el sabotaje, adoctrinando a quienes se opongan a ti.

cosmiccult-gamemode-title = El Culto Cósmico
cosmiccult-gamemode-description = Los escáneres detectan un aumento anómalo en Λ-CDM. No hay datos adicionales.

cosmiccult-vote-steward-initiator = Lo Desconocido

cosmiccult-vote-steward-title = Administración del Culto Cósmico
cosmiccult-vote-steward-briefing =
    ¡Eres el Administrador del Culto Cósmico!
    Asegúrate de que El Monumento sea colocado en un lugar seguro, y organiza al culto para garantizar vuestra victoria colectiva.
    No tienes permitido instruir a los cultistas sobre cómo usar o gastar su Entropía.

cosmiccult-vote-lone-steward-title = El Cultista Solitario
cosmiccult-vote-lone-steward-briefing =
    Estás completamente solo. Pero tu deber no ha terminado.
        Asegúrate de que El Monumento esté en un lugar seguro y termina lo que el culto empezó.

cosmiccult-finale-autocall-briefing = ¡El Monumento ha sido completado! Reúnanse, actívenlo y prepárense para el final.
cosmiccult-finale-ready = ¡Una luz aterradora surge del Monumento!
cosmiccult-finale-speedup = ¡El llamado se acelera! La energía fluye por los alrededores...

cosmiccult-finale-degen = ¡Sientes que te desintegras!
cosmiccult-finale-location = ¡Los escáneres detectan un enorme pico de Λ-CDM en {$location}!
cosmiccult-finale-cancel-begin = La fuerza de voluntad de tu mente comienza a destruir el ritual...
cosmiccult-finale-beckon-begin = Los susurros en el fondo de tu mente se intensifican...
cosmiccult-finale-beckon-success = Convocas el telón final.

cosmiccult-monument-powerdown = El Monumento cae en un silencio inquietante.

## TEXTO DE FIN DE RONDA

cosmiccult-roundend-cultist-count = {$initialCount ->
    [1] Había {$initialCount} [color=#4cabb3]Cultista Cósmico[/color].
    *[other] Había {$initialCount} [color=#4cabb3]Cultistas Cósmicos[/color].
}
cosmiccult-roundend-entropy-count = El culto sifonó {$count} Entropía.
cosmiccult-roundend-cultpop-count = Los cultistas representaron el {$count}% de la tripulación.
cosmiccult-roundend-monument-stage = {$stage ->
    [1] Por desgracia, el Monumento parece abandonado.
    [2] El Monumento avanzó, pero la finalización estaba fuera de alcance.
    [3] El Monumento fue completado.
    *[other] [color=red]Algo salió MUY mal.[/color]
}

cosmiccult-roundend-cultcomplete = [color=#4cabb3]¡Victoria completa del Culto Cósmico![/color]
cosmiccult-roundend-cultmajor = [color=#4cabb3]¡Victoria mayor del Culto Cósmico![/color]
cosmiccult-roundend-cultminor = [color=#4cabb3]¡Victoria menor del Culto Cósmico![/color]
cosmiccult-roundend-neutral = [color=yellow]¡Final neutral![/color]
cosmiccult-roundend-crewminor = [color=green]¡Victoria menor de la tripulación![/color]
cosmiccult-roundend-crewmajor = [color=green]¡Victoria mayor de la tripulación![/color]
cosmiccult-roundend-crewcomplete = [color=green]¡Victoria completa de la tripulación![/color]

cosmiccult-summary-cultcomplete = ¡Los cultistas cósmicos dieron inicio al fin!
cosmiccult-summary-cultmajor = El administrador del culto llegó a Midpoint sin ser detectado. El culto vivirá un día más.
cosmiccult-summary-cultminor = Varios cultistas llegaron a Midpoint sin ser detectados. El culto vivirá un día más.
cosmiccult-summary-neutral = Ningún cultista llegó a Midpoint, pero algunos permanecen en la estación. La lucha continúa.
cosmiccult-summary-crewminor = El Monumento no alcanzó su máxima potencia.
cosmiccult-summary-crewmajor = Todos los cultistas cósmicos fueron eliminados.
cosmiccult-summary-crewcomplete = ¡Todos los cultistas cósmicos fueron desconvertidos!

cosmiccult-elimination-shuttle-call = Según los escaneos de nuestros sensores de largo alcance, la anomalía Λ-CDM ha disminuido. Les agradecemos su prudencia. Un transbordador de emergencia ha sido llamado automáticamente a la estación para procedimientos de descontaminación e interrogatorio. ETA: {$time} {$units}.
cosmiccult-elimination-announcement = Según los escaneos de nuestros sensores de largo alcance, la anomalía Λ-CDM ha disminuido. Les agradecemos su prudencia. Ya hay un transbordador de emergencia en camino. Regresen a CentComm de forma segura para los procedimientos de descontaminación e interrogatorio.


## BRIEFINGS

cosmiccult-role-roundstart-fluff =
    Mientras te preparas para otro turno más a bordo de otra estación NanoTrasen más, ¡un conocimiento inmenso inunda repentinamente tu mente!
    Una revelación sin igual. Un fin al sufrimiento cíclico y sísifeo.
    Un suave telón final.

    Todo lo que debes hacer es darlo a conocer.

cosmiccult-role-short-briefing =
    ¡Eres un Cultista Cósmico!
    Tus objetivos están listados en el menú de personaje.
    Lee más sobre tu rol en la entrada del libro de guía.

cosmiccult-role-conversion-fluff =
    Al completarse la invocación, ¡un conocimiento inmenso inunda repentinamente tu mente!
    Una revelación sin igual. Un fin al sufrimiento cíclico y sísifeo.
    Un suave telón final.

    Todo lo que debes hacer es darlo a conocer.

cosmiccult-role-deconverted-fluff =
    Un gran vacío se apodera de tu mente. Un vacío reconfortante, aunque extraño...
    Todos los pensamientos y recuerdos de tu tiempo en el culto comienzan a desvanecerse y difuminarse.

cosmiccult-role-deconverted-briefing =
    ¡Desconvertido!
    Ya no eres un Cultista Cósmico.

cosmiccult-monument-stage1-briefing =
    El Monumento ha sido invocado.
    ¡Está ubicado en {$location}!

cosmiccult-monument-stage2-briefing =
    ¡El Monumento gana en poder!
    Su influencia afectará el espacio real en {$time} segundos.

cosmiccult-monument-stage3-briefing =
    ¡El Monumento crece en poder!
    Su influencia comenzará a superponerse con el espacio real en {$time} segundos.

## GRIETAS MALIGNAS

cosmiccult-rift-inuse = No puedes hacer esto ahora mismo.
cosmiccult-rift-invaliduser = No tienes las herramientas adecuadas para lidiar con esto.
cosmiccult-rift-chaplainoops = Empuña tu escritura sagrada.
cosmiccult-rift-alreadyempowered = Ya estás potenciado; el poder de la grieta se desperdiciaría.
cosmiccult-rift-beginabsorb = La grieta comienza a fusionarse contigo...
cosmiccult-rift-beginpurge = Tu consagración comienza a purgar la grieta maligna...

cosmiccult-rift-absorb = ¡{$NAME} absorbe la grieta, y una luz maligna potencia su cuerpo!
cosmiccult-rift-purge = ¡{$NAME} purga la grieta maligna de la realidad!



## UI / POPUP BASE

cosmiccult-ui-deconverted-title = Desconvertido
cosmiccult-ui-converted-title = Convertido
cosmiccult-ui-roundstart-title = Lo Desconocido

cosmiccult-ui-converted-text-1 =
    Has sido convertido en un Cultista Cósmico.
cosmiccult-ui-converted-text-2 =
    Ayuda al culto a alcanzar sus objetivos mientras mantienes su secreto.
    Coopera con los planes de tus compañeros cultistas.

cosmiccult-ui-roundstart-text-1 =
    ¡Eres un Cultista Cósmico!
cosmiccult-ui-roundstart-text-2 =
    Ayuda al culto a alcanzar sus objetivos mientras mantienes su secreto.
    Escucha las instrucciones de tu administrador del culto.

cosmiccult-ui-deconverted-text-1 =
    Ya no eres un Cultista Cósmico.
cosmiccult-ui-deconverted-text-2 =
    Has perdido todos los recuerdos relacionados con el Culto Cósmico.
    Si eres convertido de nuevo, estos recuerdos regresarán.

cosmiccult-ui-popup-confirm = Confirmar



## OBJETIVOS / MENÚ DE PERSONAJE

objective-issuer-cosmiccult = [bold][color=#cae8e8]Lo Desconocido[/color][/bold]

objective-cosmiccult-charactermenu = Debes dar inicio al fin de todas las cosas. Completa tus tareas para avanzar el progreso del culto.
objective-cosmiccult-steward-charactermenu = Debes dirigir al culto para dar inicio al fin de todas las cosas. Supervisa y asegura el progreso del culto.

objective-condition-entropy-title = SIFON AR ENTROPÍA
objective-condition-entropy-desc = Colectivamente sifona al menos {$count} entropía de la tripulación.
objective-condition-culttier-title = POTENCIAR EL MONUMENTO
objective-condition-culttier-desc = Asegúrate de que El Monumento alcance todo su poder.
objective-condition-victory-title = DAR INICIO AL FIN
objective-condition-victory-desc = Convoca a Lo Desconocido, y anuncia el telón final.

## ANUNCIOS DE CHAT

cosmiccult-radio-tier1-progress = El Monumento es invocado hacia la estación...

cosmiccult-announce-tier2-progress = Un entumecimiento inquietante pincha tus sentidos.
cosmiccult-announce-tier2-warning = ¡Los escáneres detectan un aumento notable en Λ-CDM! Es posible que aparezcan grietas en el espacio real en breve. Por favor, avisa al capellán de tu estación si las avistas.

cosmiccult-announce-tier3-progress = Un entumecimiento inquietante te hormiguea los sentidos.
cosmiccult-announce-tier3-warning = ¡Los escáneres detectan un aumento anormal de Λ-CDM! Informa de cualquier fenómeno antinatural a seguridad o a epistemología.

cosmiccult-announce-finale-warning = Toda la tripulación de la estación. La anomalía Λ-CDM está alcanzando el nivel supercrítico; instrumentos fallando; horizonte de eventos de transición noosfórico-a-real INMINENTE. Si no estás ya en contraprotocolo, despliégate de inmediato e intervén. Repetición: Intervenid de inmediato o moriréis.

cosmiccult-announce-victory-summon = UNA FRACCIÓN DE PODER CÓSMICO ES CONVOCADA.


## MISC

cosmiccult-spire-entropy = Una mota de entropía se condensa en la superficie de la aguja.
cosmiccult-entropy-inserted = Infundes {$count} entropía en El Monumento.
cosmiccult-entropy-unavailable = No puedes hacer eso ahora mismo.
cosmiccult-astral-ascendant = {$name}, Ascendido
cosmiccult-gear-pickup-rejection = ¡El {$ITEM} resiste el toque de {CAPITALIZE(THE($TARGET))}!
cosmiccult-gear-pickup = ¡Puedes sentirte desintegrando mientras sostienes el {$ITEM}!

# Goobstation

cult-alert-recall-shuttle = Se han detectado altas concentraciones de Λ-CDM de origen desconocido a bordo de la estación. Todas las presencias anómalas deben ser purgadas o restringidas antes de que se pueda autorizar la evacuación.

cosmiccult-rift-wasempowered = Tu cuerpo no podrá soportar ser potenciado una segunda vez...

cosmiccult-chantry-location = ¡Se ha detectado un aumento peligroso de Λ-CDM {$location}! Intercepta e interviene de inmediato.

cosmiccult-chantry-powerup = ¡La capilla vacía cobra vida con un destello!

objective-condition-conversion-title = CONVERTIR TRIPULACIÓN

objective-condition-conversion-desc = Atrae colectivamente a al menos {$count} tripulantes a la causa.

cosmiccult-announce-pre-finale-progress = Arcos de energía noosférica crepitan por la estructura crujiente de la estación. El fin se acerca.

cosmiccult-announce-pre-finale-warning = ¡Aumento crítico de Λ-CDM detectado! Estamos monitoreando la situación. Esperen nuevas instrucciones.

cosmiccult-astral-minion = {$name}, Maligno

cosmiccult-silicon-subverted-briefing =
    Una luz maligna recorre tus circuitos.
    ¡Tus leyes han sido subvertidas por el Culto Cósmico!

cosmiccult-silicon-chantry-briefing =
    ¡Has sido encarcelado en una Capilla Vacua!
    Tus compañeros de tripulación pueden liberarte dañando la capilla con armas.
    Si el ritual de la capilla se completa, te transfigurarás en un Coloso Entrópico alineado con el culto.
    El ritual se completa en {$minutesandseconds}.

cosmiccult-silicon-colossus-briefing =
    ¡Has sido transfigurado en un Coloso Entrópico!
    Como un baluarte imponente de poder maligno, aniquila a quienes se te opongan.

cult-alert-block-shuttle = Se han detectado altas concentraciones de Λ-CDM de origen desconocido a bordo de la estación. Toda presencia anómala debe ser purgada o reducida antes de poder autorizar la evacuación.
