namespace Company.SampleService.Application.Abstractions.ExternalServices;

public interface IExternalQuoteClient
{
    Task<string> GetZenAsync(CancellationToken cancellationToken);
}
