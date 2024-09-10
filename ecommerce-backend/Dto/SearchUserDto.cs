using ecommerce_backend.Models;

namespace ecommerce_backend.Dto
{
    public class SearchUserDto
    {
        public long? Id { get; init; }
        public string? Username { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public User.Role? UserRole { get; init; }

        // Pagination defaults
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;

        // Sorting defaults
        public string SortBy { get; init; } = "Username";
        public string SortOrder { get; init; } = "asc";
    }
}