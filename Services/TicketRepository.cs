using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskSystem.Services;

public class TicketRepository(ApplicationDbContext context) : ITicketRepository
{
    public async Task Create(Ticket ticket)
    {
        context.Add(ticket);
        await Save();
    }

    public async Task Update(Ticket ticket)
    {
        context.Update(ticket);
        await Save();
    }

    public async Task<bool> Delete(Ticket ticket)
    {
        ticket.IsDeleted = true;
        await Update(ticket);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var ticket = await GetById(id);
        return await Delete(ticket);
    }

    public async Task<IReadOnlyList<Ticket>> GetAll()
    {
        return await context.Ticket.Include(x=>x.CreatedBy).ToListAsync();
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Ticket> GetById(int id)
    {
        return await context.Ticket.FindAsync(id);
    }
}