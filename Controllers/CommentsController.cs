﻿using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Extensions;
using HelpDeskSystem.Interfaces;
using HelpDeskSystem.ViewModels.CommentsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HelpDeskSystem.Controllers;

public class CommentsController(ICommentRepository commentRepository, IMapper mapper) : AuthorizeBaseController
{
    [HttpGet("{ticketId}")]
    public async Task<IActionResult> Create(int ticketId)
    {
        var comments = await commentRepository.GetCommentsByTicketId(ticketId);
        return PartialView(new CreateCommentDto
        {
            TicketId = ticketId,
            CommentsOfTicket = mapper.Map<List<CommentDto>>(comments)
        });
    }

    [HttpPost("{ticketId}")]
    public async Task<IActionResult> Create(CreateCommentDto model)
    {
        var entity = mapper.Map<Comment>(model);
        entity.CreatedById = User.GetId();
        await commentRepository.CreateComment(entity);
        return RedirectToAction("Index", "Tickets");
    }
}