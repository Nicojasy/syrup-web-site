namespace Syrup.Core.Models.Entities;

public class Chat
{
    public long Id { get; set; }
    public long CreatorId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }

    public List<UserChat> UserChats { get; set; } = [];
    public List<Message> Message { get; set; } = [];
}
