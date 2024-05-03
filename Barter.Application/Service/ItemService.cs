using AutoMapper;
using Barter.Application.CustomExceptions;
using Barter.Application.Service.Interface;
using Barter.Application.UnitOfWork;
using Barter.Domain.Models;
using Barter.Domain.Models.Paging;
using Barter.Domain.Models.Search;
using Barter.Domain.Models.Search.Context;
using System.Linq.Expressions;

namespace Barter.Application.Service;

internal class ItemService(IUnitOfWork unitOfWork, IMapper mapper) : IItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddItemAsync(Item item)
    {
        await ValidateCreatePreconditionsForItemAsync(item);

        var addedItem = _unitOfWork.ItemRepository
            .Add(_mapper.Map<Item>(item));

        await _unitOfWork.SaveAsync();

        return addedItem.Id;
    }

    public async Task DeleteItemAsync(string name)
    {
        var pagingRequest = new EntitiesPagingRequest<Item>
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

        await _unitOfWork.ItemRepository.DeleteAsync(existingItem.Items.SingleOrDefault());
    }

    public async Task<SearchResult<Item>> SearchItemWithPagingAsync(ItemSearchContext context)
    {
        Expression<Func<Item, bool>> filter = x =>
                    (!context.Id.HasValue || x.Id == context.Id.Value) &&
                    (string.IsNullOrEmpty(context.Name) || x.Name == context.Name) &&
                    (!context.CategoryId.HasValue || x.CategoryId == context.CategoryId.Value) &&
                    (!context.OwnerId.HasValue || x.OwnerId == context.OwnerId.Value) &&
                    (!context.Condition.HasValue || x.Condition == context.Condition) &&
                    (!context.ItemType.HasValue || x.ItemType == context.ItemType);

        var pagingRequest = new EntitiesPagingRequest<Item>
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

        var itemForUpdate = await _unitOfWork.ItemRepository.UpdateAsync(_mapper.Map<Item>(item));

        return _mapper.Map<Item>(itemForUpdate);
    }

    private async Task ValidateCreatePreconditionsForItemAsync(Item model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<Item>
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

        var pagingRequest = new EntitiesPagingRequest<Item>
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
