using Microsoft.EntityFrameworkCore;
using UrlShortener.Databases;
using UrlShortener.Interfaces;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("sqlite");
   // $"Data Source={Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "linkshortener.db")}";

builder.Services.AddDbContext<LinkContext>(opts => {
    opts.UseSqlite(connectionString);
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
        pattern: "{controller=Redirect}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();