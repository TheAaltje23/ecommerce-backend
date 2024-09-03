namespace ecommerce_backend.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(string propertyName, object? propertyValue)
            : base($"{typeof(T).Name} with {propertyName} '{propertyValue}' was not found.") { }
    }
}