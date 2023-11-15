using System.Reflection.Metadata;

namespace Syrup.Core.Models.Entities;

public class Customer : UserBase
{
    public string Nickname { get; set; } = null!;

    public Basket? Basket { get; set; }
    public ICollection<Order> Orders { get; } = new List<Order>();
    public List<Favorite> Favorites { get; set; } = [];
}
