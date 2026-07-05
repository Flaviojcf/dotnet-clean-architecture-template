using Company.SampleService.Application.Features.Campaigns.CheckEligibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SampleService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCampaignEligibilityFeature(configuration);

        return services;
    }
}
