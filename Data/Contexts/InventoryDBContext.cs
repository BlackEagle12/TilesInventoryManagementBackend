using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public partial class InventoryDBContext : DbContext
{
    public InventoryDBContext()
    {
    }

    public InventoryDBContext(DbContextOptions<InventoryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FE7AD815A");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__countrie__3213E83FFD107A49");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83F2D314129");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FCE205E13");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_per__3213E83F758D1465");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__states__3213E83F8208F9BB");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FC8A8A869");

            entity.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
