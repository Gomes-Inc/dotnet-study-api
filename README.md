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
- PostgreSQL database (Docker recommended)

### Setup

1. **Clone the repository**
```bash
git clone <repository-url>
cd app-test-api
```

2. **Configure Database Connection**

**Option A: Using Docker Compose (recommended)**

Start both PostgreSQL database and API together:
```bash
docker-compose up -d
```

This automatically starts:
- PostgreSQL database on port `5432`
- API on ports `5000` (HTTP) and `5001` (HTTPS)

Access at: http://localhost:5000

**Option B: Using Local PostgreSQL + .NET CLI**

If you prefer to run the API locally:

1. Start only PostgreSQL with Docker:
```bash
docker-compose up postgres -d
```

2. Run the API with .NET CLI:
```bash
dotnet restore
dotnet run
```

**Option C: Using existing PostgreSQL**
- Make sure PostgreSQL is running on `localhost:5432`
- Update connection string in `appsettings.Development.json` or set `DATABASE_URL` environment variable

**Using Visual Studio:**
- Open the solution and press `F5`
- Use the "http" or "https" profile
- Ensure PostgreSQL is running (via Docker or local instance)

API runs at:
- **HTTP:** http://localhost:5095
- **HTTPS:** https://localhost:7069

**Note:** The Visual Studio launch profiles are pre-configured with local PostgreSQL connection. If you need different settings, modify `Properties/launchSettings.json` or set environment variables.

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update
```

**Note**: Migrations are applied automatically on application startup.

## Docker

### Full Stack with Docker Compose

The docker-compose setup runs the complete application stack including PostgreSQL database and the API.

**Start services:**

```bash
docker-compose up -d --build
```

This will start:
- **PostgreSQL database** on port `5432` (with data persistence)
- **API** on ports `5000` (HTTP) and `5001` (HTTPS)

Access the API:
- **Swagger UI:** http://localhost:5000

**View logs:**
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f api
docker-compose logs -f postgres
```

**Stop services:**

```bash
docker-compose down
```

**Stop and remove all data:**

```bash
docker-compose down -v
```

### Docker Configuration

The docker-compose includes:
- PostgreSQL 16 Alpine
- Health checks for database readiness
- Named volumes for data persistence
- Bridge networking between services
- Automatic database migrations on API startup

**Database Credentials (Development):**
- User: `postgres`
- Password: `postgres123`
- Database: `dotnet_study_db`

**Optional:** Create a `.env` file in the root directory to override environment variables:
```env
DATABASE_URL=Host=postgres;Port=5432;Database=dotnet_study_db;Username=postgres;Password=postgres123;
ASPNETCORE_ENVIRONMENT=Development
DatabaseProvider=PostgreSQL
```

> **Note**: This docker-compose setup is for **local development only**. For production deployments, use hosting services like Railway, Render, or Heroku. See the [Production Deployment](#production-deployment) section below.

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
├── Controllers/        # API endpoints (MessageController, UserController)
├── Data/              # Database context (AppDbContext)
├── Migrations/        # EF Core migrations
├── Models/            # Data models and DTOs
│   ├── DTOs/         # Data transfer objects
│   └── Request/      # Request DTOs
├── Services/          # Business logic (MessageService, UserService)
│   └── Interfaces/   # Service contracts
├── Properties/        # Launch settings
├── docker-compose.yml # Docker orchestration
├── Dockerfile         # API container image
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
