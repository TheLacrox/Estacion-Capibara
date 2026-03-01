interaction-LookAt-name = Mirar fijamente
interaction-LookAt-description = Mira fijamente al vacío y verás cómo el vacío te mira a ti.
interaction-LookAt-success-self-popup = Miras fijamente a {THE($target)}.
interaction-LookAt-success-target-popup = Sientes que {THE($user)} te mira fijamente...
interaction-LookAt-success-others-popup = {THE($user)} mira fijamente a {THE($target)}.

interaction-Hug-name = Abrazar
interaction-Hug-description = Un abrazo al día aleja los horrores psicológicos más allá de tu comprensión.
interaction-Hug-success-self-popup = Abrazas a {THE($target)}.
interaction-Hug-success-target-popup = {THE($user)} te abraza.
interaction-Hug-success-others-popup = {THE($user)} abraza a {THE($target)}.

interaction-KnockOn-name = Llamar
interaction-KnockOn-description = Llama al objetivo para atraer su atención.
interaction-KnockOn-success-self-popup = Llamas a {THE($target)}.
interaction-KnockOn-success-target-popup = {THE($user)} te llama.
interaction-KnockOn-success-others-popup = {THE($user)} llama a {THE($target)}.

# The below includes conditionals for if the user is holding an item
interaction-WaveAt-name = Saludar con la mano
interaction-WaveAt-description = Saluda al objetivo con la mano. Si sostienes un objeto, lo agitarás.
interaction-WaveAt-success-self-popup = Saludas {$hasUsed ->
    [false] con la mano a {THE($target)}.
    *[true] tu {$used} hacia {THE($target)}.
}
interaction-WaveAt-success-target-popup = {THE($user)} te saluda {$hasUsed ->
    [false] con la mano.
    *[true] con {POSS-PRONOUN($user)} {$used}.
}
interaction-WaveAt-success-others-popup = {THE($user)} saluda {$hasUsed ->
    [false] con la mano a {THE($target)}.
    *[true] con {POSS-PRONOUN($user)} {$used} hacia {THE($target)}.
}
