# Poggers examine system

examine-name = ¡Es [bold]{$name}[/bold]!
examine-name-selfaware = ¡Eres tú!
examine-can-see = Mirando a {OBJECT($ent)}, puedes ver:
examine-can-see-nothing = ¡{CAPITALIZE(SUBJECT($ent))} está completamente desnudo/a!
examine-border-line = ═════════════════════
examine-present-tex = Esto es un [enttex id="{ $id }" size={ $size }] [bold]{$name}[/bold].
examine-present = Esto es un [bold]{$name}[/bold].
examine-present-line = ═══

id-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cinturón.
head-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cabeza.
eyes-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} ojos.
mask-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cara.
neck-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cuello.
ears-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} orejas.
jumpsuit-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} que {SUBJECT($ent)} lleva puesto.
outer-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cuerpo.
suitstorage-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} hombro.
back-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} espalda.
gloves-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} manos.
belt-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} cinturón.
shoes-examine = • {CAPITALIZE(POSS-ADJ($ent))} { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en {POSS-ADJ($ent)} pies.

id-card-examine-full = • ID de {CAPITALIZE(POSS-ADJ($wearer))}: [bold]{$nameAndJob}[/bold].

# Selfaware version

examine-can-see-selfaware = Mirándote a ti mismo, puedes ver:
examine-can-see-nothing-selfaware = ¡Estás completamente desnudo/a!

id-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cinturón.
head-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cabeza.
eyes-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tus ojos.
mask-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cara.
neck-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cuello.
ears-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tus orejas.
jumpsuit-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} que llevas puesto.
outer-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cuerpo.
suitstorage-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu hombro.
back-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu espalda.
gloves-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tus manos.
belt-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tu cinturón.
shoes-examine-selfaware = • Tu { $id ->
     [empty] [bold]{$item}[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{$item}[/bold]
} en tus pies.

# Selfaware examine

comp-hands-examine-empty-selfaware = No tienes nada en las manos.
comp-hands-examine-selfaware = Tienes { $items }.

humanoid-appearance-component-examine-selfaware = Eres { INDEFINITE($age) } { $age } { $species }.
