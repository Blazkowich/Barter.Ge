using AutoMapper;
using Barter.Ge.Api.ApiModels.Request;
using Barter.Ge.Api.ApiModels.Response;
using Barter.Ge.BLL.Models;

namespace Barter.Ge.Api.AutoMapper;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        #region Category Mapping
        CreateMap<CreateCategoryRequest, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.Subcategories, opt => opt.Ignore());

        CreateMap<UpdateCategoryRequest, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.Subcategories, opt => opt.Ignore());


        CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId));
        #endregion
    }
}
