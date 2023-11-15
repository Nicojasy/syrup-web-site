namespace Syrup.Core.Models.Entities;

public partial class Company
{
    public long Id { get; set; }

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
