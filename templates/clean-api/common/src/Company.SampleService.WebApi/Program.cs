using Company.SampleService.Application.DependencyInjection;
using Company.SampleService.Infrastructure.Http.DependencyInjection;
using Company.SampleService.WebApi.DependencyInjection;
#if (useAuth)
using Company.SampleService.Infrastructure.Auth.DependencyInjection;
#endif
#if (useSqlServer)
using Company.SampleService.Infrastructure.SqlServer.DependencyInjection;
#endif
#if (usePostgreSql)
using Company.SampleService.Infrastructure.PostgreSql.DependencyInjection;
#endif
#if (useMongoDB)
using Company.SampleService.Infrastructure.MongoDb.DependencyInjection;
#endif
#if (useKafka)
using Company.SampleService.Infrastructure.Kafka.DependencyInjection;
#endif

namespace Company.SampleService.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddWebApi(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddHttpInfrastructure(builder.Configuration);
#if (useSqlServer)
        builder.Services.AddSqlServerInfrastructure(builder.Configuration);
#endif
#if (usePostgreSql)
        builder.Services.AddPostgreSqlInfrastructure(builder.Configuration);
#endif
#if (useMongoDB)
        builder.Services.AddMongoDbInfrastructure(builder.Configuration);
#endif
#if (useKafka)
        builder.Services.AddKafkaInfrastructure(builder.Configuration);
#endif
#if (useAuth)
        builder.Services.AddAuthInfrastructure(builder.Configuration);
#endif

        var app = builder.Build();
        app.UseWebApiPipeline();
        app.Run();
    }
}
