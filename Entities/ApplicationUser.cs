using Microsoft.AspNetCore.Identity;

namespace HelpDeskSystem.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }

    #region Relations

    public List<Ticket> Tickets { get; set; }

    #endregion

    public string Fullname()
    {
        return FirstName + " " + LastName;
    }

}

public enum Gender
{
    Male,
    Female
}