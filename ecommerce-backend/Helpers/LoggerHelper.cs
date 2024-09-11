namespace ecommerce_backend.Helpers
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
            _logger.LogInformation($"Receiving HTTP request for {typeof(T).Name}: {methodName}.");
        }

        public void ReturnHttpResponse<T>(string methodName)
        {
            _logger.LogInformation($"Returning HTTP response for {typeof(T).Name}: {methodName}.");
        }

        // Service loggers
        public void ReadDb<T>(string propertyName, object? propertyValue)
        {
            _logger.LogInformation($"Fetching {typeof(T).Name} by {propertyName.ToLower()}: {propertyValue} from the database.");
        }

        public void SearchDb<T>(int amount)
        {
            _logger.LogInformation($"Fetching {amount} {typeof(T).Name}s from the database.");
        }

        public void CreateDb<T>(string propertyName, object? propertyValue)
        {
            _logger.LogInformation($"Creating {typeof(T).Name} with {propertyName.ToLower()}: {propertyValue} and saving it to the database.");
        }

        public void UpdateDb<T>(string propertyName, object? propertyValue)
        {
            _logger.LogInformation($"Updating {typeof(T).Name} with {propertyName.ToLower()}: {propertyValue} and saving it to the database.");
        }

        public void DeleteDb<T>(string propertyName, object? propertyValue)
        {
            _logger.LogInformation($"Deleting {typeof(T).Name} with {propertyName.ToLower()}: {propertyValue} from the database.");
        }
    }
}