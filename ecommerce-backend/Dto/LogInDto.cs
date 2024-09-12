using System.ComponentModel.DataAnnotations;

namespace ecommerce_backend.Dto
{
    public class LogInDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public required string Username { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; init; }
    }
}