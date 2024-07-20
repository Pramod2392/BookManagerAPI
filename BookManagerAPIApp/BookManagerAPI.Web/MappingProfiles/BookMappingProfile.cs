using AutoMapper;

namespace BookManagerAPI.Web.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Contracts.Book.BookRequestModel, Service.Models.Book.BookRequestModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Image))
                .ForMember(dest => dest.PurchasedDate, opts => opts.MapFrom(src => src.PurchasedDate));

            //CreateMap<Contracts.Category.GetAllCategoryResponseModel, Service.>
        }
    }
}
