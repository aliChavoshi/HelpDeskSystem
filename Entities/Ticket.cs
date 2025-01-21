using System.ComponentModel.DataAnnotations;

namespace HelpDeskSystem.Entities;

public class Ticket : AuditableEntity
{
    [Display(Name = "Title")]
    public string Title { get; set; }
    public TicketStatus Status { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }

    #region Relations

    public List<Comment> Comments { get; set; }


    #endregion
}

public enum TicketStatus
{
    Open,
    Close
}