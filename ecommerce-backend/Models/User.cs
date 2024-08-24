using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce_backend.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(25, ErrorMessage = "Phone number cannot be longer than 25 characters")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public Role UserRole { get; set; } = Role.Guest;

        public enum Role
        {
            Admin,
            User,
            Guest,
        }
    }
}