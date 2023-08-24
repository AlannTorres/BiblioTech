using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers;

[Route("api/BookCheckout")]
[Controller]
[Authorize]
public class BookCheckoutController : Controller
{
    [HttpGet()]
    public IActionResult Get()
    {
        return Ok();
    }
}
