namespace Syrup.Core.Models.Entities;

public class Product
{
    public long Id { get; set; }
    public long SellerId { get; set; }
    public int Price { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Seller Seller { get; set; } = null!;
    public List<Favorite> Favorites { get; set; } = [];
}
