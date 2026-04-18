using Company.SampleService.Application.Abstractions.Messaging;

namespace Company.SampleService.Application.UseCases.Items.GetItemById;

public sealed record GetItemByIdRequest(Guid Id) : IQuery<GetItemByIdResponse>;
