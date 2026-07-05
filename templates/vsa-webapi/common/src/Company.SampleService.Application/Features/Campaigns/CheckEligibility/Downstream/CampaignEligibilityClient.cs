using Company.SampleService.Application.Common.Results;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Errors;
using Microsoft.Extensions.Logging;
using Polly.Timeout;
using Refit;

namespace Company.SampleService.Application.Features.Campaigns.CheckEligibility.Downstream;

public sealed class CampaignEligibilityClient : ICampaignEligibilityClient
{
    private readonly ICampaignEligibilityApi _api;
    private readonly ILogger<CampaignEligibilityClient> _logger;

    public CampaignEligibilityClient(
        ICampaignEligibilityApi api,
        ILogger<CampaignEligibilityClient> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task<Result<CheckCampaignEligibilityResponse>> CheckAsync(
        CheckCampaignEligibilityRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _api.CheckEligibilityAsync(
                request.CampaignId,
                request.DonorDocument,
                request.Amount,
                cancellationToken);

            return new CheckCampaignEligibilityResponse(
                response.CampaignId,
                response.DonorDocument,
                response.Amount,
                response.Eligible,
                response.Reason);
        }
        catch (ApiException exception)
        {
            _logger.LogWarning(
                exception,
                "Campaign API returned {StatusCode} while checking campaign {CampaignId}",
                exception.StatusCode,
                request.CampaignId);

            return CampaignEligibilityErrors.DownstreamUnsuccessful(exception.StatusCode);
        }
        catch (TimeoutRejectedException exception)
        {
            _logger.LogWarning(
                exception,
                "Campaign API timed out while checking campaign {CampaignId}",
                request.CampaignId);

            return CampaignEligibilityErrors.Timeout();
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning(
                exception,
                "Campaign API HTTP client timeout elapsed while checking campaign {CampaignId}",
                request.CampaignId);

            return CampaignEligibilityErrors.Timeout();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Campaign eligibility request for campaign {CampaignId} was canceled by the caller",
                request.CampaignId);

            return CampaignEligibilityErrors.Canceled();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogWarning(
                exception,
                "Campaign API request failed while checking campaign {CampaignId}",
                request.CampaignId);

            return CampaignEligibilityErrors.RequestFailed();
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Unexpected error while checking campaign {CampaignId}",
                request.CampaignId);

            return CampaignEligibilityErrors.Unexpected();
        }
    }
}
