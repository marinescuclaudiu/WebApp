using DatabaseAccess.Models;

namespace Core.Models
{
    public class User
    {
       
        public string FullName { get; set; }

        public string UserName { get; set; }

        public User(ApplicationUser applicationUser)
        {
            FullName = $"{applicationUser.FirstName} {applicationUser.LastName}";
            UserName = applicationUser.UserName;
        }
    }
}
