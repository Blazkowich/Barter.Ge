using AutoMapper;
using Barter.Ge.Api.ApiModels.Request;
using Barter.Ge.Api.ApiModels.Response;
using Barter.Ge.BLL.Models;
using Barter.Ge.DAL.Context.Entities;

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

        #region Exchange Mapping
        CreateMap<CreateExchangeRequest, Exchange>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.InitiatorId, opt => opt.MapFrom(src => src.InitiatorId))
            .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId))
            .ForMember(dest => dest.ItemOfferedId, opt => opt.MapFrom(src => src.ItemOfferedId))
            .ForMember(dest => dest.ItemRequestedId, opt => opt.MapFrom(src => src.ItemRequestedId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ExchangedAt, opt => opt.Ignore());

        CreateMap<UpdateExchangeRequest, Exchange>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.InitiatorId, opt => opt.MapFrom(src => src.InitiatorId))
            .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId))
            .ForMember(dest => dest.ItemOfferedId, opt => opt.MapFrom(src => src.ItemOfferedId))
            .ForMember(dest => dest.ItemRequestedId, opt => opt.MapFrom(src => src.ItemRequestedId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.ExchangedAt, opt => opt.MapFrom(src => src.ExchangedAt));

        CreateMap<Exchange, ExchangeResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.InitiatorId, opt => opt.MapFrom(src => src.InitiatorId))
            .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId))
            .ForMember(dest => dest.ItemOfferedId, opt => opt.MapFrom(src => src.ItemOfferedId))
            .ForMember(dest => dest.ItemRequestedId, opt => opt.MapFrom(src => src.ItemRequestedId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.ExchangedAt, opt => opt.MapFrom(src => src.ExchangedAt));
        #endregion

        #region Item Mapping
        CreateMap<CreateItemRequest, Item>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));

        CreateMap<UpdateItemRequest, Item>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));

        CreateMap<Item, ItemResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));
        #endregion
    }
}
