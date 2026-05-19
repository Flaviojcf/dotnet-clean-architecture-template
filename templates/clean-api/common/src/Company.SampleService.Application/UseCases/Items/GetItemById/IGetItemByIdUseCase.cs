using Company.SampleService.Application.Abstractions.Messaging;

namespace Company.SampleService.Application.UseCases.Items.GetItemById;

public interface IGetItemByIdUseCase : IQueryHandler<GetItemByIdRequest, GetItemByIdResponse>
{
}
