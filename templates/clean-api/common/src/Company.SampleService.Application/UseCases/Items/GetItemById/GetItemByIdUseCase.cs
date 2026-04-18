using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Domain.Exceptions;
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

    public async Task<GetItemByIdResponse> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (item is null)
        {
            throw new NotFoundException(ResourceMessages.ItemNotFound);
        }

        return new GetItemByIdResponse(item.Id, item.Name, item.Price, item.CreatedAt);
    }
}
