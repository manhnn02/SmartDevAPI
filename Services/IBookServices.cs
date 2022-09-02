using Services.Models;

namespace Services;
public interface IBookServices
{
    BookVM AddBook(BookVM book);
    List<BookVM> GetBookByName(long uID, string bookName);
    List<BookVM> GetBooksByUserID(long uID, int? book_status);
}