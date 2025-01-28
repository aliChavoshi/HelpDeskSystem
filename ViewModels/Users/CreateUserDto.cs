using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;

namespace HelpDeskSystem.ViewModels.Users;

public class CreateUserDto : IMapFrom<ApplicationUser>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserDto, ApplicationUser>();
    }
}