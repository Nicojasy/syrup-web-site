using Microsoft.EntityFrameworkCore;

namespace Syrup.Core.Db.Entities;

public class SyrupDbContext : DbContext
{
    public SyrupDbContext()
    {
    }

    public SyrupDbContext(DbContextOptions<SyrupDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChat> UserChats { get; set; }

    public virtual DbSet<DeletedUserMessage> DeletedUserMessage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chat_pkey");

            entity.ToTable("Chat");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Creator).WithMany(p => p.Chats)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chat_CreatorId_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Company_pkey");

            entity.ToTable("Company");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyUser_pkey");

            entity.ToTable("CompanyUser");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CompanyUser_CompanyId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CompanyUser_UserId_fkey");
        });

        modelBuilder.Entity<FavoriteProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FavoriteProduct_pkey");

            entity.ToTable("FavoriteProduct");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FavoriteProduct_ProductId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FavoriteProduct_UserId_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pkey");

            entity.ToTable("Message");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.Messages)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_AuthorId_fkey");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_ChatId_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_UserId_fkey");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderProduct_pkey");

            entity.ToTable("OrderProduct");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderProduct_OrderId_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderProduct_ProductId_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.ToTable("Product");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Products)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_CompanyId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserChat_pkey");

            entity.ToTable("UserChat");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Chat).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserChat_ChatId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserChat_UserId_fkey");
        });

        modelBuilder.Entity<DeletedUserMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DeletedUserMessage_pkey");

            entity.ToTable("DeletedUserMessage");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Message).WithMany(p => p.DeletedUserMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DeletedUserMessage_MessageId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.DeletedUserMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DeletedUserMessage_UserId_fkey");
        });
    }
}
