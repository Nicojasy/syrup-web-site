namespace Syrup.Core.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Nickname { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public string? AboutMyself { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

    public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    public virtual ICollection<DeletedUserMessage> DeletedUserMessages { get; set; } = new List<DeletedUserMessage>();
}
