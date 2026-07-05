namespace Company.SampleService.WebApi.Models;

public sealed record ApiResponse<T>(
    bool Success,
    T? Data,
    string? Error)
{
    public static ApiResponse<T> FromSuccess(T data) => new(true, data, null);

    public static ApiResponse<T> FromFailure(string error) => new(false, default, error);
}
