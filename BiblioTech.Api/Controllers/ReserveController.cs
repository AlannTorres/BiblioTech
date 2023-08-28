using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/Reserve")]
[Controller]
//[Authorize(Roles = "employee")]
public class ReserveController : Controller
{
    private readonly IReserveApplication _reserveApplication;

    public ReserveController(IReserveApplication reserveApplication)
    {
        _reserveApplication = reserveApplication;
    }

    [HttpPost("CreateReserve")]
    public async Task<ActionResult> CreateReserveAsync([FromBody] CreateReserveRequest reserveRequest)
    {
        var response = await _reserveApplication.CreateReserveAsync(reserveRequest);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPut("CloseReserveBook")]
    public async Task<ActionResult> CloseReserveAsync([FromQuery] string user_email, [FromQuery] string book_id)
    {
        var response = await _reserveApplication.CloseReserveAsync(user_email, book_id);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("ListReservesByFilter")]
    public async Task<ActionResult> ListAllBookReserveByFilterAsync([FromQuery] string book_name = null, [FromQuery] string user_name = null)
    {
        var response = await _reserveApplication.ListAllBookReserveByFilterAsync(book_name, user_name);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

}
