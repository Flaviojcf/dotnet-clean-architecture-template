using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Application.UseCases.Items.CreateItem;
using Company.SampleService.Application.UseCases.Items.GetItemById;
using Company.SampleService.WebApi.Extensions;
using Company.SampleService.WebApi.Models;
#if (useMediatR)
using MediatR;
#endif
using Microsoft.AspNetCore.Mvc;

namespace Company.SampleService.WebApi.Controllers.v1;

public sealed class ItemsController : BaseApiController
{
#if (useMediatR)
    public ItemsController(IMediator mediator) : base(mediator)
    {
    }
#else
    private readonly ICommandHandler<CreateItemRequest, CreateItemResponse> _createItem;
    private readonly IQueryHandler<GetItemByIdRequest, GetItemByIdResponse> _getItemById;

    public ItemsController(
        ICommandHandler<CreateItemRequest, CreateItemResponse> createItem,
        IQueryHandler<GetItemByIdRequest, GetItemByIdResponse> getItemById)
    {
        _createItem = createItem;
        _getItemById = getItemById;
    }
#endif

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateItemRequest request, CancellationToken cancellationToken)
    {
#if (useMediatR)
        var result = await Mediator.Send(request, cancellationToken);
#else
        var result = await _createItem.Handle(request, cancellationToken);
#endif

        if (result.IsFailure)
        {
            return result.Error.ToActionResult();
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id, version = "1" },
            ApiResponse<CreateItemResponse>.FromSuccess(result.Value));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetItemByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
#if (useMediatR)
        var result = await Mediator.Send(new GetItemByIdRequest(id), cancellationToken);
#else
        var result = await _getItemById.Handle(new GetItemByIdRequest(id), cancellationToken);
#endif

        if (result.IsFailure)
        {
            return result.Error.ToActionResult();
        }

        return Ok(ApiResponse<GetItemByIdResponse>.FromSuccess(result.Value));
    }
}
