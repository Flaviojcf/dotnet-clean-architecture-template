using Company.SampleService.Application.Abstractions.Messaging;
using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Exceptions;
using Company.SampleService.Domain.Items;
using Company.SampleService.Messages;

namespace Company.SampleService.Application.UseCases.Items.CreateItem;

public sealed class CreateItemUseCase : ICommandHandler<CreateItemRequest, CreateItemResponse>
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

    public async Task<CreateItemResponse> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        if (await _itemRepository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            throw new ConflictException(ResourceMessages.ItemAlreadyExists);
        }

        var item = Item.Create(request.Name, request.Price);

        await _itemRepository.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _messagePublisher.PublishAsync(
            new ItemCreatedMessage(item.Id, item.Name, item.Price, item.CreatedAt),
            cancellationToken);

        return new CreateItemResponse(item.Id, item.Name, item.Price, item.CreatedAt);
    }
}
