namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Settings;

public sealed class CampaignApiOptions
{
    public const string SectionName = "CampaignApi";

    public string BaseUrl { get; init; } = "https://localhost:55903/";
    public int TimeoutSeconds { get; init; } = 10;
    public CampaignApiRetryOptions Retry { get; init; } = new();
}
