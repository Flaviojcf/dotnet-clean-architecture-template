using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Application.Messaging;
#if (useFluentValidation)
using FluentValidation;
#endif
using Microsoft.Extensions.DependencyInjection;
#if (!useMediatR)
using Company.SampleService.Application.UseCases.Items.CreateItem;
using Company.SampleService.Application.UseCases.Items.GetItemById;
#endif
using System.Reflection;

namespace Company.SampleService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
#if (useMediatR)
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
#endif
#if (useFluentValidation)
#if (useMediatR)
        services.AddValidatorsFromAssembly(assembly);
#else
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
#endif
#endif
#if (!useMediatR)
        services.AddScoped<ICreateItemUseCase, CreateItemUseCase>();
        services.AddScoped<IGetItemByIdUseCase, GetItemByIdUseCase>();
#endif
        services.AddScoped<IMessagePublisher, NullMessagePublisher>();

        return services;
    }
}
