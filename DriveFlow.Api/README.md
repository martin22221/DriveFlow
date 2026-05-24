# DriveFlow API

DriveFlow is a car rental Web API built with ASP.NET Core 8.

## Features

- JWT Authentication
- ASP.NET Identity
- Car Management
- Booking System
- Reviews System
- Admin Dashboard
- Role-based Authorization
- Entity Framework Core
- SQL Server Database
- Swagger API Documentation

## Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- Swagger
- NUnit

## Architecture

- DriveFlow.Api
- DriveFlow.Core
- DriveFlow.Infrastructure
- DriveFlow.Tests


## Authentication

The API uses JWT Bearer Authentication.

Protected endpoints require a valid JWT token.

## Testing

The project includes NUnit unit tests for the business logic layer.

## API Documentation

Swagger is enabled for testing all endpoints.


## Default Admin Account

Email: admin@driveflow.com  
Password: Admin123							




## Main Endpoints

- /api/Auth
- /api/Cars
- /api/Categories
- /api/Bookings
- /api/Reviews
- /api/Admin