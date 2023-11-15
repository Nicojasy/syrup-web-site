using Syrup.Core.Models.Entities;

namespace Syrup.Application.Repositories.Interfaces;

public interface IChatRepository
{
    ValueTask<Chat?> GetAsync(long id);
    Task AddAsync(Chat chat);
    Task UpdateAsync(Chat chat);
    Task DeleteForUserAsync(long chatId, long userId);
}
