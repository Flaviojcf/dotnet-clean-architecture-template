# Modular .NET Architecture Templates

This repository contains a `dotnet new` template pack.

Available templates:

- `cleanarchapi`: Clean Architecture API solution.
- `vsa-worker`: Vertical Slice Architecture worker.

The API template generates:

- `Domain`, `Application`, `Messages`, `Infrastructure.Auth`, and `WebApi`
- optional persistence projects: `SqlServer`, `PostgreSql`, or `MongoDb`
- optional `Infrastructure.Kafka`
- `CommomTestsUtilities`, `UnitTests`, `IntegratedTests`, and `FunctionalTests`

## Install locally

```powershell
dotnet new install . --force
```

## Generate a project

Clean Architecture API:

```powershell
dotnet new cleanarchapi -n MinhaEmpresa.Orders
```

VSA worker:

```powershell
dotnet new vsa-worker -n MinhaEmpresa.Notifications
```

Options:

- `--useSqlServer`: include SQL Server persistence, default `true`
- `--usePostgreSql`: include PostgreSQL persistence, default `false`
- `--useMongoDB`: include MongoDB persistence, default `false`
- `--useKafka`: include Kafka messaging, default `false`
- `--useCiCd`: include CI/CD workflows, default `true`

## Pack as NuGet template

```powershell
dotnet pack .\DotnetCleanArchitecture.TemplatePack.csproj
```
