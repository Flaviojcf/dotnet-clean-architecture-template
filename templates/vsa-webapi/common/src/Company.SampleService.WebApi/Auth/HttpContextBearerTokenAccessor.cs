using Company.SampleService.Application.Abstractions.Auth;
using Microsoft.Extensions.Primitives;

namespace Company.SampleService.WebApi.Auth;

public sealed class HttpContextBearerTokenAccessor : ICurrentBearerTokenAccessor
{
    private const string AuthorizationHeaderName = "Authorization";
    private const string BearerPrefix = "Bearer ";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextBearerTokenAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetBearerToken()
    {
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;

        if (headers is null ||
            !headers.TryGetValue(AuthorizationHeaderName, out var authorization) ||
            StringValues.IsNullOrEmpty(authorization))
        {
            return null;
        }

        var value = authorization.ToString();

        return value.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase)
            ? value[BearerPrefix.Length..].Trim()
            : null;
    }
}
