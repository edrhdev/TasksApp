# TasksApp — Full-Stack Task Management Application

A full-stack Task Management application built with **.NET Core**, **React** and **Blazor WebAssembly**, following **Clean Architecture** principles, strict separation of concerns, and production-ready design patterns.

## Features

- **Clean Architecture Layout:** Domain-Driven Design (DDD) principles separating Core Logic, Application Use Cases, Infrastructure (EF Core + SQLite), and Presentation.
- **RESTful API Backend:** Clean API endpoints supporting full Task lifecycle management (Create, Read, Toggle status) with standardized error responses (`ProblemDetails`).
- **Interactive Blazor and React UI:** Component-based, responsive front-end delivering smooth state updates and data binding.
- **Data Persistence:** EF Core backed by SQLite with seed data for rapid demonstration.
- **Containerization:** Fully Dockerized with `docker-compose` for instant zero-dependency execution.

## Project Structure and Setup

TODO: Add a detailed project structure diagram and explanation.

## What was left out:

- No authentication or authorization mechanisms (e.g., JWT, OAuth2). For this basic task management app, all endpoints are open.
- No use of mapping libraries like AutoMapper; manual mapping is used for simplicity.