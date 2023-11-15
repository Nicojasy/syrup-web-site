namespace Syrup.Core.Models.Entities;

public class Favorite
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long ProductId { get; set; }

    public Customer Customer { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
