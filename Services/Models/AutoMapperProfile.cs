using System;
using AutoMapper;
using DAL.Models;

namespace Services.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>()
                .ForMember(
                    dest => dest.USER_ID,
                    opt => opt.MapFrom(src => src.UserId)
                )
                .ForMember(
                    dest => dest.USER_NAME,
                    opt => opt.MapFrom(src => src.UserName)
                )
                .ForMember(
                    dest => dest.USER_PASS,
                    opt => opt.MapFrom(src => src.UserPass)
                )
                .ForMember(
                    dest => dest.USER_EMAIL,
                    opt => opt.MapFrom(src => src.UserEmail)
                )
                .ForMember(
                    dest => dest.BOOKS,
                    opt => opt.MapFrom(src => src.Books)
                );
            CreateMap<Book, BookVM>()
                .ForMember(
                    dest => dest.BOOK_ID,
                    opt => opt.MapFrom(src => src.BookId)
                )
                .ForMember(
                    dest => dest.BOOK_NAME,
                    opt => opt.MapFrom(src => src.BookName)
                )
                .ForMember(
                    dest => dest.BOOK_DES,
                    opt => opt.MapFrom(src => src.BookDescription)
                )
                .ForMember(
                    dest => dest.USER_ID,
                    opt => opt.MapFrom(src => src.UserId)
                )
                .ForMember(
                    dest => dest.STATUS,
                    opt => opt.MapFrom(src => src.Status)
                )
                .ForMember(
                    dest => dest.USER,
                    opt => opt.MapFrom(src => src.User)
                );
            CreateMap<UserVM, User>()
               .ForMember(
                   dest => dest.UserId,
                   opt => opt.MapFrom(src => src.USER_ID)
               )
               .ForMember(
                   dest => dest.UserName,
                   opt => opt.MapFrom(src => src.USER_NAME)
               )
               .ForMember(
                   dest => dest.UserPass,
                   opt => opt.MapFrom(src => src.USER_PASS)
               )
               .ForMember(
                   dest => dest.UserEmail,
                   opt => opt.MapFrom(src => src.USER_EMAIL)
               )
               .ForMember(
                   dest => dest.Books,
                   opt => opt.MapFrom(src => src.BOOKS)
               );
            CreateMap<BookVM, Book>()
                .ForMember(
                    dest => dest.BookId,
                    opt => opt.MapFrom(src => src.BOOK_ID)
                )
                .ForMember(
                    dest => dest.BookName,
                    opt => opt.MapFrom(src => src.BOOK_NAME)
                )
                .ForMember(
                    dest => dest.BookDescription,
                    opt => opt.MapFrom(src => src.BOOK_DES)
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.USER_ID)
                )
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.STATUS)
                )
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src.USER)
                );
        }
    }
}

