# Clean Architecture Solution Template
![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![C#](https://img.shields.io/badge/C%23-13-239120)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

A modern, clean architecture template for .NET 9 applications implementing Domain-Driven Design (DDD) principles with C# 13 features.

## Architecture Overview

This solution follows the Clean Architecture principles proposed by Robert C. Martin (Uncle Bob), organized in concentric circles:

- **Domain Layer** (Core): Business entities, interfaces, and logic
- **Application Layer**: Use cases, interfaces, and DTOs
- **Infrastructure Layer**: External concerns and implementations
- **Presentation Layer**: API endpoints and UI components

## Key Features

- Full implementation of Clean Architecture principles
- Domain-Driven Design (DDD) patterns
- CQRS with MediatR
- Entity Framework Core 9
- Rich domain models with encapsulated business logic
- Comprehensive validation using FluentValidation
- Unit and integration testing setup
- API versioning
- Structured logging with Serilog
- OpenAPI documentation
- Health checks

## Prerequisites

- .NET 9 SDK
- SQL Server (or your preferred database)
- Visual Studio 2025 or later / VS Code
- Docker (optional)

## Project Structure

```
src/
├── Domain/
│   ├── Entities/
│   ├── Events/
│   ├── Exceptions/
│   └── ValueObjects/
├── Application/
│   ├── Common/
│   ├── Features/
│   ├── Interfaces/
│   └── Services/
├── Infrastructure/
│   ├── Identity/
│   ├── Persistence/
│   └── Services/
└── WebAPI/
    ├── Controllers/
    ├── Filters/
    └── Middleware/

tests/
├── UnitTests/
├── IntegrationTests/
└── FunctionalTests/
```

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/yourusername/yourrepository.git
```

2. Configure the database connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDbName;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Apply database migrations:
```bash
dotnet ef database update
```

4. Run the application:
```bash
dotnet run --project src/WebAPI
```

## Domain-Driven Design

This template implements the following DDD patterns:

- Aggregates
- Entities
- Value Objects
- Domain Events
- Repository Pattern
- Specification Pattern

## Testing Strategy

- **Unit Tests**: Testing business logic in isolation
- **Integration Tests**: Testing components with their dependencies
- **Functional Tests**: End-to-end API testing

## Docker Support

Build and run the application using Docker:

```bash
docker build -t clean-architecture .
docker run -p 8080:80 clean-architecture
```

## Configuration

The application supports different configuration methods:

- JSON configuration files
- Environment variables
- Azure Key Vault (optional)
- User secrets (Development)

## API Documentation

Access the Swagger UI at `/swagger` when running in Development mode.

## Performance Considerations

- Asynchronous operations throughout
- Response caching
- Efficient database queries
- Memory caching where appropriate

## Security Features

- JWT authentication
- HTTPS enforcement
- CORS policies
- Anti-forgery protection
- Input validation
- Security headers

## Logging

Structured logging is implemented using Serilog with support for multiple sinks:

- Console
- File
- SQL Server
- Application Insights (optional)

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- Clean Architecture by Robert C. Martin
- [Microsoft eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)
- Domain-Driven Design by Eric Evans

---

*Note: This template is continuously updated to incorporate the latest .NET features and best practices.*
