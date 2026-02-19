# App Test API

.NET 10 API with Entity Framework Core using PostgreSQL (Supabase).

## Development server

Configure your Supabase connection in environment variables or User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-supabase-connection-string"
dotnet user-secrets set "DatabaseProvider" "PostgreSQL"
```

Run locally:
```bash
dotnet run
```

API available at `http://localhost:5095/`.

## Deploy to Railway

1. Push to GitHub
2. Create new Railway project from repo
3. Add PostgreSQL database
4. Set environment variables:

```
ASPNETCORE_ENVIRONMENT=Production
DatabaseProvider=PostgreSQL
DATABASE_URL=${{Postgres.DATABASE_URL}}
```

5. Generate domain

Migrations run automatically.

## Running migrations

To create a new migration:

```bash
dotnet ef migrations add MigrationName
```

Migrations are applied automatically when the application starts.
