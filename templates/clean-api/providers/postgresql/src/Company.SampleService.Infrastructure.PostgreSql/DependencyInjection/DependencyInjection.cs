using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Items;
using Company.SampleService.Infrastructure.PostgreSql.Persistence;
using Company.SampleService.Infrastructure.PostgreSql.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SampleService.Infrastructure.PostgreSql.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanApiDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));

        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CleanApiDbContext>());
        services.AddHealthChecks().AddDbContextCheck<CleanApiDbContext>("postgresql");

        return services;
    }
}
