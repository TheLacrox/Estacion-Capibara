# SPDX-FileCopyrightText: 2021 Alex Evgrashin <aevgrashin@yandex.ru>
# SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2021 Galactic Chimp <63882831+GalacticChimp@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later


### Mensajes de interacción

# Mostrado cuando el jugador intenta reemplazar la luz, pero no quedan luces
comp-light-replacer-missing-light = No quedan luces en {THE($light-replacer)}.

# Mostrado cuando el jugador inserta una bombilla dentro del reemplazador de luces
comp-light-replacer-insert-light = Introduces {$bulb} en {THE($light-replacer)}.

# Mostrado cuando el jugador intenta insertar una bombilla rota en el reemplazador
comp-light-replacer-insert-broken-light = ¡No puedes insertar luces rotas!

# Mostrado cuando el jugador recarga luz desde una caja de luces
comp-light-replacer-refill-from-storage = Recargas {THE($light-replacer)}.

### Examinar

comp-light-replacer-no-lights = Está vacío.
comp-light-replacer-has-lights = Contiene lo siguiente:
comp-light-replacer-light-listing = {$amount ->
    [one] [color=yellow]{$amount}[/color] [color=gray]{$name}[/color]
    *[other] [color=yellow]{$amount}[/color] [color=gray]{$name}s[/color]
}
