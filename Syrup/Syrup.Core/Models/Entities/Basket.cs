namespace Syrup.Core.Models.Entities;

public class Basket
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    
    public Customer Customer { get; set; } = null!;
}
