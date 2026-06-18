# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is **Capibara Station**, a fork of Goob Station, which is itself a fork of Space Station 14 (SS14). It's a multiplayer game built on the **RobustToolbox** engine (git submodule). The codebase is C# 12 on .NET 9.0, using an Entity Component System (ECS) architecture. The station is bilingual (English and Spanish).

## Build & Run Commands

```bash
# First-time setup (initializes submodules + downloads engine)
python RUN_THIS.py

# Build (default DebugOpt configuration)
dotnet build

# Run server (default port 1212)
dotnet run --project Content.Server/Content.Server.csproj

# Run client
dotnet run --project Content.Client/Content.Client.csproj

# Run unit tests
dotnet test Content.Tests/Content.Tests.csproj

# Run integration tests
dotnet test Content.IntegrationTests/Content.IntegrationTests.csproj

# Run a single test (by name filter)
dotnet test Content.Tests/Content.Tests.csproj --filter "FullyQualifiedName~TestClassName.TestMethodName"

# YAML linter (validates all prototype YAML files)
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj
```

Build configurations: `Debug`, `DebugOpt` (default), `Release`, `Tools`.

**Important**: Kill running server/client processes before rebuilding — locked exe files cause MSB3027/MSB3021 errors. The server binds port 1212; check for stale processes if you get "port in use" errors.

## Architecture

### Three-Layer Content Split

All game content is split across three layers:

| Layer | Purpose |
|-------|---------|
| `Content.Shared` | Components, prototypes, enums, network messages — synced between client and server |
| `Content.Server` | Server-only systems, game logic, backend services |
| `Content.Client` | Client-only UI, rendering, input handling |

The same split exists for Goobstation extensions (`Content.Goobstation.Shared`, `.Server`, `.Client`, `.Common`, `.Maths`, `.UIKit`) and Capibara-specific code (`_Capibara/` subdirectories within each layer).

### ECS Pattern

RobustToolbox's ECS framework:
- **Components** — Data-only classes with `[RegisterComponent]`. Networked components use `[NetworkedComponent]` and `[AutoGenerateComponentState]` with `[AutoNetworkedField]` on fields.
- **Systems** — Inherit `EntitySystem`, subscribe to events via `SubscribeLocalEvent<TComp, TEvent>()`, query entities with `EntityQueryEnumerator<T1, T2>()`.
- **Partial classes** — Large systems split across files (e.g., `CapibaraBankSystem.cs` + `CapibaraBankSystem.ATM.cs` + `CapibaraBankSystem.SalaryConsole.cs`). `[Dependency]` fields must not duplicate across partials.
- Features are organized by domain directory, each with implementations across Shared/Server/Client.

### Prototype System

Game data is defined in **YAML prototype files** under `Resources/Prototypes/`. C# prototype classes use `[Prototype]`. Custom prototypes go in `Resources/Prototypes/_Capibara/`.

### UI System

Client UI uses XAML (RobustToolbox's UI framework, similar to Avalonia):
- XAML files define layout (`.xaml`), code-behind handles logic (`.xaml.cs`)
- `BoundUserInterface` classes bridge server state to client windows
- Server sends state via `_uiSystem.SetUiState()`, client receives in `UpdateState()`
- UI events use `BoundUserInterfaceMessage` subclasses

### Localization

Uses **Fluent** (`.ftl`) format. Entity names use `ent-{EntityId}` keys (e.g., `ent-CapibaraATM`). All new features must have both `en-US` and `es-ES` locale files.

### Guidebook System

In-game documentation uses XML files in `Resources/ServerInfo/Guidebook/`, registered via `guideEntry` YAML prototypes in `Resources/Prototypes/Guidebook/`. Supports rich markup: `[color]`, `[textlink]`, `[bold]`, `<GuideEntityEmbed>`, and Markdown-style headers (`#`, `##`).

## Fork Structure and Upstream Sync

This repo syncs upstream from Goob Station. To **minimize merge conflicts**, all Capibara-specific code MUST live in `_Capibara/` subdirectories that upstream will never touch.

### Where to put Capibara code (SAFE — no merge conflicts)

| Content type | Path |
|---|---|
| Server systems | `Content.Server/_Capibara/{Feature}/` |
| Shared components/events/prototypes | `Content.Shared/_Capibara/{Feature}/` |
| Client UI (XAML + BUI) | `Content.Client/_Capibara/{Feature}/` |
| YAML prototypes | `Resources/Prototypes/_Capibara/{Feature}/` |
| English locale strings | `Resources/Locale/en-US/_Capibara/{feature}/` |
| Spanish locale strings | `Resources/Locale/es-ES/_Capibara/{feature}/` |
| Guidebook XML content | `Resources/ServerInfo/Guidebook/_Capibara/` |
| Guidebook YAML registration | `Resources/Prototypes/_Capibara/Guidebook/` |
| Textures/sprites | `Resources/Textures/_Capibara/` |

### When you MUST modify upstream files

Sometimes you need to hook into existing upstream systems. These edits create merge conflict risk and should be **kept minimal**. Document each one clearly.

**Currently required upstream edits:**

| File | Why | Conflict risk |
|---|---|---|
| `Content.Server/Botany/Systems/PlantHolderSystem.cs` | Raise `PlantHarvestedEvent` for station objectives tracking | Low (small addition) |
| `Resources/Prototypes/game_presets.yml` | Add `StationObjectivesRule` to game presets | Medium (frequently edited) |
| `Resources/Prototypes/Guidebook/station.yml` | Add `CapibaraEconomy` to guidebook tree | Low (append to list) |
| `Resources/Locale/{en-US,es-ES}/guidebook/guides.ftl` | Add economy guidebook entry names | Low (append to end) |
| `Content.Server/Content.Server.csproj` | Add `StackExchange.Redis` NuGet package for TTS | Low |
| `Directory.Packages.props` | Add `StackExchange.Redis` version for central package management | Low (append) |
| `Content.Server/IoC/ServerContentIoC.cs` | Register `ITTSClient` / `TTSClient` for TTS | Low (append) |
| `Content.Server/Entry/EntryPoint.cs` | Initialize and shutdown `ITTSClient` for TTS | Low (append) |
| `Content.Shared/Preferences/HumanoidCharacterProfile.cs` | Add `TTSVoice` field for TTS voice selection in character creation | Medium (frequently edited) |
| `Content.Shared/Humanoid/SharedHumanoidAppearanceSystem.cs` | Set TTS voice from profile in `LoadProfile` after `SetBarkVoice` | Low (small addition) |
| `Content.Client/Lobby/UI/HumanoidProfileEditor.xaml` | Add TTS voice selector UI after barks container | Low (append) |
| `Content.Client/Lobby/UI/HumanoidProfileEditor.xaml.cs` | Init TTS voice UI + `UpdateTTSVoice()` calls | Low (small additions) |
| `Content.Shared/Mobs/MobState.cs` | Added `SoftCrit = 2`, shifted Critical to 3, Dead to 4 for Trauma genetics | High (enum values changed) |
| `Content.Shared/Mobs/Systems/MobStateSystem.cs` | `IsCritical()` also checks SoftCrit | Medium (behavior change) |
| `Content.Shared/Mobs/Systems/MobStateSystem.Subscribers.cs` | SoftCrit cases in 4 switch statements | Medium (multiple edits) |
| `Content.Shared/Mobs/Components/MobThresholdsComponent.cs` | SoftCrit alert mapping | Low (small addition) |
| `Content.Server/Body/Components/ThermalRegulatorComponent.cs` | Added mutation system to `[Access]` | Low (attribute edit) |
| `Content.Shared/Movement/Systems/SharedMoverController.cs` | Raise `FootStepEvent` for Trauma genetics | Low (small addition) |
| `Content.Server/Chat/Systems/ChatSystem.cs` | Raise `SpeechFontOverrideEvent` for Trauma genetics | Low (small addition) |
| `Content.Server/Radio/EntitySystems/RadioSystem.cs` | Raise `SpeechFontOverrideEvent` for Trauma genetics | Low (small addition) |
| `Content.Server/Medical/MedicalScannerSystem.cs` | Raise scanner events for Trauma genetics | Low (small addition) |
| `Content.Shared/Chemistry/Reaction/ReactiveComponent.cs` | Add `ScaleOverride` field for Trauma genetics | Low (append field) |
| `Content.Shared/EntityEffects/Effects/StatusEffects/GenericStatusEffect.cs` | Add `Update` to `StatusEffectMetabolismType` enum for Trauma genetics | Low (prepend value) |
| `Content.Shared/Trigger/Systems/DnaScrambleOnTriggerSystem.cs` | Extract public `Scramble()` method for `ScrambleDna` entity effect | Low (refactor) |
| `Resources/Prototypes/Roles/Jobs/departments.yml` | Added Geneticist to Science department roles | Low (append to list) |
| `Resources/Prototypes/Guidebook/science.yml` | Added Genetics to Science guidebook children | Low (append to list) |
| `Content.Shared/Body/Systems/SharedBloodstreamSystem.cs` | Raise `BleedModifierEvent` in bleed tick for Trauma genetics bleeding mutation | Low (small addition) |
| `Resources/Prototypes/Entities/Mobs/Species/base.yml` | Add `MutatableComponent` to `BaseMobSpeciesOrganic` for genetics system | Low (append component) |
| `Content.Packaging/ServerPackaging.cs` | Add `StackExchange.Redis` + `Pipelines.Sockets.Unofficial` to `ServerExtraAssemblies` so TTS deps ship in the packaged server (else `TTSClient` crashes on load in Docker/published builds) | Low (append to list) |

**Rules for upstream edits:**

1. **Prefer events over direct modification.** Define a new event in `Content.Shared/_Capibara/` and raise it from the upstream file with a 2-3 line addition. Handle all logic in `_Capibara/` systems. (Example: `PlantHarvestedEvent` in botany.)
2. **Append, don't insert.** When adding to YAML lists or locale files, add at the end to minimize diff conflicts.
3. **Never restructure upstream code.** If upstream refactors a file you edited, your change should be easy to re-apply.
4. **Comment your additions.** Use `# Capibara` or a clear marker so edits are easy to find during merge.
5. **Track all upstream edits.** Keep the table above updated when adding new ones.

### Creating a new Capibara feature

Follow this folder structure (example for a feature called `MyFeature`):

```
Content.Shared/_Capibara/MyFeature/
├── Components/MyFeatureComponent.cs    # [RegisterComponent, NetworkedComponent]
├── Events/MyFeatureEvent.cs            # Shared events
├── MyFeaturePrototype.cs               # [Prototype] if needed
└── SharedMyFeatureSystem.cs            # Shared system (ItemSlot registration, etc.)

Content.Server/_Capibara/MyFeature/
└── MyFeatureSystem.cs                  # Server logic, event handlers

Content.Client/_Capibara/MyFeature/
├── UI/MyFeatureBoundUserInterface.cs   # BUI bridge
├── UI/MyFeatureWindow.xaml             # XAML layout
└── UI/MyFeatureWindow.xaml.cs          # Code-behind

Resources/Prototypes/_Capibara/MyFeature/
└── entities.yml                        # Entity prototypes

Resources/Locale/en-US/_Capibara/myfeature/
└── myfeature.ftl                       # English strings

Resources/Locale/es-ES/_Capibara/myfeature/
└── myfeature.ftl                       # Spanish strings

Content.IntegrationTests/Tests/_Capibara/MyFeature/
└── MyFeatureInteractionTest.cs         # Headless test — REQUIRED (see Testing Policy)
```

## Testing Policy

**Every Capibara feature or change MUST ship with a headless integration test that drives the feature the way a player would and asserts it works without crashing.** No feature is "done" until its test passes. A feature with no test is treated as broken.

This is non-negotiable for anything with player-facing behavior: UI windows (BUI), machine/console interactions, item interactions, entity effects, game rules, objectives. Pure data-only YAML tweaks (e.g. changing a number) are exempt unless they change logic.

### Why

Manual testing means launching the client, connecting, and clicking through every path by hand on every change — slow, skipped, and crashes slip into the live server. A headless test runs a full server+client pair in-process (no window), simulates the real interaction, and fails loudly on any exception or wrong result. Run it after every change; deploy with confidence.

### How

Extend `InteractionTest` (`Content.IntegrationTests/Tests/Interaction/InteractionTest`). It spawns a server+client pair and a player mob, and gives player-action helpers: `SpawnTarget(proto)`, `Activate()`, `Interact()`, `InteractUsing(id, qty)`, `SendBui(key, msg)`, `IsUiOpen(key)`, `ClickControl<TWindow>("Name")`, `AssertEntityLookup(...)`, `TryComp<T>(out c)`, `RunTicks(n)`.

**Reference implementation:** `Content.IntegrationTests/Tests/_Capibara/Economy/CapibaraAtmInteractionTest.cs` — copy its structure.

A good feature test covers, at minimum:
1. **Happy path** — perform the interaction, assert the resulting game state (balance changed, item spawned, UI opened, etc.).
2. **Failure/guard paths** — invalid input, missing precondition, unauthorized actor — assert it's rejected gracefully and **does not crash or mutate state**.

Run it:

```bash
dotnet test Content.IntegrationTests/Content.IntegrationTests.csproj --filter "FullyQualifiedName~MyFeatureInteractionTest"
```

### Gotchas (learned writing the ATM test)

- The test player mob (`InteractionTestMob`) has one hand and **no `id` inventory slot**. To get an ID/item into a machine's `ItemSlot`, insert it directly: `SEntMan.System<ItemSlotsSystem>().TryInsert(machineUid, comp.IdSlot, item, null)`. Don't rely on a "insert from inventory" BUI message.
- Machines with `ActivatableUIRequiresPower` need power before the BUI opens. Spawn `APCBasic` on the target tile: `await SpawnEntity("APCBasic", SEntMan.GetCoordinates(TargetCoords)); await RunTicks(5);`.
- `EntityUid` is not in the project global usings — add `using Robust.Shared.GameObjects;` to test files that reference it.
- `SendBui(key, msg)` only works after the BUI is open client-side — call `Activate()` first and assert `IsUiOpen(key)`.

## Capibara Station Features

Current custom features:

- **Economy** — Bank accounts on ID cards, ATM machines, salary payroll system, vending machine pricing, salary management console (HOP/Captain)
- **Station Objectives** — Cooperative crew objectives with a 30-minute deadline that freezes salaries if unmet
- **TTS (Text-to-Speech)** — Converts in-game speech to audio via external TTS service over Redis. Server hooks `EntitySpokeEvent`, streams OGG audio chunks to PVS clients. Requires Redis + TTS worker (see `docker-compose.yml`). CVars: `tts.enabled`, `tts.connection_string`

### Other Fork Content

The repo also includes `_FarHorizons/` directories (a separate fork's content) with features like fission generators, machine linking, and research systems. These follow the same isolation pattern as `_Capibara/` but are not Capibara-specific code.

## Deployment (Docker / Dokploy)

The server deploys as a **3-service Docker Compose stack that Dokploy builds from the repo** on push (no CI image / registry). Full guide: `docs/deploy-dokploy.md`. Design + plan: `docs/superpowers/specs/2026-06-13-docker-dokploy-deployment-design.md`, `docs/superpowers/plans/2026-06-13-docker-dokploy-deployment.md`.

### Stack

| Service | Image | Notes |
|---|---|---|
| `game-server` | `Dockerfile` (multi-stage, from-source) | UDP 1212 (gameplay) + TCP 1212 (status/launcher) |
| `redis` | `redis:7-alpine` | TTS broker, internal only (no host port in prod) |
| `tts-worker` | `Dockerfile.tts` (python + edge-tts) | needs outbound internet; reaches `redis:6379` |

### Files

| File | Purpose |
|---|---|
| `Dockerfile` | Multi-stage build: SDK stage packages `Content.Packaging server --platform linux-x64 --hybrid-acz`; runtime stage on `dotnet/runtime:9.0` (server is framework-dependent, `--no-self-contained`) |
| `Dockerfile.tts` | TTS worker (`pip install redis edge-tts`, runs `Tools/tts_worker.py`) |
| `docker-compose.yml` | The prod stack + `ss14-data` volume (replaces the old redis-only dev file) |
| `entrypoint.sh` | Maps `SS14_*` env vars → `--cvar` flags; launches `Robust.Server --config-file ... --data-dir /data` |
| `Docker/server_config.prod.toml` | Baked prod config (named `.prod.toml` because bare `server_config.toml` is gitignored) |
| `.dockerignore` | Excludes `bin`/`obj`/`release`; **keeps `.git`** (build needs it for submodules) |

### Key design points

- **Submodules:** the `Dockerfile` runs `git submodule update --init --recursive` itself (all `space-wizards/*` submodules are public, no auth). Does **not** rely on Dokploy's flaky submodule cloning.
- **Networking:** SS14 gameplay is **UDP 1212** — Traefik can't proxy UDP, so publish it as a direct host port. The TCP status server is fronted by Dokploy/Traefik for HTTPS → launcher uses `ss14s://<domain>`; `entrypoint.sh` sets `status.connectaddress=udp://<domain>:1212` from `$SS14_DOMAIN`.
- **Security:** `console.loginlocal=false` (TOML + entrypoint). Behind a proxy, loopback == the proxy, so loopback admin would be handed to any player.
- **Admin bootstrap:** `console.login_host_user` (baked to `"TheLacrox"` in `server_config.prod.toml`, override via `$SS14_HOST_USER`) auto-promotes that account to full host (all admin flags, `AdminFlagsHelper.Everything`) on join — see `AdminManager.LoadAdminDataCore`. Survives fresh data volumes, no SQLite editing. **Safe only with `auth.mode=1`** (account names tied to real SS14 accounts); with auth disabled anyone could pick the name. SS14 has no `addadmin` console command, so without this the only bootstrap is hand-inserting into `admin`/`admin_flag` (flag `HOST`) in `/data/preferences.db`.
- **Persistence:** SQLite `preferences.db` + logs on the `ss14-data` volume at `/data`. Config travels in the image (edit `Docker/server_config.prod.toml` + redeploy).
- **Env config** (Dokploy UI): `SS14_DOMAIN`, `SS14_HOSTNAME`, `SS14_HUB_ADVERTISE` (default `true`), `SS14_AUTH_MODE` (default `1`), `SS14_HOST_USER` (default `TheLacrox`, empty = disabled), `SS14_TTS_ENABLED`, `SS14_TTS_CONN` (default `redis:6379`).

### Local build/smoke

```bash
docker compose build               # builds game-server (from source) + tts-worker
docker compose up                  # connect a client to localhost:1212
```

### Packaging gotcha (important)

`Content.Packaging/ServerPackaging.cs` **whitelists** assemblies (`ServerExtraAssemblies`) and strips unknown third-party DLLs. Any new server dependency that isn't a `Content.*` assembly must be added there or the **packaged/published/Docker server crashes on boot** (it works locally from `bin/` regardless). TTS's `StackExchange.Redis` + `Pipelines.Sockets.Unofficial` were added for this reason. When adding a new third-party server dependency, add it to `ServerExtraAssemblies` too.

## Code Style

Enforced via `.editorconfig`:
- 4-space indentation, 120 char line limit, file-scoped namespaces
- `var` preferred everywhere
- Private fields: `_camelCase` prefix with underscore
- Public members, types, methods, properties, constants: `PascalCase`
- Interfaces: `IPascalCase`, type parameters: `TPascalCase`
- Allman-style braces (opening brace on new line)
- Space after cast: `(int) value`
- SPDX license headers on all files (`AGPL-3.0-or-later`)
- Modifier order: `public, private, protected, internal, new, abstract, virtual, sealed, override, static, readonly, extern, unsafe, volatile, async`

## CI Checks

PRs must pass: Build & Test (DebugOpt on Ubuntu), Test Packaging, YAML Linter, RGA/RSI/map validators.

## Branch Protection (master)

`master` is protected on GitHub (set via `gh api PUT /repos/.../branches/master/protection`). To change rules, edit that protection object — not the repo settings UI blindly.

- **No direct pushes** — all changes land via PR. Always branch off latest `origin/master` and open a PR (`gh pr create --repo TheLacrox/Estacion-Capibara --base master ...`; the `--repo` flag is required or `gh` targets the upstream fork parent).
- **1 approving review required**; stale approvals dismissed on new commits; conversation resolution required; force-push + branch deletion blocked.
- **`enforce_admins=false`** — the owner (admin) can bypass the review gate. This is deliberate: GitHub forbids approving your own PR, so on a solo-maintained repo the owner merges their own PRs via admin bypass while contributors' PRs still need owner approval.
- No required status checks wired yet (admin merges don't need green CI). Add them to the protection object's `required_status_checks.contexts` if you want CI gating.

## Key Gotchas

- `RobustToolbox/` is a git submodule — do not modify directly.
- `IdCardComponent.JobDepartments` (`List<ProtoId<DepartmentPrototype>>`) is the correct way to get departments from an ID card. Do NOT use `idCard.JobPrototype` for department lookup — it's a `ProtoId<AccessLevelPrototype>`, not a job ID.
- `DepartmentPrototype.Primary` is `false` for Command department, so `TryGetPrimaryDepartment()` will skip Captain, CMO, etc. Use `IdCardComponent.JobDepartments` directly instead.
- For click-on-entity interactions, use `InteractUsingEvent` from `Content.Shared.Interaction`.
- Button clicks in dynamic UI must use `Button.OnPressed`, not `PanelContainer.OnKeyBindDown`.

## License

AGPL-3.0-or-later for code. Most media: CC-BY-SA 3.0 (some CC-BY-NC-SA 3.0).
