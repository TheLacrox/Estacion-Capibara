### Interaction Messages

# System

## When trying to ingest without the required utensil... but you gotta hold it
ingestion-you-need-to-hold-utensil = ¡Necesitas sostener {INDEFINITE($utensil)} {$utensil} para comer eso!

ingestion-try-use-is-empty = ¡{CAPITALIZE(THE($entity))} está vacío!
ingestion-try-use-wrong-utensil = No puedes {$verb} {THE($food)} con {INDEFINITE($utensil)} {$utensil}.

ingestion-remove-mask = Primero tienes que quitarte {$entity}.

## Failed Ingestion

ingestion-you-cannot-ingest-any-more = ¡No puedes {$verb} más!
ingestion-other-cannot-ingest-any-more = ¡{CAPITALIZE(SUBJECT($target))} no puede {$verb} más!

ingestion-cant-digest = ¡No puedes digerir {THE($entity)}!
ingestion-cant-digest-other = ¡{CAPITALIZE(SUBJECT($target))} no puede digerir {THE($entity)}!

## Action Verbs, not to be confused with Verbs

ingestion-verb-food = Comer
ingestion-verb-drink = Beber

# Edible Component

edible-nom = Ñam. {$flavors}
edible-nom-other = Ñam.
edible-slurp = Slurp. {$flavors}
edible-slurp-other = Slurp.
edible-swallow = Te tragas { THE($food) }
edible-gulp = Gulp. {$flavors}
edible-gulp-other = Gulp.

edible-has-used-storage = No puedes {$verb} { THE($food) } con un objeto guardado dentro.

## Nouns

edible-noun-edible = comestible
edible-noun-food = comida
edible-noun-drink = bebida
edible-noun-pill = pastilla

## Verbs

edible-verb-edible = ingerir
edible-verb-food = comer
edible-verb-drink = beber
edible-verb-pill = tragar

## Force feeding

edible-force-feed = ¡{CAPITALIZE(THE($user))} está intentando hacerte {$verb} algo!
edible-force-feed-success = ¡{CAPITALIZE(THE($user))} te ha obligado a {$verb} algo! {$flavors}
edible-force-feed-success-user = Le das de comer con éxito a {THE($target)}
