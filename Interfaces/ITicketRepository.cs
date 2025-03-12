using HelpDeskSystem.Entities;
using HelpDeskSystem.ViewModels.TicketsDto;

namespace HelpDeskSystem.Interfaces;

public interface ITicketRepository
{
    Task Create(Ticket ticket);
    Task Update(Ticket ticket);
    Task<bool> Delete(Ticket ticket);
    Task<bool> Delete(int id);
    Task<IReadOnlyList<Ticket>> GetAll();
    Task<List<Ticket>> MyTickets(string currentUserId);
    Task Save();
    Task<Ticket> GetById(int id);
    Task<List<TicketHistory>> GetTemporalHistory(int id);
}