using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/Loan")]
[Controller]
//[Authorize(Roles = "employee")]
public class LoanController : Controller
{
    private readonly ILoanApplication _loanApplication;

    public LoanController(ILoanApplication loanApplication)
    {
        _loanApplication = loanApplication;
    }

    [HttpPost("CreateLoan")]
    public async Task<ActionResult> CreateLoanAsync([FromBody] CreateLoanRequest loanRequest, [FromQuery] int days)
    {
        var response = await _loanApplication.CreateLoanAsync(loanRequest, days);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpPut("RegisteReturnBook")]
    public async Task<ActionResult> ResgisterReturnAsync([FromQuery] string user_email, [FromQuery] string book_id)
    {
        var response = await _loanApplication.RegisterReturnAsync(user_email, book_id);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

    [HttpGet("ListLoansByFilter")]
    public async Task<ActionResult> ListAllLoanByFilterAsync([FromQuery] string user_name = null)
    {
        var response = await _loanApplication.ListAllLoanByFilterAsync(user_name);

        if (response.Report.Any())
        {
            return UnprocessableEntity(response.Report);
        }

        return Ok(response);
    }

}
