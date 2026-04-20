using API.Response;
using Application.Common.Exceptions;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            ApiResponse<string> response;

            int statusCode;

            switch(context.Exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    response = ApiResponse<string>.Failure(context.Exception.Message);
                    break;
                case ValidationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    response = ApiResponse<string>.Failure(context.Exception.Message);
                    break;
                 //case ConcurrencyException concurrencyException :
                 //   var payload = new ConcurrencyErrorResponse
                 //   {
                 //        ClientValues = concurrencyException.ClientValues,
                 //       Message = concurrencyException.Message,
                 //       DatabaseValues = concurrencyException.DatabaseValues,
                 //       NewRowVersion = concurrencyException.RowVerson,
                 //   };
                 //   response = ApiResponse<string>.Failure(payload);
                 //   statusCode = StatusCodes.Status409Conflict;
                 //   break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    response = ApiResponse<string>.Failure("An unexpected error occurred.");
                    break;
            }

            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(response)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
