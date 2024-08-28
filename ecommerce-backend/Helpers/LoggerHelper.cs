namespace ecommerce_backend.Helpers
{
    public class LoggingHelper
    {
        private readonly ILogger _logger;

        public LoggingHelper(ILogger logger)
        {
            _logger = logger;
        }

        public void FetchFromDb<T>(string obj, string propertyName, T propertyValue)
        {
            _logger.LogInformation("Fetching {obj} by {propertyName}: {propertyValue}", obj, propertyName, propertyValue);
        }
    }
}