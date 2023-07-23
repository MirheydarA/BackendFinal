using Business.Services.Abstract;
using Business.Services.Abstract.Admin;
using Business.Services.Concrete;
using Business.Services.Concrete.Admin;
using Business.Services.Utilities;
using Business.Services.Utilities.Abstract;
using Business.Services.Utilities.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddSingleton<IFileService, FileService>();


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
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IOurVisionComponentRepository, OurVisionComponentRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();



builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IOurVisionComponentService, OurVisionComponentService>();
builder.Services.AddScoped<IOurVisionService, OurVisionService>();
builder.Services.AddScoped<Business.Services.Abstract.IAccountService, Business.Services.Concrete.AccountService>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IAccountService, Business.Services.Concrete.Admin.AccountService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.HttpContext.Request.Path.Value.StartsWith("/admin") || context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/admin/account/login" + redirectPath.Query);
        }
        else
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/account/login" + redirectPath.Query);
        }
        return Task.CompletedTask;
    };

});


var app = builder.Build();   

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
    );
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roloManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    await DbInitializer.SeedAsync(roloManager, userManager);
}

app.Run();
