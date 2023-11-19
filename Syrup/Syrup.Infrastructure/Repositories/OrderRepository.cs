using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Database.Entities;

namespace Syrup.Application.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SyrupContext _syrupContext;

    public OrderRepository(SyrupContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<Order?> GetAsync(long id) =>
        _syrupContext.Orders.FindAsync(id);

    public async Task AddAsync(Order order)
    {
        await _syrupContext.Orders.AddAsync(order);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Order order)
    {
        _syrupContext.Orders.Update(order);
        return _syrupContext.SaveChangesAsync();
    }
}
