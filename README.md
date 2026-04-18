# Modular Clean Architecture API Template

This folder contains a `dotnet new` template pack that generates a Clean Architecture API solution with:

- `Domain`, `Application`, `Messages`, `Infrastructure.Auth`, and `WebApi`
- one persistence project chosen at generation time: `SqlServer`, `PostgreSql`, or `MongoDb`
- optional `Infrastructure.Kafka`
- `CommomTestsUtilities`, `UnitTests`, `IntegratedTests`, and `FunctionalTests`

## Install locally

```powershell
dotnet new install C:\dev\study\fiap\dotnet-clean-architecture-template\templates\clean-api
```

## Generate a project

```powershell
dotnet new cleanarchapi -n MinhaEmpresa.Orders --database sqlserver --messaging kafka
```

Options:

- `--database`: `sqlserver`, `postgresql`, `mongodb`
- `--messaging`: `kafka`, `none`

## Pack as NuGet template

```powershell
dotnet pack C:\dev\study\fiap\dotnet-clean-architecture-template\DotnetCleanArchitecture.TemplatePack.csproj
```
