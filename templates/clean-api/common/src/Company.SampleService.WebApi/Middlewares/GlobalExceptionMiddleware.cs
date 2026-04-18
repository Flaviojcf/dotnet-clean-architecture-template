using Company.SampleService.Domain.Exceptions;
using Company.SampleService.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace Company.SampleService.WebApi.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConflictException exception)
        {
            await WriteResponse(context, HttpStatusCode.Conflict, exception.Message);
        }
        catch (NotFoundException exception)
        {
            await WriteResponse(context, HttpStatusCode.NotFound, exception.Message);
        }
        catch (Exception exception)
        {
            await WriteResponse(context, HttpStatusCode.InternalServerError, exception.Message);
        }
    }

    private static Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var payload = JsonSerializer.Serialize(ApiResponse<string>.FromFailure(message));
        return context.Response.WriteAsync(payload);
    }
}
