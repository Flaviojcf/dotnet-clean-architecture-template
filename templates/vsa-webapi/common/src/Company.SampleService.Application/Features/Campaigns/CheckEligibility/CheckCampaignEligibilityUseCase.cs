using Company.SampleService.Application.Common.Results;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;
using Microsoft.Extensions.Logging;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility;

public sealed class CheckCampaignEligibilityUseCase : ICheckCampaignEligibilityUseCase
{
    private readonly ICampaignEligibilityClient _client;
    private readonly ILogger<CheckCampaignEligibilityUseCase> _logger;

    public CheckCampaignEligibilityUseCase(
        ICampaignEligibilityClient client,
        ILogger<CheckCampaignEligibilityUseCase> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<Result<CheckCampaignEligibilityResponse>> ExecuteAsync(
        CheckCampaignEligibilityRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Checking campaign {CampaignId} eligibility for donor {DonorDocument}",
            request.CampaignId,
            request.DonorDocument);

        return await _client.CheckAsync(request, cancellationToken);
    }
}
