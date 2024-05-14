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
        
        // Auto Increment
        modelBuilder.Entity<ToDoList>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
    }
    
    public DbSet<ToDoList> ToDoLists { get; set; }
}