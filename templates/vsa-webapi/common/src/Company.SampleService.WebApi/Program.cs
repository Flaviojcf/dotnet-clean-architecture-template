using Company.SampleService.Application.DependencyInjection;
using Company.SampleService.WebApi.DependencyInjection;

namespace Company.SampleService.WebApi;

public class Program
{
    protected Program() { }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddWebApi(builder.Configuration);

        var app = builder.Build();

        app.UseWebApiPipeline();

        app.Run();
    }
}
