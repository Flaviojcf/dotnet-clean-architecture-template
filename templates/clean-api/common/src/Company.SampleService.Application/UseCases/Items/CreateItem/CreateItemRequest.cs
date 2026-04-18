using Company.SampleService.Application.Abstractions.Messaging;

namespace Company.SampleService.Application.UseCases.Items.CreateItem;

public sealed record CreateItemRequest(string Name, decimal Price) : ICommand<CreateItemResponse>;
