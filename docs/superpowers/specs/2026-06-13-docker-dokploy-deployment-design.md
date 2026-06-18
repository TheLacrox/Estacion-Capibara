# Docker + Dokploy Deployment — Design Spec

Date: 2026-06-13
Status: Approved (design), pending spec review → implementation plan
Topic: Containerize Capibara Station (SS14 fork) server for automatic deployment on Dokploy.

## Goal

Deploy the Capibara Station game server (plus its TTS stack) as a Docker Compose
project that **Dokploy builds directly from the repo** on push. A public,
hub-advertised server, with persistent data, that redeploys automatically.

## Locked Decisions

| Decision | Choice | Rationale |
|---|---|---|
| Build strategy | Compose-from-repo; **Dokploy builds, no CI** | User requirement; keep build in repo, no GHCR/registry |
| Server image | Multi-stage Dockerfile, **build from source** | Self-contained; Dokploy builds on push |
| Services | game-server + redis + tts-worker (3 containers) | Full stack incl. TTS out of the box |
| Database | SQLite on a Docker volume | Default SS14 mode; single instance; zero extra service |
| Deploy target | Public, hub-advertised | Listed on SS14 hub; auth required |
| Submodules | **Dockerfile fetches them**, not Dokploy | Dokploy submodule support is flaky; our submodules are public so no auth needed |

## Architecture

Three services in one `docker-compose.yml`, one named volume:

```
game-server  (Dockerfile, from-source)
   ├─ UDP 1212        → published directly on host (gameplay; Traefik can't proxy UDP)
   ├─ TCP 1212 status → via Dokploy/Traefik (HTTPS on domain; launcher + hybrid-ACZ client dl)
   ├─ depends_on: redis
   └─ volume: ss14-data → /data   (sqlite preferences.db, logs)
redis        (redis:7-alpine)  — internal only, NOT host-exposed in prod
tts-worker   (Dockerfile.tts, python+edge-tts) — reaches redis:6379, no exposed port, needs outbound internet
```

### Why the networking is split (the crux)

SS14 uses **UDP 1212** for gameplay and a **TCP/HTTP status server** on the same
port number for the launcher (engine version, connect info, and — with hybrid-ACZ —
the client ZIP download). Dokploy's Traefik proxy is HTTP/TCP only and **cannot proxy
UDP**. Therefore:

- **UDP 1212** → published as a raw host port (`1212:1212/udp`).
- **TCP 1212 status** → exposed through Traefik with the domain + HTTPS, so the
  launcher uses `ss14s://<domain>`.
- `hub.server_url = ss14s://<domain>` and `status.connectaddress = udp://<domain>:1212`
  tell the launcher where each half lives. **Both must be reachable** or connection fails.

## Components

### 1. Game server image — `Dockerfile` (multi-stage)

**Build stage** — `mcr.microsoft.com/dotnet/sdk:9.0`:
1. `apt-get install -y git python3` (git for submodules; python3 for build info tooling).
2. Copy build context (the repo Dokploy checked out, incl. `.git`/`.gitmodules`).
3. `git submodule update --init --recursive` — fetches public RobustToolbox + its
   nested submodules (NetSerializer, Lidgren, XamlX, Robust.LoaderApi, cefglue).
   Fallback if `.git` is absent from context: `git clone --recursive <public-repo-url> <branch>`.
4. `dotnet restore`
5. `dotnet build Content.Packaging --configuration Release --no-restore`
6. `dotnet run --project Content.Packaging server --platform linux-x64 --hybrid-acz`
   → produces `release/SS14.Server_linux-x64.zip` (server + embedded client ZIP).
7. Unzip to `/app`.

**Runtime stage** — `mcr.microsoft.com/dotnet/runtime:9.0`
(framework-dependent: packaging uses `--no-self-contained`, so the .NET 9 runtime
must be present in the image; `runtime-deps` is NOT enough):
1. Install any native deps the engine needs at runtime (verify on first build; likely
   `libfreetype6` / fontconfig; ICU ships with the runtime image).
2. `COPY --from=build /app /app`.
3. Non-root user; `WORKDIR /app`.
4. `ENTRYPOINT ["/app/entrypoint.sh"]` → launches `./Robust.Server`
   `--config-file /app/server_config.toml --data-dir /data` plus env-derived `--cvar` flags.
5. `EXPOSE 1212/tcp 1212/udp`.

### 2. `entrypoint.sh` — env → CVar mapping

Translates documented env vars into `--cvar key=value` args so config is 12-factor
without rebuilds. Mapped vars (with sane defaults):

| Env var | CVar | Purpose |
|---|---|---|
| `SS14_HOSTNAME` | `game.hostname` | Server name on hub |
| `SS14_DOMAIN` | builds `hub.server_url=ss14s://$DOMAIN` + `status.connectaddress=udp://$DOMAIN:1212` | Launcher routing |
| `SS14_HUB_ADVERTISE` | `hub.advertise` | Default `true` |
| `SS14_AUTH_MODE` | `auth.mode` | Default `1` (required) |
| `SS14_TTS_ENABLED` | `tts.enabled` | Default `true` |
| `SS14_TTS_CONN` | `tts.connection_string` | Default `redis:6379` |
| (fixed) | `console.loginlocal=false` | Security behind proxy |

### 3. Prod config — `Docker/server_config.toml` + `Docker/build.json`

Committed to repo, baked into the image (changes = repo edit + redeploy). Static prod
settings: `net.port=1212`, `bindto = "::,0.0.0.0"`, `status.enabled=true`,
`hub.advertise=true`, `console.loginlocal=false`, `auth.mode=1`,
sqlite DB at `/data/preferences.db`, `tts.enabled=true`, `tts.connection_string=redis:6379`,
`game.lobbyenabled=true`. Domain-derived + secret values come from env at runtime.
`build.json`: `fork_id = "capibara"` (launcher manages client ZIPs per fork).

### 4. TTS worker — `Dockerfile.tts`

`python:3.12-slim` → `pip install redis edge-tts` → `COPY Tools/tts_worker.py` →
`CMD python tts_worker.py --redis-host redis --redis-port 6379 --backend edge`.
Needs outbound internet (edge-tts calls Microsoft's endpoint). No persistence.

### 5. `docker-compose.yml` (rewrite)

- `game-server`: `build: { context: ., dockerfile: Dockerfile }`, `ports: ["1212:1212/udp"]`,
  Traefik labels / Dokploy domain for TCP 1212, `volumes: ["ss14-data:/data"]`,
  `depends_on: [redis]`, `restart: unless-stopped`, env vars.
- `redis`: `redis:7-alpine`, internal only (no host `ports:`), `restart: unless-stopped`.
- `tts-worker`: `build: { context: ., dockerfile: Dockerfile.tts }`, `depends_on: [redis]`,
  `restart: unless-stopped`.
- `volumes: { ss14-data: {} }`.

> Decision: a **single** `docker-compose.yml` is the prod stack (replaces the current
> Redis-only dev file). For local TTS dev, run just the broker with
> `docker compose up redis` (optionally add a host port override locally). No separate
> dev compose file — keep one source of truth.

### 6. `.dockerignore`

Exclude build artifacts to shrink context: `bin/`, `obj/`, `release/`, `**/bin`, `**/obj`,
`*.user`. **Do NOT exclude** `.git`, `.gitmodules`, or `RobustToolbox/` (needed for the build).

## Persistence

Named volume `ss14-data` mounted at `/data`. Server runs `--data-dir /data`; sqlite DB +
logs live there and survive redeploys. Image is stateless; config travels in the image.

## Auto-deploy (Dokploy settings — not code)

1. Create a **Compose** service, point to the repo + branch, compose path `docker-compose.yml`.
2. Enable auto-deploy (push webhook).
3. Set env vars (`SS14_HOSTNAME`, `SS14_DOMAIN`, etc.) + the **domain** for the TCP status service.
4. Ensure the Dokploy host has resources for the heavy from-source build (~10–15 min, several GB).
5. Open **UDP 1212** on the host firewall.

## Security

- `console.loginlocal=false` — **mandatory**; behind a proxy, loopback = the proxy, so
  leaving it on would grant admin to any connecting player. Admin via DB ranks instead.
- `auth.mode=1` (required) for a public server.
- Redis has no host-published port; only the compose network reaches it.
- Container runs as non-root.

## Files to Add/Change

- `Dockerfile` (new) — game server, multi-stage.
- `Dockerfile.tts` (new) — TTS worker.
- `entrypoint.sh` (new) — env→cvar launcher.
- `docker-compose.yml` (rewrite) — 3 services + volume.
- `.dockerignore` (new).
- `Docker/server_config.toml` (new) — prod config.
- `Docker/build.json` (new) — fork id.
- `docs/deploy-dokploy.md` (new) — deploy guide (Dokploy settings, env vars, domain, firewall).

## Verification / Testing

Docker packaging is infra, not a game feature, so the CLAUDE.md headless-integration-test
policy doesn't map directly. Verification instead:

1. **Local build**: `docker compose build` completes (submodules fetched, package produced).
2. **Local boot**: `docker compose up` → game-server logs server start; no fatal errors.
3. **Connect**: SS14 client/launcher connects to `localhost:1212`, round loads (AtlasUpgraded).
4. **TTS**: tts-worker logs "connected to redis"; in-game speech produces audio.
5. **Persistence**: stop/redeploy → preferences.db survives on the volume.

Document these steps in `docs/deploy-dokploy.md`.

## Risks / Open Items

- **Build resources** on Dokploy host (heavy). Mitigation: graduate to CI image (Approach B) if painful.
- **Runtime native deps**: may need extra `apt` libs after first build error — adjust then.
- **UDP exposure**: host firewall + Dokploy must pass UDP 1212; verify reachability post-deploy.
- **edge-tts internet egress** required from the worker container.
- **Hybrid-ACZ client download** must work over the Traefik HTTPS status endpoint (launcher dl).
- **Deploy-time values** (provided in Dokploy UI, not in repo): domain name, server hostname.

## Out of Scope (v1)

- CI/registry image pipeline (Approach B).
- Postgres.
- Multi-instance / horizontal scaling.
- Automated metrics/monitoring stack.
