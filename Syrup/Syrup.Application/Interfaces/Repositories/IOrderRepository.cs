using Syrup.Core.Db.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    ValueTask<Order?> GetAsync(long id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}
