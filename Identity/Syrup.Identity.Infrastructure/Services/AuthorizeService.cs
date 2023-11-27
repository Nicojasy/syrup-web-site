using System.Security.Claims;
using IdGen;
using Syrup.Identity.Application.Dtos.Requests;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Identity.Core.Db.Entities;
using Syrup.Result.Helpers;
using Syrup.Result.Models;

namespace Syrup.Identity.Infrastructure.Services;
public class AuthorizeService : IAuthorizeService
{
    private const int _refreshTokenExpirationTimeInDays = 1;
    
    private readonly IdGenerator _idGenerator;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshSessionRepository _refreshSessionRepository;

    public AuthorizeService(
        IdGenerator idGenerator,
        IJwtTokenService jwtTokenService,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IRefreshSessionRepository refreshSessionRepository)
    {
        _idGenerator = idGenerator;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _refreshSessionRepository = refreshSessionRepository;
    }

    public async Task<ValueOperationResult<AuthTokensResponse>> SignInAsync(SignInRequest request)
    {
        var user = await _userRepository.GetByNicknameAsync(request.Login);
        if (user is null
            || !_passwordHasher.CheckPassword(user.PasswordHash, request.Password))
        {
            return ResultHelper.Bad<AuthTokensResponse>("Password is wrong");
        }

        var claims = GetDefaultClaims(user.Nickname);
        var accessTokenWithExpiration = _jwtTokenService.GenerateAccessToken(claims);
        var newTokens = new AuthTokensResponse(
            accessTokenWithExpiration.AccessToken,
            _jwtTokenService.GenerateRefreshToken(),
            accessTokenWithExpiration.ExpirationDateTime);

        var newRefreshSession = new RefreshSession
        {
            Id = _idGenerator.CreateId(),
            UserId = user.Id,
            Token = newTokens.RefreshToken,
            CreationDateTime = DateTime.UtcNow
        };
        await _refreshSessionRepository.RemoveAndAddAsync(newRefreshSession);
        return ResultHelper.Success(newTokens);
    }

    public async Task<ValueOperationResult<AuthTokensResponse>> RefreshAsync(RefreshTokenRequest request)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var nickname = principal?.Identity?.Name;
        if (nickname is null) return ResultHelper.Bad<AuthTokensResponse>("Invalid token data");

        var user = await _userRepository.GetByNicknameAsync(nickname);
        if (user is null) return ResultHelper.Bad<AuthTokensResponse>("Invalid token data");

        var refreshSession = user.RefreshSessions.FirstOrDefault(x => x.Token == request.RefreshToken);
        if (refreshSession is null) return ResultHelper.Bad<AuthTokensResponse>("Invalid refresh session");

        var expirationDateTime = refreshSession.CreationDateTime.AddDays(_refreshTokenExpirationTimeInDays);
        if (expirationDateTime <= DateTimeOffset.UtcNow)
        {
            return ResultHelper.Bad<AuthTokensResponse>("Invalid refresh session");
        }

        var claims = principal.Claims ?? GetDefaultClaims(nickname);
        var accessTokenWithExpiration = _jwtTokenService.GenerateAccessToken(claims);
        var newTokens = new AuthTokensResponse(
            accessTokenWithExpiration.AccessToken,
            _jwtTokenService.GenerateRefreshToken(),
            accessTokenWithExpiration.ExpirationDateTime);

        return ResultHelper.Success(newTokens);
    }

    public async Task<OperationResult> SignOutAsync(SignOutRequest request, long userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null) return ResultHelper.Bad<AuthTokensResponse>("The user does not exist");

        var refreshSession = user.RefreshSessions.FirstOrDefault(x => x.Token == request.RefreshToken);
        if (refreshSession is null) return ResultHelper.Bad<AuthTokensResponse>("Invalid refresh session");
        
        var expirationDateTime = refreshSession.CreationDateTime.AddDays(_refreshTokenExpirationTimeInDays);
        if (expirationDateTime <= DateTimeOffset.UtcNow)
        {
            return ResultHelper.Bad<AuthTokensResponse>("Invalid client request");
        }
        
        await _refreshSessionRepository.DeleteAsync(refreshSession.Id);
        return ResultHelper.Success();
    }

    private static IEnumerable<Claim> GetDefaultClaims(string nickname)
        => new Claim[] { new(ClaimsIdentity.DefaultNameClaimType, nickname) };
}
