using System.Linq.Expressions;

namespace ecommerce_backend.Helpers
{
    public class SortHelper
    {
        public static Expression<Func<T, object>> Sort<T>(string sortBy)
        {
            var param = Expression.Parameter(typeof(T), "u");
            var property = Expression.Property(param, sortBy);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), param);
            return lambda;
        }
    }
}