using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IMessageRepository
{
    ValueTask<Message?> GetAsync(long id);
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task AddDeletedUserMessageAsync(DeletedUserMessage deletedUserMessage);
}
