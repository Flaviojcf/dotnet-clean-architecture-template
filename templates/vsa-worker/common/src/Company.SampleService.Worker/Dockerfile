FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Company.SampleService.Worker/Company.SampleService.Worker.csproj", "src/Company.SampleService.Worker/"]
COPY ["src/Company.SampleService.Application/Company.SampleService.Application.csproj", "src/Company.SampleService.Application/"]
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
RUN dotnet restore "src/Company.SampleService.Worker/Company.SampleService.Worker.csproj"
COPY . .
WORKDIR "/src/src/Company.SampleService.Worker"
RUN dotnet build "Company.SampleService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Company.SampleService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.SampleService.Worker.dll"]
