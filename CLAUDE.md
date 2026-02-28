# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is **Hispania Station**, a fork of Goob Station, which is itself a fork of Space Station 14 (SS14). It's a multiplayer game built on the **RobustToolbox** engine (git submodule). The codebase is C# 12 on .NET 9.0, using an Entity Component System (ECS) architecture.

## Build & Run Commands

```bash
# First-time setup (initializes submodules + downloads engine)
python RUN_THIS.py

# Build (default DebugOpt configuration)
dotnet build

# Run server
dotnet run --project Content.Server/Content.Server.csproj

# Run client
dotnet run --project Content.Client/Content.Client.csproj

# Run unit tests
dotnet test Content.Tests/Content.Tests.csproj

# Run integration tests
dotnet test Content.IntegrationTests/Content.IntegrationTests.csproj

# Run a single test (by name filter)
dotnet test Content.Tests/Content.Tests.csproj --filter "FullyQualifiedName~TestClassName.TestMethodName"

# YAML linter
dotnet build Content.YAMLLinter/Content.YAMLLinter.csproj
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj
```

Build configurations: `Debug`, `DebugOpt`, `Release`, `Tools`.

## Architecture

### Three-Layer Content Split

All game content is split across three layers that mirror Client/Server/Shared:

| Layer | Purpose |
|-------|---------|
| `Content.Shared` | Components, prototypes, enums, network messages — synced between client and server |
| `Content.Server` | Server-only systems, game logic, backend services |
| `Content.Client` | Client-only UI, rendering, input handling |

The same split exists for Goobstation extensions: `Content.Goobstation.Shared`, `.Server`, `.Client`, plus utility projects `.Common`, `.Maths`, `.UIKit`.

### ECS Pattern

The game uses RobustToolbox's ECS framework:
- **Components** are data-only classes attached to entities (annotated with `[RegisterComponent]`)
- **Systems** (inheriting `EntitySystem`) contain all logic, subscribing to events and querying entities
- Features are organized by domain directory (e.g., `Access/`, `Atmos/`, `Chemistry/`)
- Each feature typically has implementations across Shared, Server, and Client layers

### Prototype System

Game entities and data are defined in **YAML prototype files** under `Resources/Prototypes/`. Prototypes are configuration-driven definitions for entities, recipes, reagents, etc. C# prototype classes use the `[Prototype]` attribute.

### Resource Layout

- `Resources/Prototypes/` — Base SS14 YAML prototypes
- `Resources/Prototypes/_Goobstation/` — Goob Station-specific prototypes
- `Resources/Prototypes/_*` — Other upstream fork prototypes (Corvax, DeltaV, etc.)
- `Resources/Textures/` — Sprites and RSI (Robust Sprite Image) files
- `Resources/Locale/` — Localization strings
- `Resources/Maps/` — Map files
- `Resources/Audio/` — Sound files

### Engine Submodule

`RobustToolbox/` is the upstream SS14 engine — do not modify directly. It provides core ECS, networking, rendering, and serialization frameworks.

### Database

- `Content.Server.Database` — EF Core migrations for the server database
- `Content.Shared.Database` — Shared database model definitions

## Code Style

Enforced via `.editorconfig`:
- 4-space indentation, 120 char line limit, file-scoped namespaces
- `var` preferred everywhere
- Private fields: `_camelCase` prefix with underscore
- Public members, types, methods, properties, constants: `PascalCase`
- Interfaces: `IPascalCase`, type parameters: `TPascalCase`
- Allman-style braces (opening brace on new line)
- Space after cast: `(int) value`
- SPDX license headers on all files

## CI Checks

PRs must pass: Build & Test (Debug + Release), Test Packaging, YAML Linter, YAML RGA/map schema validators, RSI validation.

## License

AGPL-3.0-or-later for code. Most media: CC-BY-SA 3.0 (some CC-BY-NC-SA 3.0).
