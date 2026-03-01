entity-effect-guidebook-modify-disgust =
    { $chance ->
        [1] { $deltasign ->
                [1] Aumenta
                *[-1] Disminuye
            }
        *[other]
            { $deltasign ->
                [1] aumentar
                *[-1] disminuir
            }
    } el nivel de asco en { $amount }
