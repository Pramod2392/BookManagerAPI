using AutoMapper;

namespace BookManagerAPI.Web.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<Contracts.User.UserRequestModel, Service.Models.User.UserRequestModel>();
            CreateMap<Service.Models.User.UserResponseModel,Contracts.User.UserResponseModel>();
        }
    }
}
