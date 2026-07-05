using Company.SampleService.Application.Abstractions.Auth;
using System.Net.Http.Headers;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Auth;

public sealed class ForwardUserBearerTokenHandler : DelegatingHandler
{
    private readonly ICurrentBearerTokenAccessor _tokenAccessor;

    public ForwardUserBearerTokenHandler(ICurrentBearerTokenAccessor tokenAccessor)
    {
        _tokenAccessor = tokenAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _tokenAccessor.GetBearerToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
