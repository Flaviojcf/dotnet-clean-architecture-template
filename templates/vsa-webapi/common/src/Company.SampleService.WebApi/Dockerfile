FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Company.SampleService.Application/Company.SampleService.Application.csproj", "src/Company.SampleService.Application/"]
COPY ["src/Company.SampleService.WebApi/Company.SampleService.WebApi.csproj", "src/Company.SampleService.WebApi/"]
RUN dotnet restore "src/Company.SampleService.WebApi/Company.SampleService.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Company.SampleService.WebApi"
RUN dotnet publish "Company.SampleService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Company.SampleService.WebApi.dll"]
