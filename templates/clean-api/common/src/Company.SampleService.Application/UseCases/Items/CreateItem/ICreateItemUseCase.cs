using Company.SampleService.Application.Abstractions.Messaging;

namespace Company.SampleService.Application.UseCases.Items.CreateItem;

public interface ICreateItemUseCase : ICommandHandler<CreateItemRequest, CreateItemResponse>
{
}
