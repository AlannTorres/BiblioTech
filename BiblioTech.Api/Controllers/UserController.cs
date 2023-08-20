using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserApplication _userApplication;

    public UserController(IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var response = await _userApplication.CreateAsync(request);

        if (response.Report.Any()) {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("{user_id}")]
    public async Task<ActionResult> GetById(int user_id)
    {
        var response = await _userApplication.GetByIdAsync(user_id);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }
}
