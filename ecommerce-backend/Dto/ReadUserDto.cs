using ecommerce_backend.Models;

namespace ecommerce_backend.Dto
{
    public class ReadUserDto
    {
        public long Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public User.Role UserRole { get; init; } = User.Role.User;
    }
}