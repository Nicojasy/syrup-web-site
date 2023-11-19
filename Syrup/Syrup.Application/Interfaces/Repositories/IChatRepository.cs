using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IChatRepository
{
    ValueTask<Chat?> GetAsync(long id);
    Task AddAsync(Chat chat);
    Task UpdateAsync(Chat chat);
    Task DeleteForUserAsync(long chatId, long userId);
}
