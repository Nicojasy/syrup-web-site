namespace Messenger.Core.Models.Entities;

public class Photo
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }
}
