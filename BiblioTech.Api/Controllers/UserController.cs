using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/User")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserApplication _userApplication;

    public UserController(IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }

    [HttpPost("RegisterUser")]
    [AllowAnonymous]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
    {
        var response = await _userApplication.CreateAsync(request);

        if (response.Report.Any()) {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPost("LoginUser")]
    [AllowAnonymous]
    public async Task<ActionResult> LoginUser([FromBody] AuthRequest request)
    {
        var response = await _userApplication.AuthAsync(request);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("GetUserById")]
    public async Task<ActionResult> GetById([FromQuery] string user_id)
    {
        var response = await _userApplication.GetByIdAsync(user_id);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("GetBooksUser")]
    public async Task<ActionResult> ListBooksUser([FromQuery] string user_id)
    {
        var response = await _userApplication.ListBooksCheckoutUser(user_id);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    // GET: api/<UserController>
    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("GetUserByFilter")]
    public async Task<ActionResult> GetUsers([FromQuery] string? user_id = null, [FromQuery] string? name = null)
    {
        var response = await _userApplication.ListByFilterAsync(user_id, name);

        if (response.Report.Any())
            return UnprocessableEntity(response.Report);

        return Ok(response);
    }
}
