using HelpDeskSystem.Entities;

namespace HelpDeskSystem.Interfaces;

public interface ICommentRepository
{
    Task CreateComment(Comment comment);
    Task<List<Comment>> GetCommentsByTicketId(int ticketId);
}