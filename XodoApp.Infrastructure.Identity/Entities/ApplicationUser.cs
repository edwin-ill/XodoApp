using Microsoft.AspNetCore.Identity;


namespace XodoApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? img { get; set; }
    }
}
