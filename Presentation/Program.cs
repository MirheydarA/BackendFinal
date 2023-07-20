using Business.Services.Abstract;
using Business.Services.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();



builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



builder.Services.AddScoped<ICategoryService, CategoryService>();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();   
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();
