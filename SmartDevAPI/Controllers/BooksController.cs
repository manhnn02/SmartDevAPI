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
        public ActionResult<APIResponse> GetBooksByUserID(long user_id, int? bookStatus)
        {
            try
            {
                return Ok(_bookServices.GetBooksByUserID(user_id, bookStatus));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBookByName")]
        public ActionResult<APIResponse> SearchBookByName(long user_id, string bookName)
        {
            try
            {
                return Ok(_bookServices.GetBookByName(user_id, bookName));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("AddReadBook")]
        public ActionResult<APIResponse> AddReadBook(string bookName, string bookDescription, bool status, long userID)
        {
            try
            {
                BookVM book = new BookVM()
                {
                    BOOK_NAME = bookName,
                    BOOK_DES = bookDescription,
                    STATUS = status,
                    USER_ID = userID
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

