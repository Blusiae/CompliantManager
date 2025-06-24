using Microsoft.AspNetCore.Identity;

namespace CompliantManager.Server.Data.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
