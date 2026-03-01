-entity-heater-setting-name =
    { $setting ->
        [off] apagado
        [low] bajo
        [medium] medio
        [high] alto
       *[other] desconocido
    }

entity-heater-examined = Está configurado en { $setting ->
    [off] [color=gray]{ -entity-heater-setting-name(setting: "off") }[/color]
    [low] [color=yellow]{ -entity-heater-setting-name(setting: "low") }[/color]
    [medium] [color=orange]{ -entity-heater-setting-name(setting: "medium") }[/color]
    [high] [color=red]{ -entity-heater-setting-name(setting: "high") }[/color]
   *[other] [color=purple]{ -entity-heater-setting-name(setting: "other") }[/color]
}.
entity-heater-switch-setting = Cambiar a { -entity-heater-setting-name(setting: $setting) }
entity-heater-switched-setting = Cambiado a { -entity-heater-setting-name(setting: $setting) }.
