#if (useSwagger)
using Asp.Versioning;
#endif
using Company.SampleService.WebApi.Middlewares;
#if (useOpenTelemetry)
using Company.SampleService.WebApi.Observability;
#endif
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#if (useSwagger)
using Microsoft.OpenApi;
#endif
#if (useOpenTelemetry)
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
#endif
#if (useSerilog)
using Serilog;
#endif
using System.Text.Json.Serialization;

namespace Company.SampleService.WebApi.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

#if (useSwagger)
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Company.SampleService API",
                Version = "v1"
            });
        });
#endif

        services.AddHealthChecks();
        services.AddRouting(options => options.LowercaseUrls = true);

#if (useOpenTelemetry)
        services.AddObservability(configuration);
#endif
#if (useSerilog)
        services.AddSerilogLogging();
#endif

        return services;
    }

    public static WebApplication UseWebApiPipeline(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
#if (useSwagger)
        app.UseSwagger();
        app.UseSwaggerUI();
#endif
#if (useAuth)
        app.UseAuthentication();
        app.UseAuthorization();
#endif
        app.MapControllers();
        app.MapHealthChecks("/health", new HealthCheckOptions());
        return app;
    }

#if (useOpenTelemetry)
    private static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ObservabilityOptions();
        configuration.GetSection(ObservabilityOptions.SectionName).Bind(options);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(options.ServiceName))
            .WithTracing(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddHttpClientInstrumentation();
            })
            .WithMetrics(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddHttpClientInstrumentation();
                builder.AddRuntimeInstrumentation();
            });

        return services;
    }
#endif

#if (useSerilog)
    private static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog();
        });

        return services;
    }
#endif
}
