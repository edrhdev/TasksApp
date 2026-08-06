# Task Management App
A full-stack Task Management app built with **.NET 10** (Clean Architecture), **Blazor WASM**, and **React (TS)**.

### Key Assumptions & Features Added
- **Clean Architecture & Modularity**: Decoupled backend layers (`Domain`, `Application`, `Infrastructure`, `WebAPI`) and modular frontend UI components.
- **RFC 7807 Exception Handling**: Custom middleware maps `UserException` and regular `Exceptions` to standardized `ProblemDetails`.
- **Result Pattern**: Handled API failures gracefully using a non-throwing `ApiResult` / `ApiResult<T>` wrapper on the frontend.
- **Optimistic UI**: Checkbox toggle updates state immediately with auto-rollback on server error.

## Project Structure
The solution is organized into the following projects:
- **TasksApp.WebAPI:** The .NET RESTful API backend.
- **TasksApp.ReactTS:** React front-end (as requested).
- **TasksApp.BlazorWASM:** Blazor WebAssembly alternative front-end with Radzen components.

## Quick Start

### 1. Backend (.NET WebAPI)
```bash
cd TasksApp.WebAPI
dotnet run
```
*Database (SQLite) is created automatically on startup.*

### 2. Frontend (Choose one)

Option A: Blazor WASM
```bash
cd TasksApp.BlazorWASM
dotnet run
```

Option B: React (TypeScript)
```bash
cd tasksapp.reactts
pnpm install
pnpm run dev
```

## What Was Left Out (Scope Limits)
-   **Authentication/Authorization:** Open endpoints; no JWT or multi-tenancy.
-   **Pagination & Filtering:** Simple list fetch for scope simplicity.
-   **Structured Telemetry:** Uses native standard loggers instead of external aggregators (e.g., Serilog).


## What was left out:

For this basic task management app, the following features were not implemented but should be considered for a production-ready application:

- **Authentication or authorization**: all endpoints are open and same database is shared for all users, should be implemented to manage access control and separation of tasks per users.
- **Structured Telemetry**: basic console logging is implemented. Should be replaced with a robust logging solution in production like Serilog or NLog to log to a centralized observability platform.
- **Pagination, sorting or filtering**: should be implemented to handle large datasets efficiently.
- **Caching and limiting mechanisms**: caching and limiting strategies should be implemented to improve performance and reduce load on the database.
- **Custom exceptions**: Currently the app uses a single `UserException` for controlled errors the user can see, and all other exceptions are logged and returned as generic errors to avoid showing sensitive information. Detailed Exceptions are required as the project grows.
- **Swagger not included**: Should be added to provide API documentation and testing capabilities.
-   **Unit & Integration Testing:** Code structured for DI and testability, but test projects omitted for time.
-   **Docker & CI/CD Pipelines:** Manual execution assumed for evaluation.