using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dto
{
    public class CreateUserDto
    {
        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "Username cannot be blank")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        public string Username { get; init; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long")]
        public string Password { get; init; } = string.Empty;
    }
}