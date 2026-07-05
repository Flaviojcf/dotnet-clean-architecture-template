namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;

public sealed record CheckCampaignEligibilityResponse(
    Guid CampaignId,
    string DonorDocument,
    decimal Amount,
    bool Eligible,
    string? Reason);
