using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Auth;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility;

public static class DependencyInjection
{
    public static IServiceCollection AddCampaignEligibilityFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CampaignApiOptions>(configuration.GetSection(CampaignApiOptions.SectionName));

#if (useAuth)
        services.AddTransient<ForwardUserBearerTokenHandler>();
#endif

        var refitClient = services.AddRefitClient<ICampaignEligibilityApi>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<CampaignApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            });

#if (useAuth)
        refitClient.AddHttpMessageHandler<ForwardUserBearerTokenHandler>();
#endif

        refitClient.AddPolicyHandler((serviceProvider, _) =>
        {
            var retry = serviceProvider.GetRequiredService<IOptions<CampaignApiOptions>>().Value.Retry;
            return CreateRetryPolicy(retry);
        });

        services.AddScoped<ICampaignEligibilityClient, CampaignEligibilityClient>();
        services.AddScoped<ICheckCampaignEligibilityUseCase, CheckCampaignEligibilityUseCase>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(CampaignApiRetryOptions retry)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retry.Attempts,
                attempt => TimeSpan.FromMilliseconds(retry.BaseDelayMilliseconds * attempt));
    }
}
