entity-effect-guidebook-spawn-entity =
    { $chance ->
        [1] Crea
        *[other] crear
    } { $amount ->
        [1] {INDEFINITE($entname)}
        *[other] {$amount} {MAKEPLURAL($entname)}
    }

entity-effect-guidebook-destroy =
    { $chance ->
        [1] Destruye
        *[other] destruir
    } el objeto

entity-effect-guidebook-break =
    { $chance ->
        [1] Rompe
        *[other] romper
    } el objeto

entity-effect-guidebook-explosion =
    { $chance ->
        [1] Causa
        *[other] causar
    } una explosión

entity-effect-guidebook-emp =
    { $chance ->
        [1] Causa
        *[other] causar
    } un pulso electromagnético

entity-effect-guidebook-flash =
    { $chance ->
        [1] Causa
        *[other] causar
    } un destello cegador

entity-effect-guidebook-foam-area =
    { $chance ->
        [1] Crea
        *[other] crear
    } grandes cantidades de espuma

entity-effect-guidebook-smoke-area =
    { $chance ->
        [1] Crea
        *[other] crear
    } grandes cantidades de humo

entity-effect-guidebook-satiate-thirst =
    { $chance ->
        [1] Sacia
        *[other] saciar
    } { $relative ->
        [1] la sed de forma normal
        *[other] la sed a {NATURALFIXED($relative, 3)}x la tasa normal
    }

entity-effect-guidebook-satiate-hunger =
    { $chance ->
        [1] Sacia
        *[other] saciar
    } { $relative ->
        [1] el hambre de forma normal
        *[other] el hambre a {NATURALFIXED($relative, 3)}x la tasa normal
    }

entity-effect-guidebook-health-change =
    { $chance ->
        [1] { $healsordeals ->
                [heals] Cura
                [deals] Inflige
                *[both] Modifica la salud en
             }
        *[other] { $healsordeals ->
                    [heals] curar
                    [deals] infligir
                    *[both] modificar la salud en
                 }
    } { $changes }

entity-effect-guidebook-even-health-change =
    { $chance ->
        [1] { $healsordeals ->
            [heals] Cura uniformemente
            [deals] Inflige uniformemente
            *[both] Modifica uniformemente la salud en
        }
        *[other] { $healsordeals ->
            [heals] curar uniformemente
            [deals] infligir uniformemente
            *[both] modificar uniformemente la salud en
        }
    } { $changes }

entity-effect-guidebook-status-effect-old =
    { $type ->
        [update]{ $chance ->
                    [1] Causa
                     *[other] causar
                 } {LOC($key)} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
        [add]   { $chance ->
                    [1] Causa
                    *[other] causar
                } {LOC($key)} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} con acumulación
        [set]  { $chance ->
                    [1] Causa
                    *[other] causar
                } {LOC($key)} durante {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
        *[remove]{ $chance ->
                    [1] Elimina
                    *[other] eliminar
                } {NATURALFIXED($time, 3)} {MANY("second", $time)} de {LOC($key)}
    }

entity-effect-guidebook-status-effect =
    { $type ->
    [update]{ $chance ->
    [1] Causa
    *[other] causar
                     } {$key} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
    [add]   { $chance ->
    [1] Causa
    *[other] causar
                    } {$key} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} con acumulación
    [set]  { $chance ->
    [1] Causa
    *[other] causar
                    } {$key} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
    *[remove]{ $chance ->
    [1] Elimina
    *[other] eliminar
                    } {NATURALFIXED($time, 3)} {MANY("second", $time)} de {$key}
        } { $delay ->
    [0] inmediatamente
    *[other] tras un retraso de {NATURALFIXED($delay, 3)} segundos
        }

reagent-effect-guidebook-status-effect-delay =
    { $type ->
        [add]   { $chance ->
                    [1] Causa
                    *[other] causar
                } {LOC($key)} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} con acumulación
        *[set]  { $chance ->
                    [1] Causa
                    *[other] causar
                } {LOC($key)} durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
        [remove]{ $chance ->
                    [1] Elimina
                    *[other] eliminar
                } {NATURALFIXED($time, 3)} {MANY("second", $time)} de {LOC($key)}
    } tras un retraso de {NATURALFIXED($delay, 3)} segundos

entity-effect-guidebook-status-effect-indef =
    { $type ->
    [update]{ $chance ->
    [1] Causa
    *[other] causar
                     } {$key} permanente
    [add]   { $chance ->
    [1] Causa
    *[other] causar
                    } {$key} permanente
    [set]  { $chance ->
    [1] Causa
    *[other] causar
                    } {$key} permanente
    *[remove]{ $chance ->
    [1] Elimina
    *[other] eliminar
                    } {$key}
        } { $delay ->
    [0] inmediatamente
    *[other] tras un retraso de {NATURALFIXED($delay, 3)} segundos
        }

entity-effect-guidebook-knockdown =
    { $type ->
        [update]{ $chance ->
                    [1] Causa
                    *[other] causar
                } derribo durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
        [add]   { $chance ->
                    [1] Causa
                    *[other] causar
                } derribo durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} con acumulación
        *[set]  { $chance ->
                    [1] Causa
                    *[other] causar
                } derribo durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)} sin acumulación
        [remove]{ $chance ->
                    [1] Elimina
                    *[other] eliminar
                } {NATURALFIXED($time, 3)} {MANY("second", $time)} de derribo
    }

entity-effect-guidebook-set-solution-temperature-effect =
    { $chance ->
        [1] Establece
        *[other] establecer
    } la temperatura de la solución a exactamente {NATURALFIXED($temperature, 2)}k

entity-effect-guidebook-adjust-solution-temperature-effect =
    { $chance ->
        [1] { $deltasign ->
                [1] Añade
                *[-1] Elimina
            }
        *[other]
            { $deltasign ->
                [1] añadir
                *[-1] eliminar
            }
    } calor { $deltasign ->
                [1] a
                *[-1] de
           } la solución hasta alcanzar { $deltasign ->
                [1] como máximo {NATURALFIXED($maxtemp, 2)}k
                *[-1] al menos {NATURALFIXED($mintemp, 2)}k
            }

entity-effect-guidebook-adjust-reagent-reagent =
    { $chance ->
        [1] { $deltasign ->
                [1] Añade
                *[-1] Elimina
            }
        *[other]
            { $deltasign ->
                [1] añadir
                *[-1] eliminar
            }
    } {NATURALFIXED($amount, 2)}u de {$reagent} { $deltasign ->
        [1] a
        *[-1] de
    } la solución

entity-effect-guidebook-adjust-reagent-group =
    { $chance ->
        [1] { $deltasign ->
                [1] Añade
                *[-1] Elimina
            }
        *[other]
            { $deltasign ->
                [1] añadir
                *[-1] eliminar
            }
    } {NATURALFIXED($amount, 2)}u de reactivos del grupo {$group} { $deltasign ->
            [1] a
            *[-1] de
        } la solución

entity-effect-guidebook-adjust-temperature =
    { $chance ->
        [1] { $deltasign ->
                [1] Añade
                *[-1] Elimina
            }
        *[other]
            { $deltasign ->
                [1] añadir
                *[-1] eliminar
            }
    } {POWERJOULES($amount)} de calor { $deltasign ->
            [1] al
            *[-1] del
        } cuerpo en el que se encuentra

entity-effect-guidebook-chem-cause-disease =
    { $chance ->
        [1] Causa
        *[other] causar
    } la enfermedad { $disease }

entity-effect-guidebook-chem-cause-random-disease =
    { $chance ->
        [1] Causa
        *[other] causar
    } las enfermedades { $diseases }

entity-effect-guidebook-jittering =
    { $chance ->
        [1] Causa
        *[other] causar
    } temblores

entity-effect-guidebook-clean-bloodstream =
    { $chance ->
        [1] Limpia
        *[other] limpiar
    } el torrente sanguíneo de otros químicos

entity-effect-guidebook-cure-disease =
    { $chance ->
        [1] Cura
        *[other] curar
    } enfermedades

entity-effect-guidebook-eye-damage =
    { $chance ->
        [1] { $deltasign ->
                [1] Inflige
                *[-1] Cura
            }
        *[other]
            { $deltasign ->
                [1] infligir
                *[-1] curar
            }
    } daño ocular

entity-effect-guidebook-vomit =
    { $chance ->
        [1] Causa
        *[other] causar
    } vómitos

entity-effect-guidebook-create-gas =
    { $chance ->
        [1] Crea
        *[other] crear
    } { $moles } { $moles ->
        [1] mol
        *[other] moles
    } de { $gas }

entity-effect-guidebook-drunk =
    { $chance ->
        [1] Causa
        *[other] causar
    } embriaguez

entity-effect-guidebook-electrocute =
    { $chance ->
        [1] Electrocuta
        *[other] electrocutar
    } al metabolizador durante {NATURALFIXED($time, 3)} {MANY("second", $time)}

entity-effect-guidebook-emote =
    { $chance ->
        [1] Obliga
        *[other] obligar
    } al metabolizador a [bold][color=white]{$emote}[/color][/bold]

entity-effect-guidebook-extinguish-reaction =
    { $chance ->
        [1] Extingue
        *[other] extinguir
    } el fuego

entity-effect-guidebook-flammable-reaction =
    { $chance ->
        [1] Aumenta
        *[other] aumentar
    } la inflamabilidad

entity-effect-guidebook-ignite =
    { $chance ->
        [1] Enciende
        *[other] encender
    } al metabolizador

entity-effect-guidebook-make-sentient =
    { $chance ->
        [1] Hace
        *[other] hacer
    } sintiente al metabolizador

entity-effect-guidebook-make-polymorph =
    { $chance ->
        [1] Transforma
        *[other] transformar
    } al metabolizador en un { $entityname }

entity-effect-guidebook-modify-bleed-amount =
    { $chance ->
        [1] { $deltasign ->
                [1] Induce
                *[-1] Reduce
            }
        *[other] { $deltasign ->
                    [1] inducir
                    *[-1] reducir
                 }
    } sangrado

entity-effect-guidebook-modify-blood-level =
    { $chance ->
        [1] { $deltasign ->
                [1] Aumenta
                *[-1] Disminuye
            }
        *[other] { $deltasign ->
                    [1] aumentar
                    *[-1] disminuir
                 }
    } el nivel de sangre

entity-effect-guidebook-paralyze =
    { $chance ->
        [1] Paraliza
        *[other] paralizar
    } al metabolizador durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)}

entity-effect-guidebook-movespeed-modifier =
    { $chance ->
        [1] Modifica
        *[other] modificar
    } la velocidad de movimiento en {NATURALFIXED($sprintspeed, 3)}x durante al menos {NATURALFIXED($time, 3)} {MANY("second", $time)}

entity-effect-guidebook-reset-narcolepsy =
    { $chance ->
        [1] Alivia
        *[other] aliviar
    } temporalmente la narcolepsia

entity-effect-guidebook-wash-cream-pie-reaction =
    { $chance ->
        [1] Limpia
        *[other] limpiar
    } el pastelazo de la cara

entity-effect-guidebook-cure-zombie-infection =
    { $chance ->
        [1] Cura
        *[other] curar
    } una infección zombi en curso

entity-effect-guidebook-cause-zombie-infection =
    { $chance ->
        [1] Contagia
        *[other] contagiar
    } la infección zombi a un individuo

entity-effect-guidebook-innoculate-zombie-infection =
    { $chance ->
        [1] Cura
        *[other] curar
    } una infección zombi en curso, y proporciona inmunidad a futuras infecciones

entity-effect-guidebook-reduce-rotting =
    { $chance ->
        [1] Regenera
        *[other] regenerar
    } {NATURALFIXED($time, 3)} {MANY("second", $time)} de putrefacción

entity-effect-guidebook-area-reaction =
    { $chance ->
        [1] Causa
        *[other] causar
    } una reacción de humo o espuma durante {NATURALFIXED($duration, 3)} {MANY("second", $duration)}

entity-effect-guidebook-add-to-solution-reaction =
    { $chance ->
        [1] Causa
        *[other] causar
    } que {$reagent} se añada a su contenedor de solución interno

entity-effect-guidebook-artifact-unlock =
    { $chance ->
        [1] Ayuda
        *[other] ayudar
        } a desbloquear un artefacto alienígena.

entity-effect-guidebook-artifact-durability-restore =
    Restaura {$restored} de durabilidad en nodos activos de artefactos alienígenas.

entity-effect-guidebook-plant-attribute =
    { $chance ->
        [1] Ajusta
        *[other] ajustar
    } {$attribute} en {$positive ->
    [true] [color=red]{$amount}[/color]
    *[false] [color=green]{$amount}[/color]
    }

entity-effect-guidebook-plant-cryoxadone =
    { $chance ->
        [1] Rejuvenece
        *[other] rejuvenecer
    } la planta, dependiendo de la edad de la planta y el tiempo de crecimiento

entity-effect-guidebook-plant-phalanximine =
    { $chance ->
        [1] Restaura
        *[other] restaurar
    } la viabilidad de una planta no viable por una mutación

entity-effect-guidebook-plant-diethylamine =
    { $chance ->
        [1] Aumenta
        *[other] aumentar
    } la esperanza de vida y/o la salud base de la planta con un 10% de probabilidad para cada una

entity-effect-guidebook-plant-robust-harvest =
    { $chance ->
        [1] Aumenta
        *[other] aumentar
    } la potencia de la planta en {$increase} hasta un máximo de {$limit}. Causa que la planta pierda sus semillas una vez que la potencia alcance {$seedlesstreshold}. Intentar añadir potencia por encima de {$limit} puede causar una disminución del rendimiento con un 10% de probabilidad

entity-effect-guidebook-plant-seeds-add =
    { $chance ->
        [1] Restaura las
        *[other] restaurar las
    } semillas de la planta

entity-effect-guidebook-plant-seeds-remove =
    { $chance ->
        [1] Elimina las
        *[other] eliminar las
    } semillas de la planta

entity-effect-guidebook-plant-mutate-chemicals =
    { $chance ->
        [1] Muta
        *[other] mutar
    } una planta para que produzca {$name}
