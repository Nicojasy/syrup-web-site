namespace Syrup.Application.Interfaces.Services;
public interface IUserService
{
    Task GetByNicknameAsync(string nickname);
}
