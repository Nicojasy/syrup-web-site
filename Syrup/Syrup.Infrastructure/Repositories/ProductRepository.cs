using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Database.Entities;

namespace Syrup.Application.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SyrupContext _syrupContext;

    public ProductRepository(SyrupContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<Product?> GetAsync(long id) =>
        _syrupContext.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {
        await _syrupContext.Products.AddAsync(product);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Product product)
    {
        _syrupContext.Products.Update(product);
        return _syrupContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long productId)
    {
        var t = await _syrupContext.Products.FindAsync(productId);
        if (t is not null)
        {
            t.IsDeleted = true;
            await _syrupContext.SaveChangesAsync();
        }
    }
}
