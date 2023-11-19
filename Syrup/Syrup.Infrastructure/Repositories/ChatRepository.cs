using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Db.Entities;

namespace Syrup.Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly SyrupContext _syrupContext;

    public ChatRepository(SyrupContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<Chat?> GetAsync(long id) =>
        _syrupContext.Chats.FindAsync(id);

    public async Task AddAsync(Chat chat)
    {
        await _syrupContext.Chats.AddAsync(chat);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Chat chat)
    {
        _syrupContext.Chats.Update(chat);
        return _syrupContext.SaveChangesAsync();
    }

    public Task DeleteForUserAsync(long chatId, long userId)
    {
        var userChats = _syrupContext.UserChats.Where(x => x.ChatId == chatId && x.UserId == userId);
        _syrupContext.UserChats.RemoveRange(userChats);
        return _syrupContext.SaveChangesAsync();
    }
}
