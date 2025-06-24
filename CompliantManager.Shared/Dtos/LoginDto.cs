using System.ComponentModel.DataAnnotations;

namespace CompliantManager.Shared.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "E-mail jest wymagany.")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string? Password { get; set; }
    }
}
