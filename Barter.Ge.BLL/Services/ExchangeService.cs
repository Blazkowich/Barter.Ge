using AutoMapper;
using Barter.Ge.BLL.CustomExceptions;
using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Services.Interfaces;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Context.Paging;
using Barter.Ge.DAL.Repositories.UnitOfWork;
using System.Linq.Expressions;
using Barter.Ge.DAL.Context.Enum;

namespace Barter.Ge.BLL.Services;

internal class ExchangeService(IUnitOfWork unitOfWork, IMapper mapper) : IExchangeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> CreateExchangeAsync(Exchange exchange)
    {
        await ValidateCreatePreconditionsForExchangeAsync(exchange);

        var createdExchange = _unitOfWork.ExchangeRepository
            .Add(_mapper.Map<ExchangeEntity>(exchange));

        await _unitOfWork.SaveAsync();

        return createdExchange.Id;
    }

    public async Task DeleteExchangeAsync(Guid id)
    {
        var pagingRequest = new EntitiesPagingRequest<ExchangeEntity>
        {
            Filter = g => g.Id == id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var existingExchange = await _unitOfWork.ExchangeRepository.SearchWithPagingAsync(pagingRequest);

        if (!existingExchange.Items.Any())
        {
            throw new NotFoundException($"No Exchange was found For The Remove.");
        }

        await _unitOfWork.ExchangeRepository.DeleteAsync(existingExchange.Items.SingleOrDefault());
    }

    public async Task<SearchResult<Exchange>> SearchExchangeWithPagingAsync(ExchangeSearchContext context)
    {
        Expression<Func<ExchangeEntity, bool>> filter = x =>
            (!context.Id.HasValue || x.Id == context.Id.Value) &&
            (!context.InitiatorId.HasValue || x.InitiatorId == context.InitiatorId.Value) &&
            (!context.ReceiverId.HasValue || x.ReceiverId == context.ReceiverId.Value) &&
            (!context.ItemOfferedId.HasValue || x.ItemOfferedId == context.ItemOfferedId.Value) &&
            (!context.ItemRequestedId.HasValue || x.ItemRequestedId == context.ItemRequestedId.Value) &&
            (context.Status == ExchangeStatus.Pending || x.Status == (int)context.Status) &&
            (context.Status == ExchangeStatus.Accepted || x.Status == (int)context.Status) &&
            (context.Status == ExchangeStatus.Completed || x.Status == (int)context.Status) &&
            (context.Status == ExchangeStatus.Rejected || x.Status == (int)context.Status) &&
            (context.Status == ExchangeStatus.Cancelled || x.Status == (int)context.Status) &&
            (!context.CreatedAt.HasValue || x.CreatedAt == context.CreatedAt.Value) &&
            (!context.UpdatedAt.HasValue || x.UpdatedAt == context.UpdatedAt.Value) &&
            (!context.ExchangedAt.HasValue || x.ExchangedAt == context.ExchangedAt.Value);

        var pagingRequest = new EntitiesPagingRequest<ExchangeEntity>
        {
            Filter = filter,
            PageNumber = context.PageNumber,
            PerPage = context.PerPage,
        };

        var result = await _unitOfWork.ExchangeRepository.SearchWithPagingAsync(pagingRequest);

        if (!result.Items.Any())
        {
            throw new NotFoundException("Exchange Was Not Found");
        }

        return new SearchResult<Exchange>
        {
            Items = _mapper.Map<List<Exchange>>(result.Items),
            ItemsTotalCount = result.ItemsTotalCount,
            PageNumber = result.PageNumber,
            PerPage = result.PerPage,
        };
    }

    public async Task<Exchange> UpdateExchangeAsync(Exchange exchange)
    {
        await ValidateUpdatePreconditionsForExchangeAsync(exchange);

        var exchangeForUpdate = await _unitOfWork.ExchangeRepository.UpdateAsync(_mapper.Map<ExchangeEntity>(exchange));

        return _mapper.Map<Exchange>(exchangeForUpdate);
    }

    private async Task ValidateCreatePreconditionsForExchangeAsync(Exchange model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<ExchangeEntity>
        {
            Filter = category => category.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfExchangeExistsTask = await _unitOfWork.ExchangeRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfExchangeExistsTask != null && checkIfExchangeExistsTask.Items.Count != 0)
        {
            throw new BadRequestException($"Exchange Exists.");
        }

    }

    private async Task ValidateUpdatePreconditionsForExchangeAsync(Exchange model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<ExchangeEntity>
        {
            Filter = category => category.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfExchangeExistsTask = await _unitOfWork.ExchangeRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfExchangeExistsTask is null || checkIfExchangeExistsTask.Items.Count == 0)
        {
            throw new BadRequestException($"Exchange Does Not Exists.");
        }
    }
}
