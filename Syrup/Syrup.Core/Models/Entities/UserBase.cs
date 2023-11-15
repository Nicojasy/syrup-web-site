namespace Syrup.Core.Models.Entities;

public abstract class UserBase
{
    public long Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
    public DateTime CreationDateTime { get; set; }
    public bool IsDeleted { get; set; }

    public List<UserChat> UserChats { get; set; } = [];
}
