using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskSystem.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class AuthorizeBaseController : Controller;