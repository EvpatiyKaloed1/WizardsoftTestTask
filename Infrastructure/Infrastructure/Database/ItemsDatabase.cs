using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ItemsDatabase : DbContext
{
    public ItemsDatabase(DbContextOptions<ItemsDatabase> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasMany(x => x.ChildItems)
            .WithOne(x => x.Parent) 
            .HasForeignKey(x => x.ParentId) 
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Item> Items { get; set; }
}