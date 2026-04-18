namespace Company.SampleService.Application.Abstractions.Authentication;

public interface ITokenProviderService
{
    string Generate(Guid userId, string email, string role);
}
