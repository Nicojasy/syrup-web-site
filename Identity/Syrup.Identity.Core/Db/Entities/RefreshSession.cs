namespace Syrup.Identity.Core.Db.Entities;

public partial class RefreshSession
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreationDateTime { get; set; }
    
    public virtual User User { get; set; } = null!;
}
