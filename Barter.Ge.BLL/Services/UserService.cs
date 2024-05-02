using AutoMapper;
using Barter.Ge.BLL.CustomExceptions;
using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Services.Interfaces;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Context.Paging;
using Barter.Ge.DAL.Repositories.UnitOfWork;
using System.Linq.Expressions;

namespace Barter.Ge.BLL.Services;

internal class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddUserAsync(User user)
    {
        await ValidateCreatePreconditionsForUserAsync(user);

        var createdUser = _unitOfWork.UserRepository
            .Add(_mapper.Map<UserEntity>(user));

        await _unitOfWork.SaveAsync();

        return createdUser.Id;
    }

    public async Task DeleteUserAsync(string userName)
    {
        var pagingRequest = new EntitiesPagingRequest<UserEntity>
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
        Expression<Func<UserEntity, bool>> filter = x =>
            (!context.Id.HasValue || x.Id == context.Id.Value) &&
            (string.IsNullOrEmpty(context.Username) || x.Username == context.Username) &&
            (string.IsNullOrEmpty(context.Email) || x.Email == context.Email) &&
            (string.IsNullOrEmpty(context.Password) || x.Password == context.Password) &&
            (!context.MobileNumber.HasValue || x.MobileNumber == context.MobileNumber.Value) &&
            (string.IsNullOrEmpty(context.Address) || x.Address == context.Address);

        var pagingRequest = new EntitiesPagingRequest<UserEntity>
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

        var userForUpdate = await _unitOfWork.UserRepository.UpdateAsync(_mapper.Map<UserEntity>(user));

        return _mapper.Map<User>(userForUpdate);
    }

    private async Task ValidateCreatePreconditionsForUserAsync(User model)
    {
        if (model is null)
        {
            throw new BadRequestException("The provided model cannot be null");
        }

        var pagingRequest = new EntitiesPagingRequest<UserEntity>
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

        var pagingRequest = new EntitiesPagingRequest<UserEntity>
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
