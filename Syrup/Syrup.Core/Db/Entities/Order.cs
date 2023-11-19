using Syrup.Core.Database.Enums;

namespace Syrup.Core.Database.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public OrderStates State { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User User { get; set; } = null!;
}
