using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Interfaces;

namespace HelpDeskSystem.Services;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    public async Task CreateComment(Comment comment)
    {
        context.Add(comment);
        await context.SaveChangesAsync();
    }
}