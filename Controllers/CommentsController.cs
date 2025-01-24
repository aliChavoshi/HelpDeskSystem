using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskSystem.Controllers;

public class CommentsController : AuthorizeBaseController
{
    [HttpGet("{ticketId}")] 
    public IActionResult Create(string ticketId)
    {
        return PartialView();
    }
}