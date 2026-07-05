namespace Company.SampleService.WebApi.Observability;

public sealed class ObservabilityOptions
{
    public const string SectionName = "Observability";

    public string ServiceName { get; init; } = "Company.SampleService.WebApi";
}
