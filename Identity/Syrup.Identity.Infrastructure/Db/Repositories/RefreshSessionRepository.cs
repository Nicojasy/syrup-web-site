using Microsoft.EntityFrameworkCore;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Infrastructure.Db.Repositories;

public class RefreshSessionRepository : IRefreshSessionRepository
{
    private readonly SyrupIdentityDbContext _syrupIdentityContext;

    public RefreshSessionRepository(SyrupIdentityDbContext syrupContext) => _syrupIdentityContext = syrupContext;

    public ValueTask<RefreshSession?> GetAsync(long id) =>
        _syrupIdentityContext.RefreshSessions.FindAsync(id);
    
    public async Task RemoveAndAddAsync(RefreshSession refreshSession)
    {
        await _syrupIdentityContext.RefreshSessions
            .Where(x=> x.UserId == refreshSession.UserId)
            .ExecuteDeleteAsync();
        await _syrupIdentityContext.RefreshSessions.AddAsync(refreshSession);

        await _syrupIdentityContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long userId)
    {
        await _syrupIdentityContext.RefreshSessions
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync();
        await _syrupIdentityContext.SaveChangesAsync();
    }
}
