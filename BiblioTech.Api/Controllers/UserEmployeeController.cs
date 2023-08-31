using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/UserEmployee")]
[ApiController]
public class UserEmployeeController : Controller
{
    private readonly IUserApplication _userEmployeeApplication;

    public UserEmployeeController(IUserApplication userApplication)
    {
        _userEmployeeApplication = userApplication;
    }

    [HttpGet("ListEmployeeByFilter")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> ListEmployeesByFilterAsync([FromQuery] string? employee_email = null, [FromQuery] string? name = null)
    {
        var response = await _userEmployeeApplication.ListEmployeesByFilterAsync(employee_email, name);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPost("RegisterUserEmployee")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> RegisterEmployee([FromBody] CreateUserRequest request)
    {
        var response = await _userEmployeeApplication.CreateUserEmployeeAsync(request);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPut("UpdateUserEmployee")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> UpdateUserAsync([FromBody] CreateUserRequest user, [FromQuery] string user_email)
    {
        var response = await _userEmployeeApplication.UpdateUserAsync(user, user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpDelete("DeleteUserEmployee")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> DeleteUserAsync([FromQuery] string user_email)
    {
        var response = await _userEmployeeApplication.DeleteUserAsync(user_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("ListLoanEmployee")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> ListAllLoanEmployeeAsync([FromQuery] string employee_email)
    {
        var response = await _userEmployeeApplication.ListAllLoanEmployeeAsync(employee_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("ListReserveEmployee")]
    //[Authorize(Roles = "employee")]
    public async Task<ActionResult> ListAllReserveEmployeeAsync([FromQuery] string employee_email)
    {
        var response = await _userEmployeeApplication.ListAllReserveEmployeeAsync(employee_email);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }
}
