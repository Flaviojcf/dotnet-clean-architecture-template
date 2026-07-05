# YARP Web API Demo

Small ASP.NET Core Web API prototype using YARP as a reverse proxy.

This sample is intentionally minimal. It is not part of the template pack and is only meant to make the YARP flow easy to inspect before deciding whether a Web API template should use it.

## Run

```powershell
dotnet restore .\YarpWeb APIDemo.slnx
dotnet build .\YarpWeb APIDemo.slnx
dotnet run --project .\YarpWeb APIDemo\YarpWeb APIDemo.csproj --urls http://127.0.0.1:5195
```

## Try It

Health endpoint handled by the Web API itself:

```powershell
curl.exe http://127.0.0.1:5195/health
```

Proxied request handled by YARP:

```powershell
curl.exe http://127.0.0.1:5195/todos/todos/1
```

The local request matches `/todos/{**catch-all}`. The sample removes the `/todos` prefix and forwards the request to:

```text
https://jsonplaceholder.typicode.com/todos/1
```

## YARP Concepts In This Sample

- Route: decides which incoming requests the Web API should proxy. Here, `/todos/{**catch-all}`.
- Cluster: logical group of downstream destinations. Here, `jsonplaceholder`.
- Destination: actual upstream address. Here, `https://jsonplaceholder.typicode.com/`.
- Transform: adjusts the request before forwarding. Here, `PathRemovePrefix` removes `/todos`.

## Limits

- No authentication.
- No authorization.
- No OpenTelemetry.
- No Serilog.
- No aggregation logic.
- No Clean Architecture structure.

This is a proxy-first Web API sample. If the Web API needs to compose responses from multiple services or reshape contracts for the frontend, typed `HttpClient`/feature endpoints may be a better default than YARP.
