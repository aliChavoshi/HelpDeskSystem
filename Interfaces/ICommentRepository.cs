using HelpDeskSystem.Entities;

namespace HelpDeskSystem.Interfaces;

public interface ICommentRepository
{
    Task CreateComment(Comment comment);
}