using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Username must be between 8 and 50 characters long.")]
        public required string Username { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long.")]
        public required string Password { get; init; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmationPassword { get; init; }
    }
}