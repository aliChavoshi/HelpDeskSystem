using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;

namespace HelpDeskSystem.ViewModels.TicketsDto;

public class CreateTicketDto : IMapFrom<Ticket>
{
    public string Title { get; set; }
    public TicketStatus Status { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTicketDto, Ticket>();
    }
}