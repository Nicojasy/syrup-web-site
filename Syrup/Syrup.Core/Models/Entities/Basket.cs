namespace Syrup.Core.Models.Entities;

public partial class Basket
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
