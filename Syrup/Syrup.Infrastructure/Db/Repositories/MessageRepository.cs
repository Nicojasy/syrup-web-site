using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Db.Entities;

namespace Syrup.Infrastructure.Db.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SyrupDbContext _syrupContext;

    public MessageRepository(SyrupDbContext syrupContext)
    {
        _syrupContext = syrupContext;
    }

    public ValueTask<Message?> GetAsync(long id) =>
        _syrupContext.Messages.FindAsync(id);

    public async Task AddAsync(Message message)
    {
        await _syrupContext.Messages.AddAsync(message);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Message message)
    {
        _syrupContext.Messages.Update(message);
        return _syrupContext.SaveChangesAsync();
    }

    public Task AddDeletedUserMessageAsync(DeletedUserMessage deletedUserMessage)
    {
        _syrupContext.DeletedUserMessage.AddAsync(deletedUserMessage);
        return _syrupContext.SaveChangesAsync();
    }
}
