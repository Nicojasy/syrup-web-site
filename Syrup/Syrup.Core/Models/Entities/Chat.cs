namespace Syrup.Core.Models.Entities;

public partial class Chat
{
    public long Id { get; set; }

    public long CreatorId { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime CreationDateTime { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();
}
