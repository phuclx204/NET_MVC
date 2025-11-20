using Modules.Catalog.Services;
using Modules.Inventory.Services;
using Modules.CSM.Services;
using Modules.SystemApp.Services;

using Catalog.Controllers;
using Inventory.Controllers;
using CSM.Controllers;
using SystemApp.Controllers;

using Catalog.Services.Interfaces;
using CSM.Services.Interfaces;
using Inventory.Services.Interfaces;
using SystemApp.Services.Interfaces;

using BaseBusiness.util;

var builder = WebApplication.CreateBuilder(args);

// 1. Đọc chuỗi kết nối từ appsettings.json của WebApp
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
DBUtils.ConnectionString = connString;


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(ColorController).Assembly)
    .AddApplicationPart(typeof(BrandController).Assembly)
    .AddApplicationPart(typeof(SizeController).Assembly)
    .AddApplicationPart(typeof(ProductController).Assembly)
    .AddApplicationPart(typeof(SupplierController).Assembly)
    .AddApplicationPart(typeof(StockInController).Assembly)
    .AddApplicationPart(typeof(CustomerController).Assembly)
    .AddApplicationPart(typeof(VoucherController).Assembly)
    .AddApplicationPart(typeof(EmployeeController).Assembly)
    ;

builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IStockInService, StockInService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/home"));
app.MapControllers();
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
