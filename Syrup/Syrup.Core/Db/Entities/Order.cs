﻿using Syrup.Core.Db.Enums;

namespace Syrup.Core.Db.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public OrderStates State { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User User { get; set; } = null!;
}
