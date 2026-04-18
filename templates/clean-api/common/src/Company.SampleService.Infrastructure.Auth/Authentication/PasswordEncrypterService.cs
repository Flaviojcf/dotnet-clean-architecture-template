using Company.SampleService.Application.Abstractions.Authentication;

namespace Company.SampleService.Infrastructure.Auth.Authentication;

public sealed class PasswordEncrypterService : IPasswordEncrypterService
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
