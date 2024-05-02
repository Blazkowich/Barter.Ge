using AutoMapper;
using Barter.Ge.Api.ApiModels.Request;
using Barter.Ge.Api.ApiModels.Response;
using Barter.Ge.BLL.CustomExceptions;
using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barter.Ge.Api.Controllers;

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

        var getCategoryById = await _categoryService.SearchCategoriesWithPaging(searchContext);

        return _mapper.Map<CategoryResponse>(getCategoryById.Items.SingleOrDefault());
    }

    [HttpGet("{name}")]
    public async Task<CategoryResponse> GetCategoryByName(string name)
    {
        var searchContext = new CategorySearchContext { Name = name };

        var getCategoryByName = await _categoryService.SearchCategoriesWithPaging(searchContext);

        return _mapper.Map<CategoryResponse>(getCategoryByName.Items.SingleOrDefault());
    }

    [HttpGet]
    public async Task<List<CategoryResponse>> GetAllCategories([FromQuery] CategorySearchContext searchContext)
    {
        var getAllCategories = await _categoryService.SearchCategoriesWithPaging(searchContext);

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
