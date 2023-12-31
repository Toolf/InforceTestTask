using Microsoft.EntityFrameworkCore;
using webapi.Database.Model;

namespace webapi.Database;

public class Db : DbContext
{
    public Db(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ShortUrlModel> ShortUrl { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortUrlModel>()
            .HasIndex(e => e.ShortUrl)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(e => e.Username)
            .IsUnique();
    }
}