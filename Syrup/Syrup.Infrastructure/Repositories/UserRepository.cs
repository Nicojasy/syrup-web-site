using Microsoft.EntityFrameworkCore;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Database.Entities;

namespace Syrup.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SyrupContext _syrupContext;

    public UserRepository(SyrupContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<User?> GetAsync(long id) =>
        _syrupContext.Users.FindAsync(id);

    public Task<User?> GetAsync(string nickname) =>
        _syrupContext.Users.FirstOrDefaultAsync(x => x.Nickname == nickname);

    public async Task AddAsync(User user)
    {
        await _syrupContext.Users.AddAsync(user);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(User user)
    {
        _syrupContext.Users.Update(user);
        return _syrupContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long userId)
    {
        var t = await _syrupContext.Users.FindAsync(userId);
        if (t is not null)
        {
            t.IsDeleted = true;
            await _syrupContext.SaveChangesAsync();
        }
    }
}
