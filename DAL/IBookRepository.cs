using System;
using DAL.Models;

namespace DAL
{
    public interface IBookRepository
    {
        List<Book> GetBookByName(long uID, string bookName);
        Book AddBook(Book book);
        bool BookExists(long uID, string bookName);
        List<Book> GetBooksByUserID(long uID, int? book_status);
    }
}

