# Run and Troubleshooting

## Prerequisites

Install .NET 10 SDK and SQL Server. Confirm both are available:

```bash
dotnet --version
```

## Start API

```bash
dotnet restore GymPro.sln
dotnet run --project src/GymPro.Api
```

Open `https://localhost:7181/swagger`.

## Start UI

Open a second terminal:

```bash
dotnet run --project src/GymPro.Web
```

Open `https://localhost:7281`.

## SQL Server connection

Edit `src/GymPro.Api/appsettings.json`.

LocalDB example:

```json
"GymDb": "Server=(localdb)\\MSSQLLocalDB;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

SQL Express example:

```json
"GymDb": "Server=.\\SQLEXPRESS;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

SQL username/password example (use local secrets/environment variables for real credentials):

```json
"GymDb": "Server=localhost;Database=GymManagementDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
```

The API creates the database and tables on first startup using `EnsureCreatedAsync()`. Do not first execute the SQL script if the database already exists; use either the automatic creation or the reference script.

## If the UI cannot reach the API

1. Start the API first.
2. Confirm the API is listening on `https://localhost:7181`.
3. Confirm `ApiBaseUrl` in `src/GymPro.Web/appsettings.json` matches the API HTTPS URL.
4. If you change the API port, update the UI `ApiBaseUrl`.
5. Trust the local ASP.NET Core development HTTPS certificate if prompted:

```bash
dotnet dev-certs https --trust
```

## Build

```bash
dotnet build GymPro.sln
```

## EF Core migrations (optional next step)

This sample uses `EnsureCreatedAsync` for a quick start. If you want schema versioning, switch to migrations and use the EF Core tools:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project src/GymPro.Data --startup-project src/GymPro.Api
dotnet ef database update --project src/GymPro.Data --startup-project src/GymPro.Api
```

For a migration-based setup, remove the `EnsureCreatedAsync()` call from API startup and use `Database.MigrateAsync()` instead.
