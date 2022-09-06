using PieShop.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:PieShopDbContextConnectionString"]);
});
var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDeveloperExceptionPage();

app.MapDefaultControllerRoute();

DbInitializer.Seed(app);

app.Run();
