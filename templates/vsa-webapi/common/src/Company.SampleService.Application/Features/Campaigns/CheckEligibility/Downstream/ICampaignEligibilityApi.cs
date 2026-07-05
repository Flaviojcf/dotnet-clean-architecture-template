using Refit;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;

public interface ICampaignEligibilityApi
{
    [Get("/api/v1/campaigns/{campaignId}/eligibility")]
    Task<CampaignEligibilityApiResponse> CheckEligibilityAsync(
        Guid campaignId,
        [Query] string donorDocument,
        [Query] decimal amount,
        CancellationToken cancellationToken);
}
