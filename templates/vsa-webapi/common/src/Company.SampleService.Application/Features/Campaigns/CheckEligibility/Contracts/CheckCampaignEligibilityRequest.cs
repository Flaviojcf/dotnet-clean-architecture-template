namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;

public sealed record CheckCampaignEligibilityRequest(
    Guid CampaignId,
    string DonorDocument,
    decimal Amount);
