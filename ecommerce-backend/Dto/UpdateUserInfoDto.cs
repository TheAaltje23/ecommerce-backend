using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dto
{
    public class UpdateUserInfoDto
    {
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
    }
}