using Company.SampleService.Application.Common.Services;
#if (useKafka)
using Company.SampleService.Application.Common.Settings;
using Company.SampleService.Application.Features.SampleEvent;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SampleService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
#if (useKafka)
        services.Configure<KafkaSettings>(configuration.GetSection(KafkaSettings.SectionName));
        services.AddHostedService<SampleEventConsumer>();
#endif
        services.AddSingleton<SampleNotificationService>();

        return services;
    }
}
