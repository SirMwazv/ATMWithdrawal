# Docker Installation Guide

## Docker Compose Not Found?

If you see `docker: unknown command: docker compose`, you need to install Docker Compose.

## Installation Options

### Option 1: Docker Desktop (Recommended - Easiest)

Docker Desktop includes Docker Compose built-in.

**macOS:**
1. Download Docker Desktop: https://www.docker.com/products/docker-desktop
2. Install the .dmg file
3. Start Docker Desktop from Applications
4. Verify: `docker compose version`

**Windows:**
1. Download Docker Desktop: https://www.docker.com/products/docker-desktop
2. Run the installer
3. Restart your computer
4. Start Docker Desktop
5. Verify: `docker compose version`

**Linux:**
1. Follow official guide: https://docs.docker.com/desktop/install/linux-install/
2. Or use the command line installation below

### Option 2: Docker Compose Plugin (CLI)

**macOS/Linux:**
```bash
# Update package index
sudo apt-get update  # Ubuntu/Debian
brew update          # macOS

# Install Docker Compose plugin
mkdir -p ~/.docker/cli-plugins/
curl -SL https://github.com/docker/compose/releases/download/v2.24.0/docker-compose-$(uname -s)-$(uname -m) -o ~/.docker/cli-plugins/docker-compose
chmod +x ~/.docker/cli-plugins/docker-compose

# Verify
docker compose version
```

### Option 3: Standalone Docker Compose (Legacy)

**macOS:**
```bash
brew install docker-compose
```

**Linux:**
```bash
sudo curl -L "https://github.com/docker/compose/releases/download/v2.24.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
```

Then use: `docker-compose up --build` (with hyphen)

## Verify Installation

```bash
# Check Docker
docker --version
# Should show: Docker version 20.x.x or higher

# Check Docker Compose
docker compose version
# Should show: Docker Compose version v2.x.x

# OR (legacy)
docker-compose --version
```

## Alternative: Run Without Docker

If you prefer not to use Docker, you can run the services locally:

### Backend
```bash
cd /Users/mwazvitamutowo/ATMWithdrawal/backend
dotnet restore
dotnet run --project API/API.csproj
```
Runs on: http://localhost:5000

### Frontend
```bash
cd /Users/mwazvitamutowo/ATMWithdrawal/frontend
npm install
npm run dev
```
Runs on: http://localhost:5173

## Troubleshooting

### "Cannot connect to Docker daemon"
```bash
# macOS: Start Docker Desktop application
open -a Docker

# Linux: Start Docker service
sudo systemctl start docker
```

### Permission denied
```bash
# Linux: Add user to docker group
sudo usermod -aG docker $USER

# Log out and back in for changes to take effect
```

### Port already in use
```bash
# Find and kill process using port 5000
lsof -ti:5000 | xargs kill -9

# Or change port in docker-compose.yml
```

## Need Help?

1. Check official Docker docs: https://docs.docker.com/
2. Docker Desktop download: https://www.docker.com/products/docker-desktop
3. See GETTING_STARTED.md for running without Docker
