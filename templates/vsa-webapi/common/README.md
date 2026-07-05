# Company.SampleService

Web API generated with the **VSA Web API** template.

## Projects

| Project | Description |
|---|---|
| `Application` | Vertical slices, Refit clients, Polly policies, settings, and use cases |
| `Web API` | ASP.NET Core controllers, HTTP DTOs, pipeline, Dockerfile, and host configuration |

## Create a new project

```bash
dotnet new vsa-webapi -n MyCompany.MyWebApi
```

## Template options

| Flag | Default | Description |
|---|---|---|
| `--useSerilog` | `true` | Adds Serilog console/Seq logging |
| `--useOpenTelemetry` | `true` | Adds OpenTelemetry tracing and metrics |
| `--useSwagger` | `true` | Adds Swagger/OpenAPI documentation |
| `--useAuth` | `true` | Adds JWT authentication and forwards the user bearer token to downstream APIs |
| `--useCiCd` | `true` | Adds GitHub Actions wrappers for `fcg-pipelines` |
| `--serviceSlug` | `company-sample-service` | Kebab-case name used by delivery resources |

## Running locally

```bash
dotnet restore
dotnet build
dotnet run --project src/Company.SampleService.WebApi
```

With Docker Compose:

```bash
docker compose up -d
```

## Downstream API settings

Each slice owns its own downstream settings. The sample campaign eligibility slice uses:

```json
"CampaignApi": {
  "BaseUrl": "https://localhost:55903/",
  "TimeoutSeconds": 10,
  "Retry": {
    "Attempts": 3,
    "BaseDelayMilliseconds": 200
  }
}
```

## Project structure

```text
src/
  Company.SampleService.Application/
    Features/
      Campaigns/
        CheckEligibility/
          Auth/
          Contracts/
          Downstream/
          Errors/
          Settings/
          CheckCampaignEligibilityUseCase.cs
          DependencyInjection.cs
  Company.SampleService.WebApi/
    Controllers/
      v1/
```
