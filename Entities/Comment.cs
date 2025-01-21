using Microsoft.AspNetCore.Identity;

namespace HelpDeskSystem.Entities;

public class Comment : AuditableEntity
{
    public string Description { get; set; }
    public int TicketId { get; set; }

    #region Relations

    public Ticket Ticket { get; set; }
    #endregion
}