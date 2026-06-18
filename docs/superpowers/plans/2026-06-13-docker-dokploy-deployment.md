# Docker + Dokploy Deployment Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans or superpowers:subagent-driven-development to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Containerize the Capibara Station server + TTS stack as a Docker Compose project that Dokploy builds from the repo and auto-deploys.

**Architecture:** Three Compose services (game-server, redis, tts-worker). A multi-stage `Dockerfile` builds the server from source — fetching the public RobustToolbox submodules itself — and packages a framework-dependent linux-x64 server with hybrid-ACZ. SQLite lives on a named volume; config is a baked prod TOML plus env→`--cvar` overrides at runtime.

**Tech Stack:** Docker (multi-stage), .NET 9 SDK/runtime, SS14 `Content.Packaging`, Python 3.12 + edge-tts, Redis 7, Dokploy/Traefik.

**Verification note:** This is infrastructure — no unit tests. Each task validates its artifact (syntax/lint); the final task runs `docker compose build` (+ optional `up`) as the integration check. If Docker isn't available locally, the build is verified on Dokploy.

---

### Task 1: `.dockerignore`

**Files:**
- Create: `.dockerignore`

- [ ] **Step 1: Write the file** — exclude build artifacts, KEEP `.git`/`.gitmodules`/`RobustToolbox` (needed for submodule fetch + build).

```gitignore
# Build artifacts (regenerated inside the image)
bin/
obj/
**/bin/
**/obj/
release/
*.user

# Editor/local
.vs/
.vscode/
.idea/

# Do NOT ignore: .git, .gitmodules, RobustToolbox/  (build needs them)
```

- [ ] **Step 2: Commit**

```bash
git add .dockerignore
git commit -m "Docker: add .dockerignore (keep .git for submodule build)"
```

---

### Task 2: Prod server config — `Docker/server_config.toml`

**Files:**
- Create: `Docker/server_config.toml`

- [ ] **Step 1: Write the file**

```toml
# Capibara Station - production server config (baked into the Docker image).
# Per-deploy / secret values are injected at runtime via --cvar (see entrypoint.sh).

[log]
path = "logs"
level = 1
enabled = true

[net]
tickrate = 30
port = 1212
bindto = "::,0.0.0.0"
max_connections = 256

[status]
enabled = true
# status.connectaddress is set at runtime from $SS14_DOMAIN (udp://<domain>:1212)

[game]
hostname = "Capibara Station"
lobbyenabled = true
# map comes from the map pool (AtlasUpgraded)

[console]
# SECURITY: behind a reverse proxy, loopback == the proxy. Never grant loopback admin.
loginlocal = false

[hub]
advertise = true
hub_urls = "https://hub.spacestation14.com/"
# hub.server_url is set at runtime from $SS14_DOMAIN (ss14s://<domain>)

[auth]
mode = 1   # 0=optional 1=required 2=disabled. Public server -> required.

[database]
engine = "sqlite"
sqlite_dbpath = "preferences.db"   # relative to --data-dir (/data) -> /data/preferences.db

[tts]
enabled = true
connection_string = "redis:6379"

[build]
fork_id = "capibara"   # launcher manages client ZIPs per fork
```

- [ ] **Step 2: Validate TOML parses**

Run: `python -c "import tomllib; tomllib.load(open('Docker/server_config.toml','rb')); print('ok')"`
Expected: `ok`

- [ ] **Step 3: Commit**

```bash
git add Docker/server_config.toml
git commit -m "Docker: add production server_config.toml"
```

---

### Task 3: `entrypoint.sh` — env → cvar launcher

**Files:**
- Create: `entrypoint.sh`

- [ ] **Step 1: Write the file** — POSIX `sh`, uses `set --` so values with spaces (hostname) survive.

```sh
#!/bin/sh
set -e

# Base args: baked config + persistent data dir on the volume.
set -- --config-file /app/server_config.toml --data-dir /data

# Always-on hardening (defense in depth; also set in the TOML).
set -- "$@" --cvar "console.loginlocal=false"

# Optional env overrides.
[ -n "$SS14_HOSTNAME" ]      && set -- "$@" --cvar "game.hostname=$SS14_HOSTNAME"
[ -n "$SS14_HUB_ADVERTISE" ] && set -- "$@" --cvar "hub.advertise=$SS14_HUB_ADVERTISE"
[ -n "$SS14_AUTH_MODE" ]     && set -- "$@" --cvar "auth.mode=$SS14_AUTH_MODE"
[ -n "$SS14_TTS_ENABLED" ]   && set -- "$@" --cvar "tts.enabled=$SS14_TTS_ENABLED"
[ -n "$SS14_TTS_CONN" ]      && set -- "$@" --cvar "tts.connection_string=$SS14_TTS_CONN"

# Domain-derived launcher routing (HTTPS status via proxy, UDP gameplay direct).
if [ -n "$SS14_DOMAIN" ]; then
  set -- "$@" --cvar "hub.server_url=ss14s://$SS14_DOMAIN"
  set -- "$@" --cvar "status.connectaddress=udp://$SS14_DOMAIN:1212"
fi

echo "Starting Robust.Server with: $*"
exec ./Robust.Server "$@"
```

- [ ] **Step 2: Lint (if shellcheck available)**

Run: `shellcheck entrypoint.sh || echo "shellcheck not installed - skip"`
Expected: no errors (or skip).

- [ ] **Step 3: Commit**

```bash
git add entrypoint.sh
git commit -m "Docker: add entrypoint.sh (env -> cvar launcher)"
```

---

### Task 4: Game server image — `Dockerfile`

**Files:**
- Create: `Dockerfile`

- [ ] **Step 1: Write the file** — multi-stage. Build stage fetches submodules + packages; runtime stage is framework-dependent `.NET 9 runtime`.

```dockerfile
# syntax=docker/dockerfile:1

# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# git: submodule fetch. python3: SS14 build-info tooling. unzip: unpack the package.
RUN apt-get update \
 && apt-get install -y --no-install-recommends git python3 unzip \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /src

# Dokploy's checkout (source + .git + .gitmodules).
COPY . .

# Fetch the engine + nested submodules ourselves (all public space-wizards repos,
# no auth). Does NOT depend on Dokploy populating submodules.
RUN git submodule update --init --recursive

# Restore, build the packaging tool, package a linux-x64 server with the client
# embedded (hybrid ACZ) so the launcher self-downloads the client.
RUN dotnet restore \
 && dotnet build Content.Packaging --configuration Release --no-restore \
 && dotnet run --project Content.Packaging server --platform linux-x64 --hybrid-acz

# Unpack the produced server zip.
RUN mkdir -p /app && unzip -o release/SS14.Server_linux-x64.zip -d /app

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime

# Server is framework-dependent (--no-self-contained), so the runtime image is required.
# ICU ships with the runtime image; add freetype/fontconfig defensively for the engine.
RUN apt-get update \
 && apt-get install -y --no-install-recommends libfreetype6 fontconfig \
 && rm -rf /var/lib/apt/lists/*

RUN useradd --system --create-home --uid 10001 ss14
WORKDIR /app

COPY --from=build /app /app
COPY Docker/server_config.toml /app/server_config.toml
COPY entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh /app/Robust.Server \
 && mkdir -p /data \
 && chown -R ss14:ss14 /app /data

USER ss14

# UDP = gameplay (direct host port); TCP = status/launcher (frontable via Traefik).
EXPOSE 1212/udp 1212/tcp

ENTRYPOINT ["/app/entrypoint.sh"]
```

- [ ] **Step 2: Lint Dockerfile (if hadolint available)**

Run: `hadolint Dockerfile || echo "hadolint not installed - skip"`
Expected: no errors (or skip).

- [ ] **Step 3: Commit**

```bash
git add Dockerfile
git commit -m "Docker: add multi-stage game-server Dockerfile (from-source + submodule fetch)"
```

---

### Task 5: TTS worker image — `Dockerfile.tts`

**Files:**
- Create: `Dockerfile.tts`

- [ ] **Step 1: Write the file** — edge backend only (tts_worker imports `redis` at module level, `edge_tts` lazily).

```dockerfile
# syntax=docker/dockerfile:1
FROM python:3.12-slim

# edge backend needs outbound internet (Microsoft TTS endpoint).
RUN pip install --no-cache-dir redis edge-tts

WORKDIR /app
COPY Tools/tts_worker.py /app/tts_worker.py

# Reaches Redis over the compose network. Default redis port (6379) inside the network.
ENTRYPOINT ["python", "tts_worker.py", "--redis-host", "redis", "--redis-port", "6379", "--backend", "edge"]
```

> If the worker errors at startup needing the piper voices dir, add `COPY Tools/tts_voices /app/tts_voices`. Edge backend should not need it.

- [ ] **Step 2: Commit**

```bash
git add Dockerfile.tts
git commit -m "Docker: add TTS worker image (python edge-tts)"
```

---

### Task 6: `docker-compose.yml` (rewrite)

**Files:**
- Modify (replace): `docker-compose.yml`

- [ ] **Step 1: Write the file** — replaces the redis-only dev file; this is the prod stack.

```yaml
name: capibara-station

services:
  game-server:
    build:
      context: .
      dockerfile: Dockerfile
    restart: unless-stopped
    depends_on:
      - redis
    ports:
      - "1212:1212/udp"   # gameplay - Traefik cannot proxy UDP, must be a direct host port
      - "1212:1212/tcp"   # status/launcher - can also be fronted by Dokploy/Traefik with HTTPS
    volumes:
      - ss14-data:/data
    environment:
      SS14_HOSTNAME: "${SS14_HOSTNAME:-Capibara Station}"
      SS14_DOMAIN: "${SS14_DOMAIN:-}"
      SS14_HUB_ADVERTISE: "${SS14_HUB_ADVERTISE:-true}"
      SS14_AUTH_MODE: "${SS14_AUTH_MODE:-1}"
      SS14_TTS_ENABLED: "${SS14_TTS_ENABLED:-true}"
      SS14_TTS_CONN: "${SS14_TTS_CONN:-redis:6379}"

  redis:
    image: redis:7-alpine
    restart: unless-stopped
    # Internal only - no host ports in prod. (Local dev: `docker compose up redis`
    # and add a port mapping locally if you need host access.)

  tts-worker:
    build:
      context: .
      dockerfile: Dockerfile.tts
    restart: unless-stopped
    depends_on:
      - redis

volumes:
  ss14-data:
```

- [ ] **Step 2: Validate compose**

Run: `docker compose config >/dev/null && echo "compose ok" || echo "compose invalid (or docker missing)"`
Expected: `compose ok` (or docker-missing note).

- [ ] **Step 3: Commit**

```bash
git add docker-compose.yml
git commit -m "Docker: rewrite compose to 3-service prod stack (server+redis+tts)"
```

---

### Task 7: Deploy guide — `docs/deploy-dokploy.md`

**Files:**
- Create: `docs/deploy-dokploy.md`

- [ ] **Step 1: Write the file**

```markdown
# Deploying Capibara Station on Dokploy

This repo ships a 3-service Docker Compose stack that Dokploy builds from source.

## Services
- `game-server` — built from `Dockerfile` (multi-stage, fetches submodules itself).
- `redis` — message broker for TTS (internal only).
- `tts-worker` — Python edge-tts worker (`Dockerfile.tts`); needs outbound internet.

## Dokploy setup
1. Create a **Compose** application pointing at this repo + branch; compose path `docker-compose.yml`.
2. Enable **auto-deploy** (push webhook).
3. Submodules: the Dockerfile fetches the public RobustToolbox submodules itself, so
   Dokploy's (flaky) submodule cloning is NOT required. Just ensure `.git` is present
   in the build context (default for a Dokploy git checkout).
4. Set **environment variables** (Dokploy UI):
   - `SS14_DOMAIN` = your domain (e.g. `play.example.com`) — drives `hub.server_url`
     (`ss14s://<domain>`) and `status.connectaddress` (`udp://<domain>:1212`).
   - `SS14_HOSTNAME` = server name on the hub.
   - Optional: `SS14_HUB_ADVERTISE` (default `true`), `SS14_AUTH_MODE` (default `1`),
     `SS14_TTS_ENABLED` (default `true`), `SS14_TTS_CONN` (default `redis:6379`).
5. **Networking (important):**
   - **UDP 1212** must be open on the host and published (gameplay). Traefik cannot proxy UDP.
   - **TCP 1212** is the status/launcher endpoint. Front it with Dokploy's domain + HTTPS
     (Traefik) so the launcher uses `ss14s://<domain>`, or expose it directly.
6. **Resources:** the from-source build is heavy (~10-15 min, several GB). Give the
   Dokploy host enough CPU/RAM/disk.

## Persistence
A named volume `ss14-data` is mounted at `/data` (sqlite `preferences.db` + logs). It
survives redeploys. Config travels in the image (edit `Docker/server_config.toml` + redeploy).

## Security
- `console.loginlocal=false` is enforced (TOML + entrypoint) — never grant loopback admin
  behind a proxy. Use DB admin ranks instead.
- `auth.mode=1` (required) for the public hub.
- Redis is not host-published.

## Verify after deploy
1. `game-server` logs show the server starting and "status server" binding.
2. `tts-worker` logs show a Redis connection.
3. Launch the SS14 client/launcher → connect to `ss14s://<domain>` (or `udp://<domain>:1212`)
   → round loads (AtlasUpgraded).
4. In-game speech produces TTS audio.
5. Redeploy → `preferences.db` persists.

## Local smoke test
```bash
docker compose build
docker compose up         # connect a client to localhost:1212
```
```

- [ ] **Step 2: Commit**

```bash
git add docs/deploy-dokploy.md
git commit -m "Docker: add Dokploy deploy guide"
```

---

### Task 8: Integration build + verify

**Files:** none (verification only)

- [ ] **Step 1: Confirm Docker availability**

Run: `docker version --format '{{.Server.Version}}' || echo "no docker daemon"`
Expected: a version, or "no docker daemon" (then verification happens on Dokploy).

- [ ] **Step 2: Validate the full compose**

Run: `docker compose config >/dev/null && echo OK`
Expected: `OK`

- [ ] **Step 3: Build images (if Docker present)**

Run: `docker compose build 2>&1 | tail -20`
Expected: all three images build; game-server completes the package + unpack. (Heavy.)

- [ ] **Step 4: Smoke boot (optional, if Docker present)**

Run: `docker compose up -d && sleep 60 && docker compose logs game-server | tail -30`
Expected: server startup logs, no fatal exception. Then `docker compose down`.

- [ ] **Step 5: Final commit (if any tweaks were needed)**

```bash
git add -A
git commit -m "Docker: verified compose build" || echo "nothing to commit"
```

---

## Self-Review

**Spec coverage:**
- 3 services → Tasks 4,5,6 ✅  | SQLite volume → Tasks 2,6 ✅  | submodule fetch in Dockerfile → Task 4 ✅
- UDP-direct + status-via-Traefik → Tasks 6,7 ✅  | env→cvar → Tasks 3,6 ✅  | loginlocal off → Tasks 2,3 ✅
- public hub (auth/hub) → Task 2 ✅  | persistence → Tasks 4,6 ✅  | deploy guide → Task 7 ✅
- `.dockerignore` keeps `.git` → Task 1 ✅  | runtime base `dotnet/runtime:9.0` → Task 4 ✅
- Dropped manual `build.json` (fork_id set in TOML; hybrid-ACZ auto-generates build.json with the client hash — do not clobber).

**Placeholder scan:** none — all file contents are complete.

**Consistency:** env var names (`SS14_*`), `tts.connection_string=redis:6379`, port `1212`, `/data`, `/app/server_config.toml`, `Robust.Server` consistent across entrypoint, Dockerfile, compose, config.
