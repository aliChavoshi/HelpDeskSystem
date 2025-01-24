using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;

namespace HelpDeskSystem.ViewModels.CommentsDto;

public class CreateCommentDto : IMapFrom<Comment>
{
    public string TicketId { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentDto, Comment>();
    }
}