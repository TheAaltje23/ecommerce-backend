namespace ecommerce_backend.Exceptions
{
    public class AlreadyExistsException<T> : Exception
    {
        public AlreadyExistsException(string propertyName, object? propertyValue) 
            : base($"{typeof(T).Name} with {propertyName} '{propertyValue}' already exists.") { }
    }
}
