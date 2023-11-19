namespace Syrup.Core.Db.Entities;

public partial class FavoriteProduct
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
