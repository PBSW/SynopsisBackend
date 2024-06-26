﻿using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    
    public DbSet<AuthUser> AuthUsers { get; set; }
}