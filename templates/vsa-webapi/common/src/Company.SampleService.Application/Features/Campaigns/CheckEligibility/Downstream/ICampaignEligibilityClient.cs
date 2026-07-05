using Company.SampleService.Application.Common.Results;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;

public interface ICampaignEligibilityClient
{
    Task<Result<CheckCampaignEligibilityResponse>> CheckAsync(
        CheckCampaignEligibilityRequest request,
        CancellationToken cancellationToken);
}
