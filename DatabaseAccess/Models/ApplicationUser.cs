using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
