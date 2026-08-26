using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Databases;

public class LinkContext(DbContextOptions<LinkContext> options) : DbContext(options)
{
    public DbSet<LinkModel> Links { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            // Index the short code for rapid O(1) lookups during redirect
            entity.HasIndex(e => e.Code)
                .IsUnique();
            entity.Property(e => e.LongUrl)
                .IsRequired();
        });
    }


}