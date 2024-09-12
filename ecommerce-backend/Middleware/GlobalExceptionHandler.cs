using ecommerce_backend.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace ecommerce_backend.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            var (statusCode, details) = MapException(exception);

            _logger.LogError(
                exception,
                "Could not process a request on machine {MachineName}. TraceId: {TraceId}.",
                Environment.MachineName, traceId);

            await Results.Problem(
                detail: details,
                statusCode: statusCode,
                title: "Oops! Something went wrong.",
                extensions: new Dictionary<string, object?>
                {
                    {"traceId", traceId}
                }
                ).ExecuteAsync(httpContext);

            return true;
        }

        private static (int StatusCode, string Details) MapException (Exception exception)
        {
            return exception switch
            {
                AlreadyExistsException => (StatusCodes.Status400BadRequest, exception.Message),
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, exception.Message), // Default
            };
        }
    }
}