namespace Syrup.Core.Models.Entities;

public class Message
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public long UserId { get; set; }
    public string? Text { get; set; }
    public DateTime CreationDatetime { get; set; }
    public bool IsDeleted { get; set; }

    public Chat Chat { get; set; } = null!;
    public UserBase User { get; set; } = null!;
}
