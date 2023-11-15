namespace Syrup.Core.Models.Entities;

public class Seller : UserBase
{
    public string Name { get; set; } = null!;
    public string? AboutMyself { get; set; }

    public ICollection<Product> Orders { get; } = new List<Product>();
}
