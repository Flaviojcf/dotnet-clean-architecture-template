using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Domain;
using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Items;
using Company.SampleService.Messages;

namespace Company.SampleService.Application.UseCases.Items.CreateItem;

public sealed class CreateItemUseCase : ICreateItemUseCase
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessagePublisher _messagePublisher;

    public CreateItemUseCase(IItemRepository itemRepository, IUnitOfWork unitOfWork, IMessagePublisher messagePublisher)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _messagePublisher = messagePublisher;
    }

#if (useMediatR)
    public async Task<Result<CreateItemResponse>> Handle(CreateItemRequest request, CancellationToken cancellationToken)
#else
    public async Task<Result<CreateItemResponse>> Handle(CreateItemRequest command, CancellationToken cancellationToken)
#endif
    {
#if (useMediatR)
        if (await _itemRepository.ExistsByNameAsync(request.Name, cancellationToken))
#else
        if (await _itemRepository.ExistsByNameAsync(command.Name, cancellationToken))
#endif
        {
            return Error.Conflict("Item.AlreadyExists", ResourceMessages.ItemAlreadyExists);
        }

#if (useMediatR)
        var itemResult = Item.Create(request.Name, request.Price);
#else
        var itemResult = Item.Create(command.Name, command.Price);
#endif

        if (itemResult.IsFailure)
        {
            return itemResult.Error;
        }

        var item = itemResult.Value;

        await _itemRepository.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _messagePublisher.PublishAsync(
            new ItemCreatedMessage(item.Id, item.Name, item.Price, item.CreatedAt),
            cancellationToken);

        return new CreateItemResponse(item.Id, item.Name, item.Price, item.CreatedAt);
    }
}
