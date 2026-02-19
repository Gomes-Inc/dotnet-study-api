using app_test_api.Data;
using app_test_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = 
    Environment.GetEnvironmentVariable("DATABASE_URL") 
    ?? builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = null;
}

var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";

if (string.IsNullOrEmpty(connectionString))
{
    var env = builder.Environment.EnvironmentName;
    throw new InvalidOperationException(
        $"No connection string found in environment '{env}'. " +
        "Set DATABASE_URL, CONNECTION_STRING or ConnectionStrings:DefaultConnection in appsettings or environment variables."
    );
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
});

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
