using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Items;
using Company.SampleService.Infrastructure.MongoDb.HealthChecks;
using Company.SampleService.Infrastructure.MongoDb.Persistence;
using Company.SampleService.Infrastructure.MongoDb.Persistence.Repositories;
using Company.SampleService.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Company.SampleService.Infrastructure.MongoDb.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddSingleton<MongoDbContext>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
        services.AddHealthChecks().AddCheck<MongoDbHealthCheck>("mongodb");

        return services;
    }
}
