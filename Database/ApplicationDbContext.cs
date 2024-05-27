using Microsoft.EntityFrameworkCore;
using RealTimeDatabase.Models;

namespace RealTimeDatabase.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product>? Product { get; set; }
    public DbSet<Sale>? Sale { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}