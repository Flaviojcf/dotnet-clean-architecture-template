using Company.SampleService.Application.UseCases.Items.CreateItem;
using Company.SampleService.Application.UseCases.Items.GetItemById;
using Company.SampleService.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.SampleService.WebApi.Controllers.v1;

public sealed class ItemsController : BaseApiController
{
    public ItemsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateItemRequest request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id, version = "1" }, ApiResponse<CreateItemResponse>.FromSuccess(response));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetItemByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetItemByIdRequest(id), cancellationToken);
        return Ok(ApiResponse<GetItemByIdResponse>.FromSuccess(response));
    }
}
