using Syrup.Core.Db.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IUserRepository
{
    ValueTask<User?> GetAsync(long id);
    Task<User?> GetByNicknameAsync(string nickname);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(long userId);
}
