# Company.SampleService

API generated with the **Modular Clean Architecture** template.

## Projects

| Project | Description |
|---|---|
| `Domain` | Entities, domain exceptions, repository interfaces |
| `Application` | Use cases, CQRS abstractions, validation |
| `Messages` | Shared message contracts (events) |
| `Infrastructure.Auth` | JWT authentication, BCrypt password hashing _(optional)_ |
| `Infrastructure.SqlServer` | EF Core + SQL Server persistence _(optional)_ |
| `Infrastructure.PostgreSql` | EF Core + PostgreSQL persistence _(optional)_ |
| `Infrastructure.MongoDb` | MongoDB.Driver persistence _(optional)_ |
| `Infrastructure.Kafka` | Kafka message publishing via Confluent.Kafka _(optional)_ |
| `WebApi` | ASP.NET Core Web API — controllers, middleware, DI |
| `CommomTestsUtilities` | Shared test builders and fakes |
| `UnitTests` | Use case unit tests |
| `IntegratedTests` | Controller integration tests |
| `FunctionalTests` | BDD scenarios with Reqnroll |

---

## Getting started

### Install the template

```bash
dotnet new install ./path/to/DotnetCleanArchitecture.TemplatePack.csproj
```

Or from NuGet (once published):

```bash
dotnet new install DotnetCleanArchitecture.Templates
```

### Create a new project

```bash
dotnet new cleanarchapi -n MyCompany.MyService
```

This generates the project inside a `MyCompany.MyService/` folder using the default options:
SQL Server + MediatR + FluentValidation + Serilog + OpenTelemetry + Swagger + Auth.

---

## Template options

All options are boolean flags. Pass `--<flag>` to enable or `--<flag> false` to disable.

### Persistence (multiple allowed)

| Flag | Default | Description |
|---|---|---|
| `--use-sqlserver` | `true` | EF Core with SQL Server |
| `--use-postgresql` | `false` | EF Core with PostgreSQL |
| `--use-mongodb` | `false` | MongoDB.Driver |

### Messaging (multiple allowed)

| Flag | Default | Description |
|---|---|---|
| `--use-kafka` | `false` | Confluent.Kafka message publisher |

### Libraries

| Flag | Default | Description |
|---|---|---|
| `--use-mediatr` | `true` | MediatR for CQRS dispatch. When disabled, use cases are injected directly into controllers |
| `--use-fluentvalidation` | `true` | FluentValidation for request validation |
| `--use-serilog` | `true` | Serilog for structured logging |
| `--use-opentelemetry` | `true` | OpenTelemetry tracing and metrics |
| `--use-swagger` | `true` | Swashbuckle + API versioning |
| `--use-auth` | `true` | JWT Bearer authentication + BCrypt |

---

## Usage examples

### Minimal — SQL Server only, all libs enabled (default)

```bash
dotnet new cleanarchapi -n MyCompany.MyService
```

### PostgreSQL instead of SQL Server

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-sqlserver false \
  --use-postgresql
```

### Multiple databases — SQL Server + MongoDB

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-mongodb
```

### PostgreSQL + Kafka

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-sqlserver false \
  --use-postgresql \
  --use-kafka
```

### All databases + Kafka

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-postgresql \
  --use-mongodb \
  --use-kafka
```

### Without MediatR (direct use case injection)

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-mediatr false
```

Controllers will inject use cases directly:

```csharp
public ItemsController(
    ICommandHandler<CreateItemRequest, CreateItemResponse> createItem,
    IQueryHandler<GetItemByIdRequest, GetItemByIdResponse> getItemById)
```

### Without authentication

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-auth false
```

### Minimal setup — no observability, no swagger, no auth

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-auth false \
  --use-swagger false \
  --use-serilog false \
  --use-opentelemetry false
```

### Full example — MongoDB + Kafka, no MediatR, no auth

```bash
dotnet new cleanarchapi -n MyCompany.MyService \
  --use-sqlserver false \
  --use-mongodb \
  --use-kafka \
  --use-mediatr false \
  --use-auth false
```

---

## Running the project

```bash
# Restore and build
dotnet restore
dotnet build

# Run all tests
dotnet test

# Start with Docker Compose (spins up selected databases and Seq)
docker compose up -d
dotnet run --project src/Company.SampleService.WebApi
```

---

## Project structure

```
src/
  Company.SampleService.Domain/
  Company.SampleService.Application/
  Company.SampleService.Messages/
  Company.SampleService.Infrastructure.Auth/       # present if --use-auth
  Company.SampleService.Infrastructure.SqlServer/  # present if --use-sqlserver
  Company.SampleService.Infrastructure.PostgreSql/ # present if --use-postgresql
  Company.SampleService.Infrastructure.MongoDb/    # present if --use-mongodb
  Company.SampleService.Infrastructure.Kafka/      # present if --use-kafka
  Company.SampleService.WebApi/
tests/
  Company.SampleService.CommomTestsUtilities/
  Company.SampleService.UnitTests/
  Company.SampleService.IntegratedTests/
  Company.SampleService.FunctionalTests/
```
