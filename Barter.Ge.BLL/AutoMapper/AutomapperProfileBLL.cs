using AutoMapper;
using Barter.Ge.BLL.Models;
using Barter.Ge.DAL.Context.Entities;

namespace Barter.Ge.BLL.AutoMapper;

public class AutomapperProfileBLL : Profile
{
    public AutomapperProfileBLL()
    {
        #region Category Mapping
        CreateMap<Category, CategoryEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
            .ForMember(dest => dest.Subcategories, opt => opt.Ignore());

        CreateMap<CategoryEntity, Category>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories));
        #endregion

        #region Exchange Mapping
        CreateMap<Exchange, ExchangeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InitiatorId, opt => opt.MapFrom(src => src.InitiatorId))
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId))
                .ForMember(dest => dest.ItemOfferedId, opt => opt.MapFrom(src => src.ItemOfferedId))
                .ForMember(dest => dest.ItemRequestedId, opt => opt.MapFrom(src => src.ItemRequestedId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.ExchangedAt, opt => opt.MapFrom(src => src.ExchangedAt));

        CreateMap<ExchangeEntity, Exchange>()
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
        CreateMap<Item, ItemEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
                .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
                .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));

        CreateMap<ItemEntity, Item>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));
        #endregion

        #region User Mapping
        CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));
        #endregion
    }
}