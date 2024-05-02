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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Barter.Ge.BLL.Services;
public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddCategoryAsync(Category category)
    {
        await ValidateCreatePreconditionsForCategoryAsync(category);

        var createdCategory = _unitOfWork.CategoryRepository
            .Add(_mapper.Map<CategoryEntity>(category));

        await _unitOfWork.SaveAsync();

        return createdCategory.Id;
    }

    public async Task DeleteCategoryAsync(string name)
    {
        var pagingRequest = new EntitiesPagingRequest<CategoryEntity>
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

        await _unitOfWork.CategoryRepository.DeleteAsync(existingCategory.Items.FirstOrDefault());
    }

    public async Task<SearchResult<Category>> SearchCategoriesWithPaging(CategorySearchContext context)
    {
        Expression<Func<CategoryEntity, bool>> filter = x =>
            (!context.Id.HasValue || x.Id == context.Id.Value) &&
            (string.IsNullOrEmpty(context.Name) || x.Name == context.Name) &&
            (!context.ParentCategoryId.HasValue || x.ParentCategoryId == context.ParentCategoryId.Value);

        var pagingRequest = new EntitiesPagingRequest<CategoryEntity>
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

        var categoryForUpdate = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<CategoryEntity>(category));

        return _mapper.Map<Category>(categoryForUpdate);
    }

    private async Task ValidateCreatePreconditionsForCategoryAsync(Category model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<CategoryEntity>
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

        var pagingRequest = new EntitiesPagingRequest<CategoryEntity>
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