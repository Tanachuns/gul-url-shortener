using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Databases;

public class LinkContext:DbContext
{
    public DbSet<LinkModel> Links { get; set; }

    public string DbPath { get; }

    public LinkContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "linkshortener.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}