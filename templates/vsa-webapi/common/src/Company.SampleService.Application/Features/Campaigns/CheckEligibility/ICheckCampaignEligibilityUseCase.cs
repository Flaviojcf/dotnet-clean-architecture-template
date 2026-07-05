using Company.SampleService.Application.Common.Results;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility;

public interface ICheckCampaignEligibilityUseCase
{
    Task<Result<CheckCampaignEligibilityResponse>> ExecuteAsync(
        CheckCampaignEligibilityRequest request,
        CancellationToken cancellationToken);
}
