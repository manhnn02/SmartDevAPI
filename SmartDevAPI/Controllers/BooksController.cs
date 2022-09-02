using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Helpers;
using Services.Models;

namespace SmartDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BooksController(IBookServices bookServices)
        {
            this._bookServices = bookServices;
        }

        [HttpGet("GetBooksByUserID")]
        [Authorize]
        public ActionResult GetBooksByUserID(int? bookStatus)
        {
            try
            {
                long user_id = 0;
                string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Int64.TryParse(userID, out user_id))
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(_bookServices.GetBooksByUserID(user_id, bookStatus));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBookByName")]
        [Authorize]
        public ActionResult SearchBookByName(string bookName)
        {
            try
            {
                long user_id = 0;
                string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Int64.TryParse(userID, out user_id))
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(_bookServices.GetBookByName(user_id, bookName));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("AddReadBook")]
        [Authorize]
        public ActionResult AddReadBook(string bookName, string bookDescription, bool status)
        {
            try
            {
                long user_id = 0;
                string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Int64.TryParse(userID, out user_id))
                    return StatusCode(StatusCodes.Status500InternalServerError);

                BookVM book = new BookVM()
                {
                    BOOK_NAME = bookName,
                    BOOK_DES = bookDescription,
                    STATUS = status,
                    USER_ID = user_id
                };
                return Ok(_bookServices.AddBook(book));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

