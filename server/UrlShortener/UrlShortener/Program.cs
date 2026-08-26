using Microsoft.EntityFrameworkCore;
using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LinkContext>(opts => {
    opts.UseSqlite($"Data Source={Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "linkshortener.db")}");
});

#region DI
builder.Services.AddScoped<ILinkService, LinkService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();