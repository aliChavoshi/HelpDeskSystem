using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;

namespace HelpDeskSystem.ViewModels.CommentsDto;

public class CommentDto : IMapFrom<Comment>
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Id { get; set; }
    public string Description { get; set; }
    public int TicketId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, CommentDto>()
            .ForMember(x => x.CreatedBy,
                c => c.MapFrom(v => v.CreatedBy.UserName));
    }
}