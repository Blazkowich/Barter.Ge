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
public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddCategoryAsync(Category category)
    {
        await ValidateCreatePreconditionsForCategoryAsync(category);

        var createdCategory = _unitOfWork.CategoryRepository
            .Add(_mapper.Map<Category>(category));

        await _unitOfWork.SaveAsync();

        return createdCategory.Id;
    }

    public async Task DeleteCategoryAsync(string name)
    {
        var pagingRequest = new EntitiesPagingRequest<Category>
        {
            Filter = g => g.Name == name,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var existingCategory = await _unitOfWork.CategoryRepository.SearchWithPagingAsync(pagingRequest);

        if (!existingCategory.Items.Any())
        {
            throw new NotFoundException($"No Category was found on Name '{name}' For The Remove.");
        }

        await _unitOfWork.CategoryRepository.DeleteAsync(existingCategory.Items.SingleOrDefault());
    }

    public async Task<SearchResult<Category>> SearchCategoriesWithPagingAsync(CategorySearchContext context)
    {
        Expression<Func<Category, bool>> filter = x =>
            (!context.Id.HasValue || x.Id == context.Id.Value) &&
            (string.IsNullOrEmpty(context.Name) || x.Name == context.Name) &&
            (!context.ParentCategoryId.HasValue || x.ParentCategoryId == context.ParentCategoryId.Value);

        var pagingRequest = new EntitiesPagingRequest<Category>
        {
            Filter = filter,
            PageNumber = context.PageNumber,
            PerPage = context.PerPage,
        };

        var result = await _unitOfWork.CategoryRepository.SearchWithPagingAsync(pagingRequest);

        if (!result.Items.Any())
        {
            throw new NotFoundException("Category Was Not Found");
        }

        return new SearchResult<Category>
        {
            Items = _mapper.Map<List<Category>>(result.Items),
            ItemsTotalCount = result.ItemsTotalCount,
            PageNumber = result.PageNumber,
            PerPage = result.PerPage,
        };
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        await ValidateUpdatePreconditionsForCategoryAsync(category);

        var categoryForUpdate = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(category));

        return _mapper.Map<Category>(categoryForUpdate);
    }

    private async Task ValidateCreatePreconditionsForCategoryAsync(Category model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<Category>
        {
            Filter = category => category.Name == model.Name,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfCategoryExistsTask = await _unitOfWork.CategoryRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfCategoryExistsTask != null && checkIfCategoryExistsTask.Items.Count != 0)
        {
            throw new BadRequestException($"Category Exists on Name {model.Name}");
        }

    }

    private async Task ValidateUpdatePreconditionsForCategoryAsync(Category model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<Category>
        {
            Filter = category => category.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfCategoryExistsTask = await _unitOfWork.CategoryRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfCategoryExistsTask is null || checkIfCategoryExistsTask.Items.Count == 0)
        {
            throw new BadRequestException($"Category Does Not Exists on Name {model.Name}");
        }
    }
}