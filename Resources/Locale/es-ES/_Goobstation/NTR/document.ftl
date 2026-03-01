# templates
# service
ntr-document-service-starting-text1 = [color=#009100]█▄ █ ▀█▀    [head=3]Documento NanoTrasen[/head]
    █ ▀█     █        Para: Departamento de Servicio
                           De: CentComm
                           Emitido: {$date}
    ──────────────────────────────────────────[/color]

# security
ntr-document-security-starting-text1 = [head=3]Documento NanoTrasen[/head]                               [color=#990909]█▄ █ ▀█▀
    Para: Departamento de Seguridad                                       █ ▀█     █
    De: CentComm
    Emitido: {$date}
    ──────────────────────────────────────────[/color]

# cargo
ntr-document-cargo-starting-text1 = [head=3]  NanoTrasen[/head]        [color=#d48311]█▄ █ ▀█▀ [/color][bold]      Para: Departamento de Cargo[/bold][head=3]
       Documento[/head]           [color=#d48311]█ ▀█     █       [/color] [bold]   De: CentComm[/bold]
    ──────────────────────────────────────────
                                        Emitido: {$date}

# medical
ntr-document-medical-starting-text1 = [color=#118fd4]░             █▄ █ ▀█▀    [head=3]Documento NanoTrasen[/head]                 ░
    █             █ ▀█     █        Para: Departamento Médico                         █
    ░                                    De: CentComm                                     ░
                                         Emitido: {$date}
    ──────────────────────────────────────────[/color]

# engineering
ntr-document-engineering-starting-text1 = [color=#a15000]█▄ █ ▀█▀    [head=3]Documento NanoTrasen[/head]
    █ ▀█     █        Para: Departamento de Ingeniería
                           De: CentComm
                           Emitido: {$date}
    ──────────────────────────────────────────[/color]

# science
ntr-document-science-starting-text1 = [color=#94196f]░             █▄ █ ▀█▀    [head=3]Documento NanoTrasen[/head]                 ░
    █             █ ▀█     █        Para: Departamento de Ciencia                         █
    ░                                    De: CentComm                                     ░
                                         Emitido: {$date}
    ──────────────────────────────────────────[/color]
ntr-document-service-document-text =
    {$start}
    La corporación quiere que sepas que no eres {$text1} {$text2}
    A la corporación le agradaría si {$text3}
    Los sellos a continuación confirman que {$text4}

ntr-document-security-document-text =
    {$start}
    La corporación quiere que revises algunas cosas antes de sellar este documento, asegúrate de que {$text1} {$text2}
    {$text3}
    {$text4}

ntr-document-cargo-document-text =
    {$start}
    {$text1}
    {$text2}
    Al sellar aquí, {$text3}

ntr-document-medical-document-text =
    {$start}
    {$text1} {$text2}
    {$text3}
    Al sellar aquí, {$text4}

ntr-document-engineering-document-text =
    {$start}
    {$text1} {$text2}
    {$text3}
    Al sellar aquí, {$text4}

ntr-document-science-document-text =
    {$start}
    Hemos estado monitoreando de cerca el Departamento de Investigación. {$text1} {$text2}
    debido a todo lo anterior, queremos que te asegures de {$text3}
    los sellos a continuación confirman {$text4}
