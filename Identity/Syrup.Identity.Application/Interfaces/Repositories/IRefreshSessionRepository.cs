using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Application.Interfaces.Repositories;

public interface IRefreshSessionRepository
{
    ValueTask<RefreshSession?> GetAsync(long id);
    Task RemoveAndAddAsync(RefreshSession refreshSession);
    Task DeleteAsync(long userId);
}
