using ecommerce_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dto
{
    public class CreateUserDto
    {
        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "Username cannot be blank.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Username must be between 8 and 50 characters long.")]
        public string? Username { get; init; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long.")]
        public string? Password { get; init; }

        // Optional fields
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string? FirstName { get; init; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string? LastName { get; init; }

        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; init; }

        [StringLength(25, ErrorMessage = "Phone number cannot be longer than 25 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; init; }

        public User.Role UserRole { get; init; } = User.Role.User;
    }
}