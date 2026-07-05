namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;

public sealed record CampaignEligibilityApiResponse(
    Guid CampaignId,
    string DonorDocument,
    decimal Amount,
    bool Eligible,
    string? Reason);
