namespace ecommerce_backend.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
    }

    public class AlreadyExistsException<T> : AlreadyExistsException
    {
        public AlreadyExistsException(string propertyName, object? propertyValue) 
            : base($"{typeof(T).Name} with {propertyName.ToLower()} '{propertyValue}' already exists.") { }
    }
}