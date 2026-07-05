using Company.SampleService.Application.Common.Results;
using System.Net;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Errors;

public static class CampaignEligibilityErrors
{
    public static Error DownstreamUnsuccessful(HttpStatusCode statusCode) =>
        Error.Failure(
            "CampaignEligibility.DownstreamUnsuccessful",
            $"Campaign API returned an unsuccessful response: {(int)statusCode}.");

    public static Error Timeout() =>
        Error.Failure(
            "CampaignEligibility.Timeout",
            "Campaign API request timed out.");

    public static Error Canceled() =>
        Error.Failure(
            "CampaignEligibility.Canceled",
            "Campaign eligibility request was canceled.");

    public static Error RequestFailed() =>
        Error.Failure(
            "CampaignEligibility.RequestFailed",
            "Campaign API request failed.");

    public static Error Unexpected() =>
        Error.Failure(
            "CampaignEligibility.Unexpected",
            "Unexpected error while calling Campaign API.");
}
