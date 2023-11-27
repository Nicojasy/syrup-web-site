using Syrup.Identity.Application.Dtos.Requests;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Identity.Core.Db.Entities;
using Syrup.Result.Helpers;
using Syrup.Result.Models;

namespace Syrup.Identity.Infrastructure.Services;
public class UserRegistrationService : IUserRegistrationService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserRegistrationService(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> SignupAsync(SignupRequest request)
    {
        var user = await _userRepository.GetByNicknameAsync(request.Login);
        if (user is not null) return ResultHelper.Bad("The nickname already exists");

        var passwordHash = _passwordHasher.Hash(request.Password);
        var newUser = new User
        {
            Nickname = request.Login,
            PasswordHash = passwordHash
        };

        await _userRepository.AddAsync(newUser);
        return ResultHelper.Success();
    }
}