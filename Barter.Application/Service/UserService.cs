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

internal class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddUserAsync(User user)
    {
        await ValidateCreatePreconditionsForUserAsync(user);

        var createdUser = _unitOfWork.UserRepository
            .Add(_mapper.Map<User>(user));

        await _unitOfWork.SaveAsync();

        return createdUser.Id;
    }

    public async Task DeleteUserAsync(string userName)
    {
        var pagingRequest = new EntitiesPagingRequest<User>
        {
            Filter = g => g.Username == userName,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var existingUser = await _unitOfWork.UserRepository.SearchWithPagingAsync(pagingRequest);

        if (!existingUser.Items.Any())
        {
            throw new NotFoundException($"No User was found on Name '{userName}' For The Remove.");
        }

        await _unitOfWork.UserRepository.DeleteAsync(existingUser.Items.SingleOrDefault());
    }

    public async Task<SearchResult<User>> SearchUserWithPagingAsync(UserSearchContext context)
    {
        Expression<Func<User, bool>> filter = x =>
            (!context.Id.HasValue || x.Id == context.Id.Value) &&
            (string.IsNullOrEmpty(context.Username) || x.Username == context.Username) &&
            (string.IsNullOrEmpty(context.Email) || x.Email == context.Email) &&
            (string.IsNullOrEmpty(context.Password) || x.Password == context.Password) &&
            (!context.MobileNumber.HasValue || x.MobileNumber == context.MobileNumber.Value) &&
            (string.IsNullOrEmpty(context.Address) || x.Address == context.Address);

        var pagingRequest = new EntitiesPagingRequest<User>
        {
            Filter = filter,
            PageNumber = context.PageNumber,
            PerPage = context.PerPage,
        };

        var result = await _unitOfWork.UserRepository.SearchWithPagingAsync(pagingRequest);

        if (!result.Items.Any())
        {
            throw new NotFoundException("User Was Not Found");
        }

        return new SearchResult<User>
        {
            Items = _mapper.Map<List<User>>(result.Items),
            ItemsTotalCount = result.ItemsTotalCount,
            PageNumber = result.PageNumber,
            PerPage = result.PerPage,
        };
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await ValidateUpdatePreconditionsForUserAsync(user);

        var userForUpdate = await _unitOfWork.UserRepository.UpdateAsync(_mapper.Map<User>(user));

        return _mapper.Map<User>(userForUpdate);
    }

    private async Task ValidateCreatePreconditionsForUserAsync(User model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<User>
        {
            Filter = user => user.Username == model.Username,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfUserExistsTask = await _unitOfWork.UserRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfUserExistsTask != null && checkIfUserExistsTask.Items.Count != 0)
        {
            throw new BadRequestException($"User Exists on Name {model.Username}");
        }

    }

    private async Task ValidateUpdatePreconditionsForUserAsync(User model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<User>
        {
            Filter = user => user.Id == model.Id,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var checkIfUserExistsTask = await _unitOfWork.UserRepository
            .SearchWithPagingAsync(pagingRequest);

        if (checkIfUserExistsTask is null || checkIfUserExistsTask.Items.Count == 0)
        {
            throw new BadRequestException($"User Does Not Exists on ID {model.Id}");
        }
    }
}
