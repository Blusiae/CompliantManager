using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CompliantManager.Shared.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public bool NotificationsEnabled { get; set; }
        public int? AddressId { get; set; }
        public string? Street { get; set; } = default!;
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-]{3,10}$", ErrorMessage = "Invalid postal code format.")]
        public string? PostalCode { get; set; }

        public string? Country { get; set; }
    }
}
