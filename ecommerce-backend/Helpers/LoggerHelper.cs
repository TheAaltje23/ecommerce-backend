﻿namespace ecommerce_backend.Helpers
{
    public class LoggingHelper
    {
        private readonly ILogger _logger;

        public LoggingHelper(ILogger logger)
        {
            _logger = logger;
        }

        // Controller loggers
        public void ReceiveHttpRequest<T>(string methodName)
        {
            _logger.LogInformation($"Receiving HTTP request for {typeof(T).Name}: {methodName}");
        }

        public void ReturnHttpResponse<T>(string methodName)
        {
            _logger.LogInformation($"Returning HTTP response for {typeof(T).Name}: {methodName}");
        }

        // Service loggers
        public void FetchDb<T>(string propertyName, object propertyValue)
        {
            _logger.LogInformation($"Fetching {typeof(T).Name} by {propertyName}: {propertyValue} from the database");
        }

        public void NotFoundDb<T>(string propertyName, object propertyValue)
        {
            _logger.LogWarning($"{typeof(T).Name} with {propertyName}: {propertyValue} was not found");
        }
    }
}