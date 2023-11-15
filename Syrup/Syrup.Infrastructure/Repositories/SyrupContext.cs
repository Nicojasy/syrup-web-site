using Messenger.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Syrup.Core.Models.Entities;

namespace Syrup.Infrastructure.Repositories;
public class SyrupContext : DbContext
{
    public DbSet<Chat> Users => Set<Chat>();
    public SyrupContext() => Database.EnsureCreated();

    public DbSet<Chat> Chats { get; set; }
    public DbSet<UserChat> UserChats { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
