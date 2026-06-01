using Company.SampleService.Application.DependencyInjection;
#if (useSerilog)
using Serilog;
#endif

var builder = Host.CreateApplicationBuilder(args);

#if (useSerilog)
builder.Services.AddSerilog((serviceProvider, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(serviceProvider)
    .Enrich.FromLogContext()
    .WriteTo.Console());
#endif

builder.Services.AddApplication(builder.Configuration);

var host = builder.Build();

await host.RunAsync();
