using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace HelpDeskSystem.ViewModels.TicketsDto;

public class EditTicketDto : IMapFrom<Ticket>
{
    public int Id { get; set; }
    public int Version { get; set; }
    public bool IsActive { get; set; }
    [Display(Name = "Title")] public string Title { get; set; }
    public TicketStatus Status { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Ticket, EditTicketDto>().ReverseMap()
            .AfterMap((_, ticket) => ticket.Version += 1);
        // .ForMember(x => x.Version,
        // c => c.MapFrom(v => v.Version + 1));
    }
}