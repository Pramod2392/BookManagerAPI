using AutoMapper;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Service.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.MappingProfiles
{
    public class UserServiceMappingProfile : Profile
    {
        public UserServiceMappingProfile()
        {
            CreateMap<UserRequestModel, AddUserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new Guid(src.UserId)))
                .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UserRequestModel, UserResponseModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
