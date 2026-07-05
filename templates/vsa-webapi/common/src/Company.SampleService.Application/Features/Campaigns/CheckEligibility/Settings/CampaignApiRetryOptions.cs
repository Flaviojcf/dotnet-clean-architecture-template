namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Settings;

public sealed class CampaignApiRetryOptions
{
    public int Attempts { get; init; } = 3;
    public int BaseDelayMilliseconds { get; init; } = 200;
}
