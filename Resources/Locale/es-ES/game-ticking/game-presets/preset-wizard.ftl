roles-antag-survivor-name = Superviviente

roles-antag-survivor-objective = Objetivo actual: sobrevivir

survivor-role-greeting =
    Eres un Superviviente. Ante todo, necesitas volver con vida al Comando Central.
    Reúne tanto poder de fuego como sea necesario para garantizar tu supervivencia.
    No confíes en nadie.

survivor-round-end-dead-count =
{
    $deadCount ->
        [one] Murió [color=red]{$deadCount}[/color] superviviente.
        *[other] Murieron [color=red]{$deadCount}[/color] supervivientes.
}

survivor-round-end-alive-count =
{
    $aliveCount ->
        [one] [color=yellow]{$aliveCount}[/color] superviviente quedó abandonado en la estación.
        *[other] [color=yellow]{$aliveCount}[/color] supervivientes quedaron abandonados en la estación.
}

survivor-round-end-alive-on-shuttle-count =
{
    $aliveCount ->
        [one] [color=green]{$aliveCount}[/color] superviviente escapó con vida.
        *[other] [color=green]{$aliveCount}[/color] supervivientes escaparon con vida.
}
