using Syrup.Core.Db.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface IProductRepository
{
    ValueTask<Product?> GetAsync(long id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(long productId);
}
