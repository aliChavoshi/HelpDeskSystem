using AutoMapper;
using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskSystem.Controllers;

public class UsersController(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    ApplicationDbContext context,IMapper mapper)
    : AuthorizeBaseController
{
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await context.Users.ToListAsync();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto model)
    {
        var user = mapper.Map<ApplicationUser>(model);
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        if (result.Errors.Any())
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code,error.Description);
                return View(model);
            }
        }
        return View(model);
    }
}