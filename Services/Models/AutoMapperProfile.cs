using System;
using AutoMapper;
using DAL.Models;

namespace Services.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>();
            CreateMap<Book, BookVM>();
        }
    }
}

