namespace Syrup.Identity.Core.Db.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Nickname { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public string? AboutMyself { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<RefreshSession> RefreshSessions { get; set; } = new List<RefreshSession>();
}
