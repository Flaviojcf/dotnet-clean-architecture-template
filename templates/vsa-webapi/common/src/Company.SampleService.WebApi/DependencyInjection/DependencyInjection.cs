using Company.SampleService.Application.Abstractions.Auth;
using Company.SampleService.WebApi.Auth;
using Company.SampleService.WebApi.Middlewares;
#if (useOpenTelemetry)
using Company.SampleService.WebApi.Observability;
#endif
#if (useAuth)
using Company.SampleService.WebApi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddScoped<ICurrentBearerTokenAccessor, HttpContextBearerTokenAccessor>();

        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

#if (useSwagger)
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Company.SampleService Web API",
                Version = "v1"
            });
#if (useAuth)
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the JWT bearer token only. The 'Bearer' prefix is added automatically.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecuritySchemeReference(
                        JwtBearerDefaults.AuthenticationScheme,
                        document,
                        null)
                ] = []
            });
#endif
        });
#endif

#if (useAuth)
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
                options.Authority = jwt.Authority;
                options.Audience = jwt.Audience;
                options.RequireHttpsMetadata = jwt.RequireHttpsMetadata;
            });
        services.AddAuthorization();
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
