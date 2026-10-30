# GymPro – Gym Management System

A simple full-stack gym management sample built with:
- .NET 10 Blazor Web App (Interactive Server) for the UI
- ASP.NET Core Web API for backend CRUD operations
- Entity Framework Core 10
- Microsoft SQL Server

## Features
- Dashboard: member, trainer, membership-plan and payment totals
- Members: create, list, edit, delete and search
- Trainers: create, list, edit and delete
- Membership plans: create, list, edit and delete
- Payments: create, list and delete
- SQL Server persistence; database is created automatically on first API startup
- Swagger/OpenAPI in Development

## Requirements
- .NET 10 SDK
- SQL Server (LocalDB, SQL Server Express, Developer edition, or another SQL Server instance)
- Visual Studio 2022 with a .NET 10-capable update, or VS Code

## Run

1. Update the connection string in `src/GymPro.Api/appsettings.json`.
   The default is SQL Server LocalDB:
   `Server=(localdb)\\MSSQLLocalDB;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;`

2. Open `GymPro.sln` in Visual Studio, or use a terminal at the solution root.

3. Start the API:
   ```bash
   dotnet run --project src/GymPro.Api
   ```
   API base URL: `https://localhost:7181` (HTTP: `http://localhost:5181`)
   Swagger: `https://localhost:7181/swagger`

4. In a second terminal, start the Blazor UI:
   ```bash
   dotnet run --project src/GymPro.Web
   ```
   UI: `https://localhost:7281` (HTTP: `http://localhost:5281`)

If your local launch URLs differ, use the URLs printed by `dotnet run` and update `ApiBaseUrl` in `src/GymPro.Web/appsettings.json`.

## Database
The API uses `Database.EnsureCreated()` to create the database and tables automatically. This keeps the sample easy to start. For an evolving production application, replace this with EF Core migrations (`dotnet ef migrations add InitialCreate` and `dotnet ef database update`).

A SQL reference script is provided at `database/GymManagementDB.sql`.

## Sample usage
1. Open the UI and add membership plans.
2. Add members and trainers.
3. Record payments against existing members.
4. Review the dashboard totals.

## Notes
- This is a learning/sample application, not a production-ready financial or identity system.
- No login/role system or payment gateway is included.
- Payment records are manually entered; no real money is collected.
- The project source was generated in this environment, but .NET SDK is not installed here, so compilation/runtime execution could not be verified here. Run the commands above on a machine with the .NET 10 SDK and SQL Server.
