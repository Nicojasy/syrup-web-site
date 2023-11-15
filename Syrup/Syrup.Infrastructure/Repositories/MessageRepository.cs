using Syrup.Application.Repositories.Interfaces;
using Syrup.Core.Models.Entities;

namespace Syrup.Application.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SyrupContext _syrupContext;

    public MessageRepository(SyrupContext syrupContext)
    {
        _syrupContext = syrupContext;
    }

    public ValueTask<Chat?> GetAsync(long id)
    {
        return _syrupContext.Chats.FindAsync(id);
    }

    public async Task AddAsync(Message message)
    {
        await _syrupContext.Chats.AddAsync(message);
        await _syrupContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Chat chat)
    {
        var dbChat = await _syrupContext.Chats.FindAsync(chat.Id);
        if (dbChat != null)
        {
            dbChat.Name = chat.Name;
            await _syrupContext.SaveChangesAsync();
        }
    }

    public Task DeleteForUserAsync(long chatId, long userId)
    {
        var userChats = _syrupContext.UserChats.Where(x => x.ChatId == chatId && x.UserId == userId);
        _syrupContext.UserChats.RemoveRange(userChats);
        return _syrupContext.SaveChangesAsync();
    }
}
