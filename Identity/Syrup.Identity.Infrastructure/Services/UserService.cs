using AutoMapper;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Result.Helpers;
using Syrup.Result.Models;

namespace Syrup.Identity.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserResponse> GetAsync(long id) =>
        _mapper.Map<UserResponse>(
            await _userRepository.GetAsync(id));

    public async Task<ValueOperationResult<UserResponse>> UpdateAsync(UpdateUserRequest request, long userId)
    {
        if (request.Id == userId) return ResultHelper.Bad<UserResponse>("The user is not available for update");

        var user = await _userRepository.GetAsync(request.Id);
        if (user is null) return ResultHelper.Bad<UserResponse>("The user doesn't exist");

        user.Email = request.Email;
        user.Email = request.AboutMyself;
        
        await _userRepository.UpdateAsync(user);

        var response = _mapper.Map<UserResponse>(user);
        return ResultHelper.Success(response);
    }

    public async Task<OperationResult> DeleteAsync(long id, long userId)
    {
        if (id == userId) return ResultHelper.Bad("The user is not available for update");

        var user = await _userRepository.GetAsync(id);
        if (user is null) return ResultHelper.Bad("The user doesn't exist");

        user.IsDeleted = true;

        await _userRepository.UpdateAsync(user);
        return ResultHelper.Success();
    }
}
