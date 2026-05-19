using Refit;

namespace Company.SampleService.Infrastructure.Http.ExternalQuotes;

public interface IGitHubZenApi
{
    [Get("/zen")]
    Task<string> GetZenAsync(CancellationToken cancellationToken);
}
