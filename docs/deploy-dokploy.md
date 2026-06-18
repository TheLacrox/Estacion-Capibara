# Deploying Capibara Station on Dokploy

This repo ships a 3-service Docker Compose stack that Dokploy builds from source.

## Services
- `game-server` — built from `Dockerfile` (multi-stage, fetches submodules itself).
- `redis` — message broker for TTS (internal only).
- `tts-worker` — Python edge-tts worker (`Dockerfile.tts`); needs outbound internet.

## Dokploy setup
1. Create a **Compose** application pointing at this repo + branch; compose path `docker-compose.yml`.
2. Enable **auto-deploy** (push webhook).
3. Enable **Isolated Deployment** (see "Network isolation" below) so the stack runs on its
   own per-app network, separated from `dokploy-network` and other Dokploy apps.
4. Submodules: the Dockerfile fetches the public RobustToolbox submodules itself, so
   Dokploy's (flaky) submodule cloning is NOT required. Just ensure `.git` is present
   in the build context (default for a Dokploy git checkout).
5. Set **environment variables** (Dokploy UI):
   - `SS14_DOMAIN` = your domain (e.g. `play.example.com`) — drives `hub.server_url`
     (`ss14s://<domain>`) and `status.connectaddress` (`udp://<domain>:1212`).
   - `SS14_HOSTNAME` = server name on the hub.
   - Optional: `SS14_HUB_ADVERTISE` (default `true`), `SS14_AUTH_MODE` (default `1`),
     `SS14_TTS_ENABLED` (default `true`), `SS14_TTS_CONN` (default `redis:6379`).
6. **Networking (important):**
   - **UDP 1212** must be open on the host and published (gameplay). Traefik cannot proxy UDP.
   - **TCP 1212** is the status/launcher endpoint. Front it with Dokploy's domain + HTTPS
     (Traefik) so the launcher uses `ss14s://<domain>`, or expose it directly.
7. **Resources:** the from-source build is heavy (~10-15 min, several GB). Give the
   Dokploy host enough CPU/RAM/disk.

## Network isolation (Enable Isolated Deployment)
Turn on Dokploy's **Enable Isolated Deployment** toggle for this Compose app. It creates a
separate Docker network named after the app and attaches every service in this compose to it,
so the stack is isolated from `dokploy-network` and from other Dokploy apps — while the three
containers still reach each other by hostname (`redis`, `tts-worker`) and still have outbound
internet (the worker needs it for edge-tts; the server needs it for the hub and auth).

This is why `docker-compose.yml` defines **no `networks:` block** and does not reference
`dokploy-network` — isolation is handled automatically by Dokploy. Do not add a custom
`networks:` block; it can fight Dokploy's network injection and break hostname resolution.

Caveats:
- **Do NOT use Dokploy's project-level database feature for Redis.** Those managed databases
  live on `dokploy-network`; an isolated stack cannot reach them. Redis is a service *inside*
  this compose, so it resolves fine. Keep it that way.
- Only `game-server` publishes ports. `redis` and `tts-worker` publish nothing, so they have
  zero inbound exposure regardless of isolation. Redis is never reachable from the host or
  internet.

## Persistence
A named volume `ss14-data` is mounted at `/data` (sqlite `preferences.db` + logs). It
survives redeploys. Config travels in the image (edit `Docker/server_config.prod.toml` + redeploy).

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
