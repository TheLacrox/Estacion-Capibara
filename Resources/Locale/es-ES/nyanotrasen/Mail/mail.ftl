# SPDX-FileCopyrightText: 2024 BombasterDS <115770678+BombasterDS@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

mail-recipient-mismatch = El nombre o el trabajo del destinatario no coinciden.
mail-invalid-access = El nombre y el trabajo del destinatario coinciden, pero el acceso no es el esperado.
mail-locked = El bloqueo antifraude no se ha retirado. Acerca el ID del destinatario.
mail-desc-far = Un paquete de correo. Desde esta distancia no puedes distinguir a quién va dirigido.
mail-desc-close = Un paquete de correo dirigido a {CAPITALIZE($name)}, {$job}.
mail-desc-fragile = Tiene una [color=red]etiqueta roja de frágil[/color].
mail-desc-priority = La [color=yellow]cinta amarilla de prioridad[/color] del bloqueo antifraude está activa. ¡Mejor entrégalo a tiempo!
mail-desc-priority-inactive = La [color=#886600]cinta amarilla de prioridad[/color] del bloqueo antifraude está inactiva.
mail-unlocked = Sistema antifraude desbloqueado.
mail-unlocked-by-emag = Sistema antifraude *BZZT*.
mail-unlocked-reward = Sistema antifraude desbloqueado. Se han añadido {$bounty} spesos a la cuenta de logística.
mail-penalty-lock = BLOQUEO ANTIFRAUDE ROTO. CUENTA BANCARIA DE LOGÍSTICA PENALIZADA CON {$credits} SPESOS.
mail-penalty-fragile = INTEGRIDAD COMPROMETIDA. CUENTA BANCARIA DE LOGÍSTICA PENALIZADA CON {$credits} SPESOS.
mail-penalty-expired = ENTREGA FUERA DE PLAZO. CUENTA BANCARIA DE LOGÍSTICA PENALIZADA CON {$credits} SPESOS.
mail-item-name-addressed = correo ({$recipient})

command-mailto-description = Pone en cola un paquete para entregarlo a una entidad. Ejemplo de uso: `mailto 1234 5678 false false`. El contenido del contenedor objetivo será transferido a un paquete de correo real.
### Frontier: add is-large description
command-mailto-help = Uso: {$command} <entityUid del destinatario> <entityUid del contenedor> [es-frágil: true o false] [es-prioritario: true o false] [es-grande: true o false, opcional]
command-mailto-no-mailreceiver = La entidad destinataria objetivo no tiene un {$requiredComponent}.
command-mailto-no-blankmail = El prototipo {$blankMail} no existe. Algo está muy mal. Contacta a un programador.
command-mailto-bogus-mail = {$blankMail} no tenía {$requiredMailComponent}. Algo está muy mal. Contacta a un programador.
command-mailto-invalid-container = La entidad contenedor objetivo no tiene un contenedor {$requiredContainer}.
command-mailto-unable-to-receive = No se pudo configurar la entidad destinataria objetivo para recibir correo. Puede que falte el ID.
command-mailto-no-teleporter-found = No se pudo asociar la entidad destinataria objetivo con ningún teletransportador de correo de la estación. El destinatario puede estar fuera de la estación.
command-mailto-success = ¡Éxito! El paquete de correo ha sido puesto en cola para el siguiente teletransporte en {$timeToTeleport} segundos.

command-mailnow = Fuerza a todos los teletransportadores de correo a entregar otra ronda de correo lo antes posible. Esto no ignorará el límite de correo sin entregar.
command-mailnow-help = Uso: {$command}
command-mailnow-success = ¡Éxito! Todos los teletransportadores de correo pronto entregarán otra ronda de correo.
