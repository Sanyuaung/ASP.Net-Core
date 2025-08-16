# NZWalks.API

An ASP.NET Core Web API (NET 8) for managing New Zealand walking regions and tracks. It exposes CRUD endpoints for Regions and Walks, backed by SQL Server via Entity Framework Core. Swagger is included for interactive API exploration.

## Tech stack
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 9 (SqlServer, Tools)
- SQL Server
- Swagger (Swashbuckle)

## Project layout
- NZWalks.API
  - Controllers
    - RegionsController.cs
    - WalksController.cs
    - WeatherForecastController.cs
    - StudendsController.cs (sample)
  - Data
    - NZWalksDbContext.cs
  - Migrations
  - Models
    - Domain: Region, Walk, Difficulty
    - DTO: RegionDto, AddRegionRequestDto, UpdateRegionRequest, WalkDto, AddWalkRequestDto, UpdateWalkRequestDto
  - Program.cs
  - appsettings.json

## Prerequisites
- .NET SDK 8.x
- SQL Server (localdb or full instance)
- Optional: Visual Studio 2022 or VS Code
- Optional: EF Core CLI
  - dotnet tool install --global dotnet-ef

## Configuration
The API uses a SQL Server connection string named DefaultConnection.
- File: NZWalks.API/appsettings.json
- Key: ConnectionStrings:DefaultConnection

Example:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=Udemy;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Override via environment variable if needed:
- Windows PowerShell: `$env:ConnectionStrings__DefaultConnection="..."`
- Linux/macOS: `export ConnectionStrings__DefaultConnection="..."`

## Database and migrations
Migrations are included (Initial Migration). To create/update the database:

Using EF Core CLI:
- Restore tools (first time only): `dotnet tool install --global dotnet-ef`
- From NZWalks.API directory: `dotnet ef database update`

Using Package Manager Console (Visual Studio):
- Set Default project to NZWalks.API
- Run: `Update-Database`

## Build and run
From the solution root or NZWalks.API directory:
- Restore: `dotnet restore`
- Build: `dotnet build`
- Run: `dotnet run --project NZWalks.API`

Swagger UI:
- Browse to https://localhost:{port}/swagger after the app starts.

## Data model
- Region
  - Id (Guid)
  - Name (string)
  - Code (string)
  - RegionImageUrl (string?)
- Difficulty
  - Id (Guid)
  - Name (string)
- Walk
  - Id (Guid)
  - Name (string)
  - RegionId (Guid) [FK -> Region]
  - DifficultyId (Guid) [FK -> Difficulty]

## API endpoints
Base URL: `/`

Regions
- GET /api/regions
- GET /api/regions/{id}
- POST /api/regions
  - Body:
    ```json
    {
      "name": "Auckland",
      "code": "AKL",
      "regionImageUrl": "https://example.com/akl.jpg"
    }
    ```
- PUT /api/regions/{id}
  - Body:
    ```json
    {
      "name": "Auckland",
      "code": "AKL",
      "regionImageUrl": "https://example.com/akl-new.jpg"
    }
    ```
- DELETE /api/regions/{id}

Walks
- GET /api/walks
- GET /api/walks/{id}
- POST /api/walks
  - Body:
    ```json
    {
      "name": "Coastal Track",
      "regionId": "<region-guid>",
      "difficultyId": "<difficulty-guid>"
    }
    ```
- PUT /api/walks/{id}
  - Body:
    ```json
    {
      "name": "Coastal Track (Updated)",
      "regionId": "<region-guid>",
      "difficultyId": "<difficulty-guid>"
    }
    ```
- DELETE /api/walks/{id}

Other sample endpoints
- GET /WeatherForecast
- GET /api/studends

## Notes
- Swagger is enabled in Development by default. Adjust Program.cs if you want it in Production.
- Controllers map Domain models to DTOs to shape responses and requests.
- WalksController validates RegionId and DifficultyId exist before creating or updating a Walk.

## Troubleshooting
- SQL connection issues: verify server name, credentials, and TrustServerCertificate setting.
- Development HTTPS certificate: `dotnet dev-certs https --trust` (if browser warns about HTTPS).
- EF CLI not found: install with `dotnet tool install --global dotnet-ef` and re-open terminal.

## License
No license specified.
