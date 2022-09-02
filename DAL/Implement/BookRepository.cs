using System;
using System.Net.NetworkInformation;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implement
{
    public class BookRepository : IBookRepository
    {
        private readonly MyDbContext _context;
        public BookRepository(MyDbContext context)
        {
            this._context = context;
        }

        public Book AddBook(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return book;
        }


        public bool BookExists(long uID, string bookName)
        {
            return (_context.Books?.Any(e => e.UserId == uID && e.BookName == bookName)).GetValueOrDefault();
        }

        public List<Book> GetBookByName(long uID, string bookName)
        {
            if (_context.Books != null)
                return _context.Books.Where(x => x.UserId == uID && x.BookName.Contains(bookName)).ToList();
            else
                return null;
        }

        public List<Book> GetBooksByUserID(long uID, int? book_status)
        {
            if (_context.Users != null)
                return _context.Books.Where(x => x.UserId == uID && (!book_status.HasValue || (book_status.HasValue && x.Status == Convert.ToBoolean(book_status)))).ToList();
            else
                return null;
        }
    }
}