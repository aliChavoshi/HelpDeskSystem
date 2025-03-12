namespace HelpDeskSystem.ViewModels.TicketsDto;

public class TicketHistory : TicketDetailDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}