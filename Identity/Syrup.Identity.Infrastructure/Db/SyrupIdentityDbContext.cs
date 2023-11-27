using Microsoft.EntityFrameworkCore;
using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Infrastructure.Db;

public class SyrupIdentityDbContext : DbContext
{
    public SyrupIdentityDbContext()
    {
    }

    public SyrupIdentityDbContext(DbContextOptions<SyrupIdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RefreshSession> RefreshSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RefreshSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RefreshSession_pkey");

            entity.ToTable("RefreshSession");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.RefreshSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefreshSession_UserId_fkey");
        });
    }
}
