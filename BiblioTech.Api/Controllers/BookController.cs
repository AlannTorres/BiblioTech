using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Api.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookApplication _bookApplication;

        public BookController(IBookApplication bookApplication)
        {
            _bookApplication = bookApplication;
        }

        [HttpPost("CreateBook")]
        //[Authorize(Roles = "employee")]
        public async Task<ActionResult> CreateBookAsync(CreateBookRequest book)
        {
            var response = await _bookApplication.CreateBookAsync(book);

            if (response.Report.Any())
            {
                return UnprocessableEntity(response.Report);
            }

            return Ok(response);
        }

        [HttpPut("UpdateBook")]
        //[Authorize(Roles = "employee")]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookRequest book, string book_id)
        {
            var response = await _bookApplication.UpdateBookAsync(book, book_id);

            if (response.Report.Any())
            {
                return UnprocessableEntity(response.Report);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteBook")]
        //[Authorize(Roles = "employee")]
        public async Task<ActionResult> DeleteBookAsync(string book_id)
        {
            var response = await _bookApplication.DeleteBookAsync(book_id);

            if (response.Report.Any())
            {
                return UnprocessableEntity(response.Report);
            }

            return Ok(response);
        }

        [HttpGet("GetBookByFilter")]
        //[Authorize(Roles = "client, employee")]
        public async Task<ActionResult> ListAllBooksByFilterAsync(string? book_name = null)
        {
            var response = await _bookApplication.ListAllBooksByFilterAsync(book_name);

            if (response.Report.Any())
            {
                return UnprocessableEntity(response.Report);
            }

            return Ok(response);
        }

    }
}
