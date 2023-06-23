using Microsoft.AspNetCore.Identity;

namespace BazaOsobZaginionych.Models.Domain
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
