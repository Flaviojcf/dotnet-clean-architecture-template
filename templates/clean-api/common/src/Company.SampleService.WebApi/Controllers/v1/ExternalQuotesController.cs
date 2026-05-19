using Company.SampleService.Application.Abstractions.ExternalServices;
using Company.SampleService.WebApi.Models;
#if (useMediatR)
using MediatR;
#endif
using Microsoft.AspNetCore.Mvc;

namespace Company.SampleService.WebApi.Controllers.v1;

public sealed class ExternalQuotesController : BaseApiController
{
#if (useMediatR)
    public ExternalQuotesController(IMediator mediator) : base(mediator)
    {
    }
#endif

    [HttpGet("zen")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetZen(
        [FromServices] IExternalQuoteClient externalQuoteClient,
        CancellationToken cancellationToken)
    {
        var quote = await externalQuoteClient.GetZenAsync(cancellationToken);
        return Ok(ApiResponse<string>.FromSuccess(quote));
    }
}
