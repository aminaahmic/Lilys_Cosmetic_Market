# Lily's Cosmetic Market

Monorepo for the RS1 project:
- `Lilys_CM.Backend` - ASP.NET Core 8 API (Clean Architecture + EF Core + SQL Server)
- `Lilys_CM.Frontend` - Angular application (admin + public catalog)

## Current Product Scope

Product flow is now covered end-to-end:
- Product CRUD (create, list, update, delete)
- 5 filters (category, subcategory, price min, price max, name/brand search)
- Backend paging + backend filters
- Wizard-style product create/edit screens
- Stock adjustment endpoint and stock movement history
- Public catalog page connected to live product/category API

## Prerequisites

Install these before first run:
- .NET SDK `8.0.419` (see `global.json`)
- SQL Server instance (`(localdb)\\MSSQLLocalDB` or `localhost\\SQLEXPRESS`)
- Node.js + npm (Angular CLI is used through npm scripts)

## First Run (Fresh Pull)

1. Restore backend packages:
```powershell
cd Lilys_CM.Backend
dotnet restore Lilys_CM.Backend.sln
```

2. Check backend local config:
- Open `Lilys_CM.Backend/Lilys_CM.API/appsettings.Development.json`
- Set `ConnectionStrings:Main` to your local SQL Server instance if needed

3. Run backend:
```powershell
dotnet run --project Lilys_CM.API
```
- On startup, migrations are applied automatically.
- Development seed inserts demo categories and demo users:
  - `admin@lilys.local` / `Admin123!`
  - `customer@lilys.local` / `Customer123!`

4. Start frontend:
```powershell
cd ..\Lilys_CM.Frontend
npm install
npm start
```

5. Open app:
- Frontend: `http://localhost:4200`
- API Swagger: `http://localhost:5177/swagger`

## Teammate Notes (Suljo Setup)

If API cannot connect to DB after pull:
- Update only `ConnectionStrings:Main` in `appsettings.Development.json`.
- Keep the rest of the file as-is for local development.

Two valid connection string examples:
- LocalDB:
```text
Server=(localdb)\MSSQLLocalDB;Database=Lilys_CM_db;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=true
```
- SQLEXPRESS:
```text
Server=localhost\SQLEXPRESS;Database=Lilys_CM_db;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=true
```

If you hit `requires encryption but this machine does not support it`:
- Keep `Encrypt=False` and `TrustServerCertificate=True` in the connection string.
- Try LocalDB first (`(localdb)\MSSQLLocalDB`) if your SQLEXPRESS instance enforces encryption.

## Notes About Warnings

- `NU1900` warnings were caused by restricted/offline network access to NuGet audit feed.
- Backend now disables NuGet audit during local builds (`Lilys_CM.Backend/Directory.Build.props`) to keep local output clean.

## Build Commands

Backend:
```powershell
cd Lilys_CM.Backend
dotnet build Lilys_CM.Backend.sln -v minimal
```

Frontend:
```powershell
cd Lilys_CM.Frontend
npm run build
```
