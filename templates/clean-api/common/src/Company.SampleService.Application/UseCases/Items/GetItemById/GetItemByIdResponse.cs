namespace Company.SampleService.Application.UseCases.Items.GetItemById;

public sealed record GetItemByIdResponse(Guid Id, string Name, decimal Price, DateTime CreatedAt);
