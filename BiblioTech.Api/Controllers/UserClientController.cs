using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/UserClient")]
[ApiController]
public class UserClientController : Controller
{
    private readonly IUserApplication _userClientApplication;

    public UserClientController(IUserApplication userApplication)
    {
        _userClientApplication = userApplication;
    }

    [HttpGet("GetUserByFilter")]
    //[Authorize(Roles = "client")]
    public async Task<ActionResult> ListClientsByFilter([FromQuery] string? user_email = null, [FromQuery] string? name = null)
    {
        var response = await _userClientApplication.ListClientsByFilterAsync(user_email, name);

        if (response.Report.Any())
            return UnprocessableEntity(response.Report);

        return Ok(response);
    }

    [HttpPost("RegisterUserClient")]
    //[AllowAnonymous]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
    {
        var response = await _userClientApplication.CreateUserClientAsync(request);

        if (response.Report.Any()) {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPut("UpdateUserClient")]
    //[Authorize(Roles = "client")]
    public async Task<ActionResult> UpdateUserAsync([FromBody] CreateUserRequest user, [FromQuery] string user_email)
    {
        var response = await _userClientApplication.UpdateUserAsync(user, user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpDelete("DeleteUserClient")]
    //[Authorize(Roles = "client")]
    public async Task<ActionResult> DeleteUserAsync([FromQuery] string user_email)
    {
        var response = await _userClientApplication.DeleteUserAsync(user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("GetBooksUserClient")]
    //[Authorize(Roles = "client")]
    public async Task<ActionResult> ListAllBooksClientsAsync([FromQuery] string user_email)
    {
        var response = await _userClientApplication.ListAllBooksClientAsync(user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("GetReserveUserClient")]
    //[Authorize(Roles = "client")]
    public async Task<ActionResult> ListAllReserveClientsAsync([FromQuery] string user_email)
    {
        var response = await _userClientApplication.ListAllReserveClientAsync(user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

}
