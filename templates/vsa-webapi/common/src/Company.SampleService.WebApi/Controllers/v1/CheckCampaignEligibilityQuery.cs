namespace Company.SampleService.WebApi.Controllers.v1;

public sealed class CheckCampaignEligibilityQuery
{
    public string DonorDocument { get; init; } = string.Empty;
    public decimal Amount { get; init; }
}
