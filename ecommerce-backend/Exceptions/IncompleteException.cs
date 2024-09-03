namespace ecommerce_backend.Exceptions
{
    public class IncompleteException<T> : Exception
    {
        public IncompleteException()
            : base($"{typeof(T).Name} data is incomplete.") { }
    }
}