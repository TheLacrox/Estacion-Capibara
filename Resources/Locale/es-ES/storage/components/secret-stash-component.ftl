# SPDX-FileCopyrightText: 2021 Alex Evgrashin <aevgrashin@yandex.ru>
# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 metalgearsloth <comedian_vs_clown@hotmail.com>
# SPDX-FileCopyrightText: 2022 Morb <14136326+Morb0@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 beck-thompson <107373427+beck-thompson@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 brainfood1183 <113240905+brainfood1183@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

### Secret stash component. Stuff like potted plants, comfy chair cushions, etc...

comp-secret-stash-action-hide-success = Escondes { THE($item) } en el {$stashname}.
comp-secret-stash-action-hide-container-not-empty = ¡Ya hay algo aquí dentro!
comp-secret-stash-action-hide-item-too-big = { CAPITALIZE(THE($item)) } es demasiado grande para caber en el {$stashname}.
comp-secret-stash-action-get-item-found-something = ¡Había algo dentro del {$stashname}!
comp-secret-stash-on-examine-found-hidden-item = ¡Hay algo escondido dentro del {$stashname}!
comp-secret-stash-on-destroyed-popup = ¡Algo cae del {$stashname}!

### Verbs
comp-secret-stash-verb-insert-into-stash = Guardar objeto
comp-secret-stash-verb-insert-message-item-already-inside = Ya hay un objeto dentro del {$stashname}.
comp-secret-stash-verb-insert-message-no-item = Esconder { THE($item) } en el {$stashname}.
comp-secret-stash-verb-take-out-item = Coger objeto
comp-secret-stash-verb-take-out-message-something = Sacar el contenido del {$stashname}.
comp-secret-stash-verb-take-out-message-nothing = No hay nada dentro del {$stashname}.

comp-secret-stash-verb-close = Cerrar
comp-secret-stash-verb-cant-close = No puedes cerrar el {$stashname} con eso.
comp-secret-stash-verb-open = Abrir

### Stash names
secret-stash-plant = planta
secret-stash-toilet = cisterna del inodoro
secret-stash-plushie = peluche
secret-stash-cake = pastel
