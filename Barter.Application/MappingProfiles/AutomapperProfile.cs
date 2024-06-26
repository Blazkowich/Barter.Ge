﻿using AutoMapper;
using Barter.Application.Models.Request;
using Barter.Application.Models.Response;
using Barter.Domain.Models;

namespace Barter.Application.MappingProfiles;

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
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));

        CreateMap<UpdateItemRequest, Item>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));

        CreateMap<Item, ItemResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Views));
        #endregion

        #region User Mapping
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

        CreateMap<SignInRequest, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        #endregion
    }
}
