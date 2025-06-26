using Microsoft.AspNetCore.Identity;

namespace CompliantManager.Server.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Claim>? Claims { get; set; } = [];
    }
}
