namespace Syrup.Core.Models.Entities;

public partial class Message
{
    public long Id { get; set; }

    public long ChatId { get; set; }

    public long AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;
}
