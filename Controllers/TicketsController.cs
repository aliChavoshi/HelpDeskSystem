﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;

namespace HelpDeskSystem.Controllers;

public class TicketsController(ApplicationDbContext context) : Controller
{
    // GET: Tickets
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = context.Ticket.Include(t => t.CreatedBy);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Tickets/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await context.Ticket
            .Include(t => t.CreatedBy)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // GET: Tickets/Create
    public IActionResult Create()
    {
        ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Tickets/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Status,Priority,Description,CreatedById,CreatedOn,Version,IsActive,IsDeleted,Id")] Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            context.Add(ticket);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // GET: Tickets/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await context.Ticket.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // POST: Tickets/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Title,Status,Priority,Description,CreatedById,CreatedOn,Version,IsActive,IsDeleted,Id")] Ticket ticket)
    {
        if (id != ticket.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(ticket);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticket.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // GET: Tickets/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await context.Ticket
            .Include(t => t.CreatedBy)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // POST: Tickets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ticket = await context.Ticket.FindAsync(id);
        if (ticket != null)
        {
            context.Ticket.Remove(ticket);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TicketExists(int id)
    {
        return context.Ticket.Any(e => e.Id == id);
    }
}