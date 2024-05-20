using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary Keys
        modelBuilder.Entity<ToDoList>()
            .HasKey(t => t.Id)
            .HasName("PK_Id");
        
        modelBuilder.Entity<Item>()
            .HasKey(i => i.Id)
            .HasName("PK_Id");
        
        // Foreign Keys
        modelBuilder.Entity<Item>()
            .HasOne(i => i.ToDoList)
            .WithMany(t => t.Items)
            .HasForeignKey(i => i.ToDoListId)
            .HasConstraintName("FK_ToDoListId");
        
        // Auto Increment
        modelBuilder.Entity<ToDoList>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Item>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
    }
    
    public DbSet<ToDoList> ToDoLists { get; set; }
    public DbSet<Item> Items { get; set; }
}