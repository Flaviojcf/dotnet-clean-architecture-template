using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Infrastructure.Kafka.Messaging;
using Company.SampleService.Infrastructure.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SampleService.Infrastructure.Kafka.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddMessagingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaSettings>(configuration.GetSection(KafkaSettings.SectionName));
        services.AddSingleton<IMessagePublisher, KafkaMessagePublisher>();
        return services;
    }
}
