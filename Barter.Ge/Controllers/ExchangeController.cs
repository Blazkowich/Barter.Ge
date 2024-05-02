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
[Route("exchange")]
public class ExchangeController(IExchangeService exchangeService, IMapper mapper) : ControllerBase
{
    private readonly IExchangeService _exchangeService = exchangeService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<Guid> CreateExchange([FromBody] CreateExchangeRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var addExchange = await _exchangeService.CreateExchangeAsync(_mapper.Map<Exchange>(model));

        return addExchange;
    }

    [HttpGet("find/{id}")]
    public async Task<ExchangeResponse> GetExchangeById(Guid id)
    {
        var searchContext = new ExchangeSearchContext { Id = id };

        var getExchangeById = await _exchangeService.SearchExchangeWithPagingAsync(searchContext);

        return _mapper.Map<ExchangeResponse>(getExchangeById.Items.SingleOrDefault());
    }

    [HttpGet]
    public async Task<List<ExchangeResponse>> GetAllExchanges([FromQuery] ExchangeSearchContext searchContext)
    {
        var getAllExchanges = await _exchangeService.SearchExchangeWithPagingAsync(searchContext);

        return _mapper.Map<List<ExchangeResponse>>(getAllExchanges.Items);
    }

    [HttpPut]
    public async Task<ExchangeResponse> UpdateExchange([FromBody] UpdateExchangeRequest model)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var updateExchange = await _exchangeService.UpdateExchangeAsync(_mapper.Map<Exchange>(model));

        return _mapper.Map<ExchangeResponse>(updateExchange);
    }

    [HttpDelete]
    public async Task DeleteExchange(Guid id)
    {
        await _exchangeService.DeleteExchangeAsync(id);
    }
}
