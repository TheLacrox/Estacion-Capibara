# SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 Riggle <27156122+RigglePrime@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
# SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
# SPDX-FileCopyrightText: 2024 beck-thompson <107373427+beck-thompson@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

# UI
admin-notes-title = Notas de {$player}
admin-notes-new-note = Nueva nota
admin-notes-show-more = Mostrar más
admin-notes-for = Nota para: {$player}
admin-notes-id = Id: {$id}
admin-notes-type = Tipo: {$type}
admin-notes-severity = Gravedad: {$severity}
admin-notes-secret = Secreta
admin-notes-notsecret = No secreta
admin-notes-expires = Expira el: {$expires}
admin-notes-expires-never = No expira
admin-notes-edited-never = Nunca
admin-notes-round-id = Id de ronda: {$id}
admin-notes-round-id-unknown = Id de ronda: Desconocido
admin-notes-created-by = Creada por: {$author}
admin-notes-created-at = Creada el: {$date}
admin-notes-last-edited-by = Última edición por: {$author}
admin-notes-last-edited-at = Última edición el: {$date}
admin-notes-edit = Editar
admin-notes-delete = Eliminar
admin-notes-hide = Ocultar
admin-notes-delete-confirm = Confirmar eliminación
admin-notes-edited = Última edición por {$author} el {$date}
admin-notes-unbanned = Desbaneado por {$admin} el {$date}
admin-notes-message-desc = [color=white]Has recibido { $count ->
    [1] un mensaje administrativo
    *[other] mensajes administrativos
} desde la última vez que jugaste en este servidor.[/color]
admin-notes-message-admin = De [bold]{ $admin }[/bold], escrito el { TOSTRING($date, "f") }:
admin-notes-message-wait = El botón de aceptar se activará en {$time} segundos.
admin-notes-message-accept = Descartar permanentemente
admin-notes-message-dismiss = Descartar por ahora
admin-notes-message-seen = Visto
admin-notes-banned-from = Baneado de
admin-notes-the-server = el servidor
admin-notes-permanently = permanentemente
admin-notes-days = {$days} días
admin-notes-hours = {$hours} horas
admin-notes-minutes = {$minutes} minutos

# Note editor UI
admin-note-editor-title-new = Creando una nueva nota para {$player}
admin-note-editor-title-existing = Editando la nota {$id} de {$player} por {$author}
admin-note-editor-pop-out = Sacar ventana
admin-note-editor-secret = ¿Secreta?
admin-note-editor-secret-tooltip = Marcar esto hará que la nota no sea visible para el jugador
admin-note-editor-type-note = Nota
admin-note-editor-type-message = Mensaje
admin-note-editor-type-watchlist = Lista de vigilancia
admin-note-editor-type-server-ban = Ban de servidor
admin-note-editor-type-role-ban = Ban de rol
admin-note-editor-severity-select = Seleccionar
admin-note-editor-severity-none = Ninguna
admin-note-editor-severity-low = Baja
admin-note-editor-severity-medium = Media
admin-note-editor-severity-high = Alta
admin-note-editor-expiry-checkbox = ¿Permanente?
admin-note-editor-expiry-checkbox-tooltip = Marca esto para que expire
admin-note-editor-expiry-label = Expira el:
admin-note-editor-expiry-label-params = Expira el: {$date} (en {$expiresIn})
admin-note-editor-expiry-label-expired = Expirada
admin-note-editor-expiry-placeholder = Introduce la fecha de expiración (aaaa-MM-dd HH:mm:ss)
admin-note-editor-submit = Enviar
admin-note-editor-submit-confirm = ¿Estás seguro?

# Verb
admin-notes-verb-text = Abrir Notas de Admin

# Watchlist and message login
admin-notes-watchlist = Lista de vigilancia de {$player}: {$message}
admin-notes-new-message = Has recibido un mensaje de administrador de {$admin}: {$message}
admin-notes-fallback-admin-name = [Sistema]

# Admin remarks
admin-remarks-command-description = Abre la página de observaciones de admin
admin-remarks-command-error = Las observaciones de admin han sido desactivadas
admin-remarks-title = Observaciones de admin

# Misc
system-user = [Sistema]
