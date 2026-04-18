using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Domain;
using Company.SampleService.Domain.Items;
using Company.SampleService.Messages;

namespace Company.SampleService.Application.UseCases.Items.GetItemById;

public sealed class GetItemByIdUseCase : IQueryHandler<GetItemByIdRequest, GetItemByIdResponse>
{
    private readonly IItemRepository _itemRepository;

    public GetItemByIdUseCase(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

#if (useMediatR)
    public async Task<Result<GetItemByIdResponse>> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
#else
    public async Task<Result<GetItemByIdResponse>> Handle(GetItemByIdRequest query, CancellationToken cancellationToken)
#endif
    {
#if (useMediatR)
        var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
#else
        var item = await _itemRepository.GetByIdAsync(query.Id, cancellationToken);
#endif

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", ResourceMessages.ItemNotFound);
        }

        return new GetItemByIdResponse(item.Id, item.Name, item.Price, item.CreatedAt);
    }
}
