using Syrup.Core.Enums;

namespace Syrup.Core.Models.Entities;

public class Order
{
    public long Id { get; set; }
    public int CustomerId { get; set; }
    public OrderState State { get; set; }

    public Customer Customer { get; set; } = null!;
}
