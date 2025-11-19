using Catalog.Services.Interfaces;
using Modules.Catalog.Services;
using Catalog.Controllers;
using BaseBusiness.util;

var builder = WebApplication.CreateBuilder(args);

// 1. Đọc chuỗi kết nối từ appsettings.json của WebApp
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
DBUtils.ConnectionString = connString;


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(ColorController).Assembly);

builder.Services.AddScoped<IColorService,ColorService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
