using AutoMapper;
using Barter.Application.CustomExceptions;
using Barter.Application.Models.Request;
using Barter.Application.Models.Response;
using Barter.Application.Service.Interface;
using Barter.Domain.Models;
using Barter.Domain.Models.Search.Context;
using Microsoft.AspNetCore.Mvc;

namespace Barter.Api.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService categoryService, IMapper mapper) : ControllerBase
{
    private ICategoryService _categoryService = categoryService;
    private IMapper _mapper = mapper;

    [HttpPost]
    public async Task<Guid> AddCategory([FromBody] CreateCategoryRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var addCategory = await _categoryService.AddCategoryAsync(_mapper.Map<Category>(model));

        return addCategory;
    }

    [HttpGet("find/{id}")]
    public async Task<CategoryResponse> GetCategoryById(Guid id)
    {
        var searchContext = new CategorySearchContext { Id = id };

        var getCategoryById = await _categoryService.SearchCategoriesWithPagingAsync(searchContext);

        return _mapper.Map<CategoryResponse>(getCategoryById.Items.SingleOrDefault());
    }

    [HttpGet("{name}")]
    public async Task<CategoryResponse> GetCategoryByName(string name)
    {
        var searchContext = new CategorySearchContext { Name = name };

        var getCategoryByName = await _categoryService.SearchCategoriesWithPagingAsync(searchContext);

        return _mapper.Map<CategoryResponse>(getCategoryByName.Items.SingleOrDefault());
    }

    [HttpGet]
    public async Task<List<CategoryResponse>> GetAllCategories([FromQuery] CategorySearchContext searchContext)
    {
        var getAllCategories = await _categoryService.SearchCategoriesWithPagingAsync(searchContext);

        return _mapper.Map<List<CategoryResponse>>(getAllCategories.Items);
    }

    [HttpPut]
    public async Task<CategoryResponse> UpdateCategory([FromBody] UpdateCategoryRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var updateCategory = await _categoryService.UpdateCategoryAsync(_mapper.Map<Category>(model));

        return _mapper.Map<CategoryResponse>(updateCategory);
    }

    [HttpDelete]
    public async Task DeleteCategory(string name)
    {
        await _categoryService.DeleteCategoryAsync(name);
    }
}
