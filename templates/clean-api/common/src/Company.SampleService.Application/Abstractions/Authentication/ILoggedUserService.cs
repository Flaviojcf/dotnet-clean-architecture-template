namespace Company.SampleService.Application.Abstractions.Authentication;

public interface ILoggedUserService
{
    Guid? GetUserId();
}
