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
