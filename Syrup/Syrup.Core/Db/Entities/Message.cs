namespace Syrup.Core.Database.Entities;

public partial class Message
{
    public long Id { get; set; }

    public long ChatId { get; set; }

    public long AuthorId { get; set; }

    public string? Text { get; set; }

    public DateTime CreationDatetime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;

    public virtual ICollection<DeletedUserMessage> DeletedUserMessages { get; set; } = new List<DeletedUserMessage>();
}
