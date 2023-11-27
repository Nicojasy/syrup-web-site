using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Application.Interfaces.Repositories;

public interface IUserRepository
{
    ValueTask<User?> GetAsync(long id);
    Task<User?> GetByNicknameAsync(string nickname);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(long userId);
}
