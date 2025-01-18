using HelpDeskSystem.Entities;
using HelpDeskSystem.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskSystem.ViewModels.TicketsDto;

public class TicketDetailDto : IMapFrom<Ticket>
{
    public int Id { get; set; }
    [Display(Name = "Title")] public string Title { get; set; }
    public TicketStatus Status { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }

    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; } = true;
}