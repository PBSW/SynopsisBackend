using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary Keys
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id)
            .HasName("PK_Id");
        
        // Auto Increment
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        
        // Unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Mail)
            .IsUnique();
    }
    
    public DbSet<User> Users { get; set; }
}