using Microsoft.EntityFrameworkCore;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Infrastructure.Db.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SyrupIdentityDbContext _syrupContext;

    public UserRepository(SyrupIdentityDbContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<User?> GetAsync(long id) =>
        _syrupContext.Users.FindAsync(id);

    public Task<User?> GetByNicknameAsync(string nickname) =>
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
