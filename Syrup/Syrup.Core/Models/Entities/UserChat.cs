namespace Syrup.Core.Models.Entities;

public class UserChat
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public DateTime JoinedDateTime { get; set; }

    public UserBase User { get; set; } = null!;
    public Chat Chat { get; set; } = null!;
}
