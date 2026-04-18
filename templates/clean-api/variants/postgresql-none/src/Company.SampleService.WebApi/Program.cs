using Company.SampleService.Application.DependencyInjection;
using Company.SampleService.Infrastructure.Auth.DependencyInjection;
using Company.SampleService.Infrastructure.PostgreSql.DependencyInjection;
using Company.SampleService.WebApi.DependencyInjection;

namespace Company.SampleService.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddWebApi(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddPersistenceInfrastructure(builder.Configuration);
        builder.Services.AddAuthInfrastructure(builder.Configuration);

        var app = builder.Build();
        app.UseWebApiPipeline();
        app.Run();
    }
}
