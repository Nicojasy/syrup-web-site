﻿using Syrup.Core.Db.Enums;

namespace Syrup.Core.Db.Entities;

public partial class Product
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public int Price { get; set; }

    public CurrencyCode Currency { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}