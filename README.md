# App Test API

REST API built with .NET 10 and Entity Framework Core with PostgreSQL support.

## Stack

- .NET 10
- Entity Framework Core 10
- PostgreSQL
- Swagger/OpenAPI

## Features

- Message management endpoints
- User management endpoints
- Automatic database migrations
- PostgreSQL or SQL Server support
- Docker ready

## API Endpoints

### Messages
```
GET    /api/message          # List all messages
GET    /api/message/{id}     # Get message by ID
POST   /api/message          # Create new message
DELETE /api/message/{id}     # Delete message
```

### Users
```
GET    /api/user             # List all users
GET    /api/user/{id}        # Get user by ID
POST   /api/user             # Create new user
DELETE /api/user/{id}        # Delete user
```

**Swagger**: Available at root path `/`

## Local Development

### Prerequisites

- .NET 10 SDK
- PostgreSQL database (local or cloud)

### Setup

1. **Clone the repository**
```bash
git clone <repository-url>
cd app-test-api
```

2. **Start PostgreSQL (choose one option)**

**Option A: Using Docker (recommended)**
```bash
docker-compose up postgres -d
```

**Option B: Using existing PostgreSQL**
- Make sure your PostgreSQL is running on `localhost:5432`

3. **Run the API**

**Using Visual Studio:**
- Open the solution and press `F5`
- Use the "http" or "https" profile

**Using command line:**
```bash
dotnet restore
dotnet run
```

API runs at:
- **HTTP:** http://localhost:5095
- **HTTPS:** https://localhost:7069

**Note:** The Visual Studio launch profiles are pre-configured with local PostgreSQL connection. If you need different settings, create a `.env` file or modify `Properties/launchSettings.json`.

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update
```

**Note**: Migrations are applied automatically on application startup.

## Docker

> **Note**: This docker-compose setup is for **local development only**. For production deployments, use hosting services like Railway, Render, or Heroku. See the [Production Deployment](#production-deployment) section below.

### Quick Start with Docker

Run the API with PostgreSQL database included:

```bash
docker-compose up -d --build
```

This will start:
- PostgreSQL database on port `5432`
- API on ports `5000` (HTTP) and `5001` (HTTPS)

Access the API:
- **Swagger UI:** http://localhost:5000

### Stop

```bash
docker-compose down
```

### Stop and Remove Data

```bash
docker-compose down -v
```

## Production Deployment

### Environment Variables

Configure these environment variables in your hosting service:

| Variable | Required | Description | Example |
|----------|----------|-------------|---------|
| `DATABASE_URL` | Yes | PostgreSQL connection string | `Host=...;Database=...;Username=...;Password=...` |
| `DatabaseProvider` | No | Database provider (defaults to PostgreSQL) | `PostgreSQL` or `SqlServer` |
| `ASPNETCORE_ENVIRONMENT` | Recommended | Environment name | `Production` |

### Deploy to Railway

1. Push to GitHub
2. Create Railway project from repository
3. Add PostgreSQL database service
4. Set environment variable:
   ```
   DATABASE_URL=${{Postgres.DATABASE_URL}}
   ```
5. Deploy automatically on push

### Deploy to Render/Heroku

Similar process - configure `DATABASE_URL` environment variable with your PostgreSQL connection string.

## Database Providers

The API supports both PostgreSQL and SQL Server. Configure via `DatabaseProvider` environment variable:

- `PostgreSQL` (default)
- `SqlServer`

## Project Structure

```
├── Controllers/        # API endpoints
├── Data/              # Database context
├── Migrations/        # EF Core migrations
├── Models/            # Data models
│   └── Request/      # Request DTOs
├── Services/          # Business logic
│   └── Interfaces/   # Service contracts
└── Program.cs         # Application entry point
```

## Contributing

1. Create a feature branch from `develop`
2. Make your changes
3. Submit a PR to `develop`
4. After review, merge to `develop`
5. Create PR from `develop` to `main` for production release

## License

See LICENSE file for details.
