using PieShop.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:PieShopDbContextConnectionString"]);
});
var app = builder.Build();

app.UseStaticFiles();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDeveloperExceptionPage();

app.MapDefaultControllerRoute();

app.Run();
