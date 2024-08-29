using AutoMapper;
using ecommerce_backend.Dto;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, ReadUserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}