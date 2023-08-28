using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Application.Applications
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserApplication _userApplication;

        public AuthController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost("Login")]
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
    }
}
