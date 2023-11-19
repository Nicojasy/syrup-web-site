using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IUserRepository
{
    ValueTask<User?> GetAsync(long id);
    Task<User?> GetAsync(string nickname);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(long userId);
}
