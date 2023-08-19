using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserApplication userApplication;
    [HttpGet]
    public ActionResult Index()
    {
        return BadRequest("Erro");
    }

    
}
