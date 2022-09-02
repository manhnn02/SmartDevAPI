using Services.Models;

namespace Services;
public interface IBookServices
{
    APIResponse AddBook(BookVM book);
    APIResponse GetBookByName(long uID, string bookName);
    APIResponse GetBooksByUserID(long uID, int? book_status);
}