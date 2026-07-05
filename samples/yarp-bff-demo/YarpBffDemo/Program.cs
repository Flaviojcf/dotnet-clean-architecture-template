var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.Use(async (context, next) =>
{
    app.Logger.LogInformation(
        "Web API received {Method} {Path}",
        context.Request.Method,
        context.Request.Path);

    await next(context);
});

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    service = "yarp-Web API-demo"
}));

app.MapReverseProxy();

app.Run();
