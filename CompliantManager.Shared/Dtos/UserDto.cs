using System.ComponentModel.DataAnnotations;

namespace CompliantManager.Shared.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdministrator { get; set; } = false;
    }
}
