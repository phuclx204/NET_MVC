using System.Text;
using BaseBusiness.util;
using Catalog.Controllers;
using Catalog.Services.Interfaces;
using CSM.Controllers;
using CSM.Services.Interfaces;
using Inventory.Controllers;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Modules.Catalog.Services;
using Modules.CSM.Services;
using Modules.Inventory.Services;
using Modules.SystemApp.Services;
using SystemApp.Controllers;
using SystemApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<DBUtils>(sp => new DBUtils(connString));


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
    .AddApplicationPart(typeof(AuthController).Assembly)
    ;

builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IStockInService, StockInService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IAccountService, AccountService>();


// --- Cấu hình JWT ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/auth/login");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/home"));
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();
