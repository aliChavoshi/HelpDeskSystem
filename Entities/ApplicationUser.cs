using Microsoft.AspNetCore.Identity;

namespace HelpDeskSystem.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }

    public string Fullname()
    {
        return FirstName + " " + LastName;
    }

}