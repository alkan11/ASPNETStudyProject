using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace WebApiProject.Utility.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
