namespace Company.SampleService.Application.Features.SampleEvent;

public sealed record SampleEvent(
    Guid Id,
    string Recipient,
    string Message,
    DateTimeOffset OccurredAt);
