using DevBooking.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevBooking.Api.ExceptionHandler
{ 
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case ValidationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Failed";
                    break;

                case UnauthorizedException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Unauthorized";
                    break;

                case ForbiddenException:
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Title = "Forbidden";
                    break;

                case NotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource Not Found";
                    break;

                case ConflictException:
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Conflict";
                    break;

                case BusinessRuleException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Business Rule Violation";
                    break; ;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Server Error";
                    break;
            }

            problemDetails.Detail = exception.Message;

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
