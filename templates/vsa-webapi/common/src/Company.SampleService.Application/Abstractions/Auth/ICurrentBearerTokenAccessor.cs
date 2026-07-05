namespace Company.SampleService.Application.Abstractions.Auth;

public interface ICurrentBearerTokenAccessor
{
    string? GetBearerToken();
}
