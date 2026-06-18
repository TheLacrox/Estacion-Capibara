# Upstream Sync Test Plan — Goob → Capibara

Branch: `upstream-sync-goob` (merge of `upstream/master`, 491 Goob commits since 2026-02-27).
`master` is untouched — this is the throwaway test branch.

Goal: (1) prove Goob's new content/features work, (2) prove the sync did **not** break Capibara features.

Status: build ✅ (0 errors). Run the automated layer first — it catches the most breakage with zero clicking.

---

## Phase 1 — Automated safety net (run these first)

| # | Check | Command | Catches |
|---|---|---|---|
| 1 | YAML linter | `dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj` | duplicate/broken prototype IDs from merged content — **highest value** |
| 2 | All entities spawn | `dotnet test Content.IntegrationTests --filter "FullyQualifiedName~EntityTest"` | any entity proto with broken/missing components |
| 3 | All maps init | `dotnet test Content.IntegrationTests --filter "FullyQualifiedName~PostMapInitTest"` | broken maps (incl. AtlasUpgraded + new Lavaland) |
| 4 | Economy regression | `dotnet test Content.IntegrationTests --filter "FullyQualifiedName~CapibaraAtmInteractionTest"` | Capibara ATM/bank still works |
| 5 | Unit tests | `dotnet test Content.Tests/Content.Tests.csproj` | logic regressions |

If 1–3 pass, the merged content is structurally sound. Then do the manual passes below.

---

## Phase 2 — Capibara regression (did the sync break OUR work?)

Ordered by break-risk. Risk = how much upstream touched the same system.

### 2A. Trauma Genetics / Wounds — **RISK: HIGH**
Upstream touched the exact systems Capibara ported: WoundSystem cleanup/refactor (#6333), Fix woundmed (#6661), Trauma Undetermined Kits port (#6553), new changeling ability (#5577), wounding damage-type guard (#5995).
Steps:
1. Spawn as organic species. Open Genetics console (Geneticist role exists in Science).
2. Run sequencing / splice a mutation. No crash, console UI works.
3. Take damage → watch transitions **SoftCrit → Critical → Dead**, HUD alerts correct.
4. Medical scanner on the mob → scanner events fire, readout shows wounds.
5. Apply a Trauma Undetermined Kit → expected effect.

### 2B. Chat fonts — **RISK: MEDIUM**
Our `SpeechFontOverrideEvent` was merged with Goob's font-modifier feature (`TransformSpeakerFontEvent` / `modFontId`). Upstream added clown comic-sans (#6464).
Steps:
1. Speak normally → default font.
2. Wear clown mask → speech renders comic sans (goob feature).
3. Trigger a Trauma font-override mutation/effect → font still overrides (our feature). Both must coexist.

### 2C. MobState SoftCrit — **RISK: MEDIUM**
Capibara shifted the enum (`SoftCrit=2`, Critical=3, Dead=4). Upstream: crit/dead escape-pulling fix (#6645), blind hearing (#6621).
Steps:
1. Damage a mob through all thresholds; confirm SoftCrit state + alert show.
2. Pull a crit/dead mob (upstream fix) — works.
3. Defib / revive path works.

### 2D. Economy (ATM / bank / salary / vending) — **RISK: MEDIUM**
Upstream changed food/drink valuation (#6387) — Capibara vending pricing reads valuations.
Steps:
1. ATM: insert ID, deposit/withdraw (covered by automated test #4).
2. Buy from a vending machine → correct price deducted from bank account.
3. Salary console (HOP/Captain) → set salary, trigger payout.
4. Confirm Capi Puntos branding intact (not "Goob Coins").

### 2E. Botany genome — **RISK: LOW-MED**
Upstream: removed botany rr chems (#6417), bluespace tomato fix.
Steps: plant seed → use gene machine / splicer → harvest. Confirm `PlantHarvestedEvent` still feeds station objective tracking.

### 2F. TTS — **RISK: LOW**
Steps: enable (`cvar tts.enabled true`), speak → hear OGG. Character creator TTS voice selector + preview works.

### 2G. Guidebooks — **KNOWN DEBT (not a crash, content drift)**
During merge, 4 Spanish guidebooks kept our translation. Upstream changed mechanics that are now NOT reflected in Spanish:
- `ChangelingsAbilities.xml` — changeling stasis **reworked** (0/15 chems, merged enter/exit, new Regenerate ability, 15–60s). **Worth re-translating.**
- `Slasher.xml` — upstream redid layout with `[tex]` ability icons; ours stays plain Spanish text.
- `Zombies.xml`, `Capoeira.xml` — patched the changed mechanic line to Spanish already.
Follow-up: re-translate changeling + slasher to Spanish if we want docs accurate.

---

## Phase 3 — New Goob features (do THEIR additions work?)

Smoke-test the high-visibility additions. Spawn/equip via `spawn` verb or admin, then exercise.

| Feature | Commit | Quick test |
|---|---|---|
| Hydrakin species | #6633 | Selectable in char creator; round-spawn no crash |
| Sandevistan (major) | #6336 | Implant + activate speed ability |
| Lavaland Biome Rework 2.0 | #6520 | Lavaland generates; biome looks right |
| Lavaland dungeons | #5728 | Dungeons spawn on lavaland |
| Changeling: darkness adaption | #5577 | Buy ability, activate in dark |
| E-cigs / cryo cigs | #6634 | Spawn, smoke, effect applies |
| Polling booths | #6530 | Place booth, start a poll, vote |
| Low-pop gamemode | #6563 | Selectable preset, starts |
| Shield bashing | #6550 | Bash with shield → knockback |
| Particles system | #6510 | Effects render |
| Xenobio obtainables | #5554 | Slime extracts obtainable; metal slimes spawn |
| Reclaimer | #6592 | Reclaimer machine processes |
| Energy food synthesizer | #6369 | Machine makes food |
| Honkops re-enabled | #6534 | Gamemode/antag available |
| Obra Dinn watch | #6423 | Item spawns, works |

(Full feature list: `git log --no-merges --oneline f247e3d4..upstream/master`.)

---

## Decision after testing
- Phase 1 green + Phase 2 no regressions → safe to merge `upstream-sync-goob` → `master`.
- Any Phase 2 break → fix on this branch first (master stays safe).
