namespace ecommerce_backend.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string propertyName, object? propertyValue)
    : base($"{typeof(T).Name} with {propertyName.ToLower()} '{propertyValue}' was not found.") { }
    }
}