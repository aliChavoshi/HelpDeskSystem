﻿using HelpDeskSystem.Entities;

namespace HelpDeskSystem.Interfaces;

public interface ITicketRepository
{
    Task Create(Ticket ticket);
    Task Update(Ticket ticket);
    Task<bool> Delete(Ticket ticket);
    Task<bool> Delete(int id);
    Task<IReadOnlyList<Ticket>> GetAll();
    Task Save();
    Task<Ticket> GetById(int id);
}