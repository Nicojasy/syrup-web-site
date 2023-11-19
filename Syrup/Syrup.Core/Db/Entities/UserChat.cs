namespace Syrup.Core.Db.Entities;

public partial class UserChat
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ChatId { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
