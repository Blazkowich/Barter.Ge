using AutoMapper;
using Barter.Ge.Api.ApiModels.Request;
using Barter.Ge.Api.ApiModels.Response;
using Barter.Ge.BLL.CustomExceptions;
using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Services;
using Barter.Ge.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barter.Ge.Api.Controllers;

[ApiController]
[Route("items")]
public class ItemController(IItemService itemService, IMapper mapper) : ControllerBase
{
    private readonly IItemService _itemService = itemService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<Guid> AddItem([FromBody] CreateItemRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var addItem = await _itemService.AddItemAsync(_mapper.Map<Item>(model));
        return addItem;
    }

    [HttpGet("find/{id}")]
    public async Task<ItemResponse> GetItemById(Guid id)
    {
        var searchContext = new ItemSearchContext { Id = id };

        var getItemById = await _itemService.SearchItemWithPagingAsync(searchContext);

        return _mapper.Map<ItemResponse>(getItemById.Items.SingleOrDefault());
    }

    [HttpGet("{name}")]
    public async Task<ItemResponse> GetItemByName(string name)
    {
        var searchContext = new ItemSearchContext { Name = name };

        var getItemByName = await _itemService.SearchItemWithPagingAsync(searchContext);

        return _mapper.Map<ItemResponse>(getItemByName.Items.SingleOrDefault());
    }

    [HttpGet]
    public async Task<List<ItemResponse>> GetAllItems([FromQuery] ItemSearchContext searchContext)
    {
        var getAllItems = await _itemService.SearchItemWithPagingAsync(searchContext);

        return _mapper.Map<List<ItemResponse>>(getAllItems.Items);
    }

    [HttpPut]
    public async Task<ItemResponse> UpdateItem([FromBody] UpdateItemRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var updateItem = await _itemService.UpdateItemAsync(_mapper.Map<Item>(model));

        return _mapper.Map<ItemResponse>(updateItem);
    }

    [HttpDelete]
    public async Task DeleteItem(string name)
    {
        await _itemService.DeleteItemAsync(name);
    }
}
