using System;
using AutoMapper;
using DAL;
using DAL.Implement;
using DAL.Models;
using Services.Helpers;
using Services.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Services.Implement
{
    public class BookServices:IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper mapper;

        public BookServices(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            this.mapper = mapper;
        }
        public BookVM AddBook(BookVM book)
        {
            try
            {
                var checkValid = ValidateInputs(book);
                if (!checkValid)
                    return new BookVM() { ResponseCode = ResponseCode.BadRequest };

                //check existed
                var existedBook = BookExists(book.UserId, book.BookName);
                if (existedBook)
                    return new BookVM() { ResponseCode = ResponseCode.BadRequest };

                var added = _bookRepository.AddBook(book);
                if (added == null)
                    return new BookVM() { ResponseCode = ResponseCode.Error };

                BookVM response = mapper.Map<BookVM>(added);
                response.ResponseCode = ResponseCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                return new BookVM() { ResponseCode = ResponseCode.Error };
            }
        }

        public List<BookVM> GetBookByName(long uID, string bookName)
        {
            try
            {
                if (uID <= 0 || String.IsNullOrEmpty(bookName))
                    return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.BadRequest } };

                var books = _bookRepository.GetBookByName(uID, bookName);

                if (books != null)
                {
                    var response = new List<BookVM>();
                    foreach (var item in books)
                    {
                        var book = mapper.Map<BookVM>(item);
                        book.ResponseCode = ResponseCode.Success;
                        response.Add(book);
                    }
                    return response;
                }
                else
                    return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.BadRequest } };
            }
            catch (Exception ex) {
                return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.Error } };
            }
        }

        public List<BookVM> GetBooksByUserID(long uID, int? book_status)
        {
            try
            {
                if (uID <= 0)
                    return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.BadRequest } };

                var books = _bookRepository.GetBooksByUserID(uID, book_status);

                if (books != null)
                {
                    var response = new List<BookVM>();
                    foreach (var item in books)
                    {
                        var book = mapper.Map<BookVM>(item);
                        book.ResponseCode = ResponseCode.Success;
                        response.Add(book);
                    }

                    return response;
                }
                else
                    return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.BadRequest } };
            }
            catch (Exception ex)
            {
                return new List<BookVM>() { new BookVM() { ResponseCode = ResponseCode.Error } };
            }
        }

        private bool ValidateInputs(Book book)
        {
            //validate input
            if (book == null || String.IsNullOrEmpty(book.BookName))
                return false;

            return true;
        }

        public bool BookExists(long uId, string bookName)
        {
            return _bookRepository.BookExists(uId, bookName);
        }
    }
}

