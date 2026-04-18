using Application.Common.Exceptions;
using Application.Common.Models;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";

        switch (ex)
        {
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = ex.Message;
                break;

            case ValidationException:
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
                break;

            case BusinessRuleException:
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
                break;

            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
                break;

        }

        //var response = new
        //{
        //    success = false,
        //    error = message
        //};
        var response = ApiResponse<string>.Failure(message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}