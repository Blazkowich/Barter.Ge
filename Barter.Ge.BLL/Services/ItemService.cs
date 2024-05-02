using AutoMapper;
using Barter.Ge.BLL.CustomExceptions;
using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Models.Enum;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Services.Interfaces;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Context.Paging;
using Barter.Ge.DAL.Repositories.UnitOfWork;
using System.Linq.Expressions;

namespace Barter.Ge.BLL.Services;

internal class ItemService(IUnitOfWork unitOfWork, IMapper mapper) : IItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddItemAsync(Item item)
    {
        await ValidateCreatePreconditionsForItemAsync(item);

        var addedItem = _unitOfWork.ItemRepository
            .Add(_mapper.Map<ItemEntity>(item));

        await _unitOfWork.SaveAsync();

        return addedItem.Id;
    }

    public async Task DeleteItemAsync(string name)
    {
        var pagingRequest = new EntitiesPagingRequest<ItemEntity>
        {
            Filter = g => g.Name == name,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var existingItem = await _unitOfWork.ItemRepository.SearchWithPagingAsync(pagingRequest);

        if (!existingItem.Items.Any())
        {
            throw new NotFoundException($"No Item was found on Name '{name}' For The Remove.");
        }

        await _unitOfWork.ItemRepository.DeleteAsync(existingItem.Items.FirstOrDefault());
    }

    public async Task<SearchResult<Item>> SearchItemWithPagingAsync(ItemSearchContext context)
    {
        Expression<Func<ItemEntity, bool>> filter = x =>
                    (!context.Id.HasValue || x.Id == context.Id.Value) &&
                    (string.IsNullOrEmpty(context.Name) || x.Name == context.Name) &&
                    (!context.CategoryId.HasValue || x.CategoryId == context.CategoryId.Value) &&
                    (!context.OwnerId.HasValue || x.OwnerId == context.OwnerId.Value) &&
                    (!context.Condition.HasValue || x.Condition == (int)context.Condition) &&
                    (!context.ItemType.HasValue || x.ItemType == (int)context.ItemType);

        var pagingRequest = new EntitiesPagingRequest<ItemEntity>
        {
            Filter = filter,
            PageNumber = context.PageNumber,
            PerPage = context.PerPage,
        };

        var result = await _unitOfWork.ItemRepository.SearchWithPagingAsync(pagingRequest);

        if (!result.Items.Any())
        {
            throw new NotFoundException("Item Was Not Found");
        }

        return new SearchResult<Item>
        {
            Items = _mapper.Map<List<Item>>(result.Items),
            ItemsTotalCount = result.ItemsTotalCount,
            PageNumber = result.PageNumber,
            PerPage = result.PerPage,
        };
    }

    public async Task<Item> UpdateItemAsync(Item item)
    {
        await ValidateUpdatePreconditionsForItemAsync(item);

        var itemForUpdate = await _unitOfWork.ItemRepository.UpdateAsync(_mapper.Map<ItemEntity>(item));

        return _mapper.Map<Item>(itemForUpdate);
    }

    private async Task ValidateCreatePreconditionsForItemAsync(Item model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<ItemEntity>
        {
            Filter = category => category.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfItemExistsTask = await _unitOfWork.ItemRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfItemExistsTask != null && checkIfItemExistsTask.Items.Count != 0)
        {
            throw new BadRequestException($"Item Exists on Name");
        }

    }

    private async Task ValidateUpdatePreconditionsForItemAsync(Item model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<ItemEntity>
        {
            Filter = category => category.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfItemExistsTask = await _unitOfWork.ItemRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfItemExistsTask is null || checkIfItemExistsTask.Items.Count == 0)
        {
            throw new BadRequestException($"Item Does Not Exists For Update.");
        }
    }
}
