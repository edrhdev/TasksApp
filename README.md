# Task Management App
A full-stack Task Management app built with **.NET 10** (Clean Architecture), **Blazor WASM**, and **React (TS)**.

## Key Assumptions & Features Added
- **Clean Architecture & Modularity**: Decoupled backend layers (`Domain`, `Application`, `Infrastructure`, `WebAPI`) and modular frontend UI components.
- **RFC 7807 Exception Handling**: Custom middleware maps `UserException` and generic `Exception` to standardized `ProblemDetails`.
- **Result Pattern**: Handled API failures gracefully using an `ApiResult` / `ApiResult<T>` wrapper on the frontend.
- **Optimistic UI & Concurrency Protection**: Checkbox toggle updates state immediately with automatic rollback on server error, using TanStack Query mutation guards to prevent race conditions.
- **Delete Confirmation**: Deletion is protected by accessible modal confirmation dialogs (`AlertDialog`).

## Project Structure
The solution is organized into the following projects:
- **TasksApp.WebAPI:** The .NET RESTful API backend.
- **TasksApp.ReactTS:** React front-end (using TanStack Query, Shadcn/UI, and Sonner).
- **TasksApp.BlazorWASM:** Blazor WebAssembly alternative front-end with Radzen components.

## Requirements
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Node 22+](https://nodejs.org/en/download/)
- [pnpm](https://pnpm.io/installation)

## Setup configuration
- Listening port of the backend app is configured in `appsettings.Development.json`. Default: `http://localhost:5000`.
- Database connection string is in `appsettings.Development.json`. Default: `Data Source=TasksApp.db` (SQLite).
- React frontend uses `.env` (`VITE_API_BASE_URL=http://localhost:5000`).
- Blazor WASM frontend uses `wwwroot/appsettings.Development.json`.

## Quick Start

Clone the repository and run the following commands to start the backend and frontend.

### 1. Backend (.NET WebAPI)
```bash
cd src/Presentation/TasksApp.WebAPI
dotnet restore
dotnet run
```
*Database (SQLite) is created automatically on startup.*

### 2. Frontend (Choose one)

Option A: Blazor WASM
```bash
cd src/Presentation/TasksApp.BlazorWASM
dotnet restore
dotnet run
```

Option B: React (TypeScript)
```bash
cd src/Presentation/TasksApp.ReactTS
pnpm install
pnpm run dev
```

## What Was Left Out (Trade-offs for time)

For this technical assessment, the following features were intentionally omitted but are recommended for a production-ready application:

- **Authentication & Authorization**: All endpoints are open. Multi-tenancy or user isolation should be added via JWT / ASP.NET Core Identity.
- **Structured Telemetry**: Basic console logging is used. Production should integrate Serilog / OpenTelemetry to an APM platform.
- **Caching, Pagination, Sorting, & Filtering**: Left out to keep API contracts minimal for this scope.
- **Database Migrations**: EnsureCreated() is used for auto-creation on launch. Proper EF Core migrations should be used in production.
- **Swagger / OpenAPI Specs**: Excluded to minimize boilerplate, but recommended for API documentation.
- **Unit & Integration Tests**: Code is structured around DI and testability, but test projects were omitted to fit the assessment timeframe.
- **Docker & CI/CD Pipelines**: Assumed manual execution for local evaluation.