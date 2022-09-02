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
        public ActionResult<List<BookVM>> GetBooksByUserID(long user_id, int? bookStatus)
        {
            try
            {
                List<BookVM> lstBooks = _bookServices.GetBooksByUserID(user_id, bookStatus);
                if (lstBooks.Count > 0 && lstBooks[0].ResponseCode != ResponseCode.Success)
                    return StatusCode((int)lstBooks[0].ResponseCode, lstBooks[0].ResponseCode);

                return lstBooks;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBookByName")]
        public ActionResult<List<BookVM>> SearchBookByName(long user_id, string bookName)
        {
            try
            {
                List<BookVM> lstBooks = _bookServices.GetBookByName(user_id, bookName);
                if (lstBooks.Count > 0 && lstBooks[0].ResponseCode != ResponseCode.Success)
                    return StatusCode((int)lstBooks[0].ResponseCode, lstBooks[0].ResponseCode);

                return lstBooks;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("AddReadBook")]
        public ActionResult<BookVM> AddReadBook(string bookName, string bookDescription, bool status, long userID)
        {
            try
            {
                BookVM book = new BookVM()
                {
                    BookName = bookName,
                    BookDescription = bookDescription,
                    Status = status,
                    UserId = userID
                };
                BookVM resBook = _bookServices.AddBook(book);
                if (resBook.ResponseCode != ResponseCode.Success)
                    return StatusCode((int)resBook.ResponseCode, resBook.ResponseCode);

                return resBook;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

