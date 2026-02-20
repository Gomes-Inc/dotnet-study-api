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

2. **Configure environment variables**

Copy `.env.example` to `.env` and configure your database:
```bash
cp .env.example .env
```

Edit `.env`:
```bash
DATABASE_URL=Host=localhost;Port=5432;Database=your_db;Username=your_user;Password=your_password;Pooling=true;
DatabaseProvider=PostgreSQL
```

3. **Restore and run**
```bash
dotnet restore
dotnet run
```

API runs at `http://localhost:5000/` (HTTP) and `https://localhost:5001/` (HTTPS)

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update
```

**Note**: Migrations are applied automatically on application startup.

## Docker

### Running with Docker

```bash
# Build and run
docker-compose up -d --build
```

API will be available at:
- **HTTP:** http://localhost:5000
- **HTTPS:** https://localhost:5001

**Important**: Configure `DATABASE_URL` environment variable in `.env` file or in docker-compose.yml

### Stop

```bash
docker-compose down
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
