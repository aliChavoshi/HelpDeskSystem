using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Extensions;
using HelpDeskSystem.Interfaces;
using HelpDeskSystem.ViewModels.TicketsDto;
using Microsoft.AspNetCore.Authorization;

namespace HelpDeskSystem.Controllers;

[Authorize]
public class TicketsController(IMapper mapper, ITicketRepository ticketRepository)
    : Controller
{
    // GET: Tickets
    public async Task<IActionResult> Index()
    {
        return View(await ticketRepository.GetAll());
    }

    // GET: Tickets/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await ticketRepository.GetById(id.Value);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // GET: Tickets/Create
    public IActionResult Create()
    {
        // ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Tickets/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTicketDto ticket)
    {
        if (ModelState.IsValid)
        {
            var entity = mapper.Map<Ticket>(ticket); //new
            entity.CreatedById = User.GetId();
            await ticketRepository.Create(entity);
            return RedirectToAction(nameof(Index));
        }

        // ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // GET: Tickets/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await ticketRepository.GetById(id.Value);
        if (ticket == null)
        {
            return NotFound();
        }

        // ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // POST: Tickets/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Title,Status,Priority,Description,CreatedById,CreatedOn,Version,IsActive,IsDeleted,Id")] Ticket ticket)
    {
        if (id != ticket.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await ticketRepository.Update(ticket);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!TicketExists(ticket.Id))
                // {
                //     return NotFound();
                // }
                // else
                // {
                //     throw;
                // }
            }

            return RedirectToAction(nameof(Index));
        }

        // ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
        return View(ticket);
    }

    // GET: Tickets/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await ticketRepository.GetById(id.Value);
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
        var ticket = await ticketRepository.GetById(id);
        if (ticket != null)
        {
            await ticketRepository.Delete(ticket);
        }

        return RedirectToAction(nameof(Index));
    }
}