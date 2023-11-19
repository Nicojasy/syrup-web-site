namespace Syrup.Core.Db.Entities;

public partial class DeletedUserMessage
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long MessageId { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
