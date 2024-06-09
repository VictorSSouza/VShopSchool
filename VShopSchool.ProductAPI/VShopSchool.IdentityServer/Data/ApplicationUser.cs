using Microsoft.AspNetCore.Identity;

namespace VShopSchool.IdentityServer.Data
{
    public class ApplicationUser : IdentityUser
    {
	    public string FirstName { get; set; } = String.Empty;
	    public string LastName { get; set; } = String.Empty; 
    }
}
