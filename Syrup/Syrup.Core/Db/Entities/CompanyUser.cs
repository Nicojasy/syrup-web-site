using Syrup.Core.Database.Enums;

namespace Syrup.Core.Database.Entities;

public partial class CompanyUser
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public long UserId { get; set; }

    public CompanyUserRole Type { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
