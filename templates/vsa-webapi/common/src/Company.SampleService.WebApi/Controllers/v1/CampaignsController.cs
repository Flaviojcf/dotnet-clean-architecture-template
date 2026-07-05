using Company.SampleService.Application.Common.Results;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility;
using Company.SampleService.Application.Features.Campaigns.CheckEligibility.Contracts;
using Company.SampleService.WebApi.Models;
#if (useAuth)
using Microsoft.AspNetCore.Authorization;
#endif
using Microsoft.AspNetCore.Mvc;

namespace Company.SampleService.WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/campaigns")]
public sealed class CampaignsController : ControllerBase
{
    private readonly ICheckCampaignEligibilityUseCase _useCase;

    public CampaignsController(ICheckCampaignEligibilityUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet("{campaignId:guid}/eligibility")]
#if (useAuth)
    [Authorize]
#endif
    public async Task<ActionResult<ApiResponse<CheckCampaignEligibilityResponse>>> CheckEligibility(
        Guid campaignId,
        [FromQuery] CheckCampaignEligibilityQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _useCase.ExecuteAsync(
            new CheckCampaignEligibilityRequest(campaignId, query.DonorDocument, query.Amount),
            cancellationToken);

        if (result.IsFailure)
        {
            return ToFailureResponse(result.Error);
        }

        return Ok(ApiResponse<CheckCampaignEligibilityResponse>.FromSuccess(result.Value));
    }

    private ActionResult<ApiResponse<CheckCampaignEligibilityResponse>> ToFailureResponse(Error error)
    {
        var response = ApiResponse<CheckCampaignEligibilityResponse>.FromFailure(error.Message);

        return error.Type switch
        {
            ErrorType.Validation => BadRequest(response),
            ErrorType.NotFound => NotFound(response),
            ErrorType.Conflict => Conflict(response),
            _ => StatusCode(StatusCodes.Status502BadGateway, response)
        };
    }
}
