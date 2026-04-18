using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.CommomTestsUtilities.TestDoubles;
using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Items;
using Company.SampleService.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Company.SampleService.IntegratedTests.Configurations;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public InMemoryItemRepository Repository { get; } = new();
    public FakeUnitOfWork UnitOfWork { get; } = new();
    public FakeMessagePublisher Publisher { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IItemRepository>();
            services.RemoveAll<IUnitOfWork>();
            services.RemoveAll<IMessagePublisher>();

            services.AddSingleton<IItemRepository>(Repository);
            services.AddSingleton<IUnitOfWork>(UnitOfWork);
            services.AddSingleton<IMessagePublisher>(Publisher);
        });
    }
}
