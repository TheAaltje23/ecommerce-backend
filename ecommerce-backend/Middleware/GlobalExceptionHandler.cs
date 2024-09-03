using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

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
            _logger.LogInformation(exception, exception.Message);

            var details = new ProblemDetails()
            {
                Detail = $"API error: {exception.Message}",
                Instance = "API",
                Status = (int) HttpStatusCode.InternalServerError,
                Title = "API error",
                Type = "Server error"

            };

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

            return true;
        }
    }
}