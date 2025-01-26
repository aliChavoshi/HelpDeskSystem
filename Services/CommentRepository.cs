using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskSystem.Services;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    public async Task CreateComment(Comment comment)
    {
        context.Add(comment);
        await context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsByTicketId(int ticketId)
    {
        return await context.Comment
            .Include(x => x.CreatedBy)
            .Where(x => x.TicketId == ticketId)
            .OrderByDescending(x=>x.CreatedOn)
            .ToListAsync();
    }
}