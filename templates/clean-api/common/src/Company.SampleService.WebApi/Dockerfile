FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "./Company.SampleService.slnx"
RUN dotnet publish "./src/Company.SampleService.WebApi/Company.SampleService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Company.SampleService.WebApi.dll"]
