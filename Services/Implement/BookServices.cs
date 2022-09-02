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
        public APIResponse AddBook(BookVM book)
        {
            try
            {
                var checkValid = ValidateInputs(book);
                if (!checkValid)
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                //check existed
                var existedBook = BookExists(book.USER_ID, book.BOOK_NAME);
                if (existedBook)
                    return new APIResponse() { Success = false, Message = "Existed" };

                var added = _bookRepository.AddBook(mapper.Map<Book>(book));
                if (added == null)
                    return new APIResponse() { Success = false, Message = "Internal error" };

                return new APIResponse() { Success = true, Message = "Successful", Data = mapper.Map<BookVM>(added) };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse GetBookByName(long uID, string bookName)
        {
            try
            {
                if (uID <= 0 || String.IsNullOrEmpty(bookName))
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                var books = _bookRepository.GetBookByName(uID, bookName);

                if (books != null)
                {
                    var response = new List<BookVM>();
                    foreach (var item in books)
                    {
                        var book = mapper.Map<BookVM>(item);
                        response.Add(book);
                    }
                    return new APIResponse() { Success = true, Message = "Successful", Data = response };
                }
                else
                    return new APIResponse() { Success = false, Message = "No result" };
            }
            catch (Exception ex) {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse GetBooksByUserID(long uID, int? book_status)
        {
            try
            {
                if (uID <= 0)
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                var books = _bookRepository.GetBooksByUserID(uID, book_status);

                if (books != null)
                {
                    var response = new List<BookVM>();
                    foreach (var item in books)
                    {
                        var book = mapper.Map<BookVM>(item);
                        response.Add(book);
                    }
                    return new APIResponse() { Success = true, Message = "Successful", Data = response };
                }
                else
                    return new APIResponse() { Success = false, Message = "No result" };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        private bool ValidateInputs(BookVM book)
        {
            //validate input
            if (book == null || String.IsNullOrEmpty(book.BOOK_NAME))
                return false;

            return true;
        }

        public bool BookExists(long uId, string bookName)
        {
            return _bookRepository.BookExists(uId, bookName);
        }
    }
}

