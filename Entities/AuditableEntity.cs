namespace HelpDeskSystem.Entities;

public class AuditableEntity : BaseEntity
{
    public string CreatedById { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int Version { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    #region Relations

    public ApplicationUser CreatedBy { get; set; }

    #endregion
}