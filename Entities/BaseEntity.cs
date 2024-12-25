using System.ComponentModel.DataAnnotations;

namespace HelpDeskSystem.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}