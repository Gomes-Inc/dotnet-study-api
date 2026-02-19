# App Test API

REST API for message management built with .NET 10 and Entity Framework Core.

## Stack

- .NET 10
- Entity Framework Core 10
- PostgreSQL (Supabase)
- Swagger/OpenAPI
- Railway (deployment)

## API Endpoints

```
GET    /api/message          # List all messages
GET    /api/message/{id}     # Get message by ID
POST   /api/message          # Create new message
DELETE /api/message/{id}     # Delete message
```

**Swagger**: `http://localhost:5095/swagger`

## Local Development

### Setup

```bash
# Configure database connection
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-supabase-connection-string"
dotnet user-secrets set "DatabaseProvider" "PostgreSQL"

# Restore and run
dotnet restore
dotnet run
```

API runs at `http://localhost:5095/`

### Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations (automatic on startup)
```

## CI/CD Workflow

### Branches
- `main`/`master` - Production branch
- `develop` - Development branch
- `feature/*` - Feature branches

### GitHub Actions
- **Build**: Runs on PRs and pushes (builds, tests, validates)
- **Release**: Auto-generates version tags on merge to main (v0.1, v0.2, etc.)

### Development Flow
1. Create feature branch from `develop`
2. Open PR to `develop` → build runs
3. Merge to `develop`
4. Open PR from `develop` to `main`
5. Merge to `main` → **auto-release created**

## Deploy to Railway

1. Push to GitHub
2. Create Railway project from repo
3. Add PostgreSQL database
4. Set environment variables:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   DatabaseProvider=PostgreSQL
   DATABASE_URL=${{Postgres.DATABASE_URL}}
   ```
5. Deploy automatically on push

Migrations run automatically on startup.
