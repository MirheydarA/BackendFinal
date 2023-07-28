using Business.Services.Abstract.Admin;
using Business.Services.Abstract.User;
using Business.Services.Concrete.Admin;
using Business.Services.Concrete.User;
using Business.Services.Utilities;
using Business.Services.Utilities.Abstract;
using Business.Services.Utilities.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.User;
using DataAccess.Repositories.Abstract.Userr;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.Repositories.Concrete.User;
using DataAccess.Repositories.Concrete.Userr;
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


/// Admin Repositories ////////
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IOurVisionComponentRepository, OurVisionComponentRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAbuotUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentComponentRepository, DepartmentComponentRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IDutyRepository, DutyRepository>();
builder.Services.AddScoped <DataAccess.Repositories.Abstract.Admin.IDoctorRepository, DataAccess.Repositories.Concrete.Admin.DoctorRepository>();
builder.Services.AddScoped<IWhyChooseRepository, WhyChooseRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Admin.IFAQCategoryRepository, DataAccess.Repositories.Concrete.Admin.FAQCategoryRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Admin.IProductRepository, DataAccess.Repositories.Concrete.Admin.ProductRepository>();



//// User Repositories ////////
builder.Services.AddScoped<IFAQRepository, FAQRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.User.IDoctorRepository, DataAccess.Repositories.Concrete.User.DoctorRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Userr.IProductRepository, DataAccess.Repositories.Concrete.Userr.ProductRepository>();


/// Admin Services ////////
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IOurVisionComponentService, OurVisionComponentService>();
builder.Services.AddScoped<IOurVisionService, OurVisionService>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IAccountService, Business.Services.Concrete.Admin.AccountService>();
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IDepartmentComponentService, DepartmentComponentService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IDutyService, DutyService>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IDoctorService, Business.Services.Concrete.Admin.DoctorService>();
builder.Services.AddScoped<IWhyChooseService, WhyChooseService>();
builder.Services.AddScoped<IFAQCategoryService, FAQCategoryService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IProductService, Business.Services.Concrete.Admin.ProductService>();


/// User Services ////////
builder.Services.AddScoped<Business.Services.Abstract.User.IAccountService, Business.Services.Concrete.User.AccountService>();
builder.Services.AddScoped<IFAQService, FAQService>();
builder.Services.AddScoped<Business.Services.Abstract.User.IDoctorService, Business.Services.Concrete.User.DoctorService>();
builder.Services.AddScoped<Business.Services.Abstract.User.IProductService, Business.Services.Concrete.Userr.ProductService>();




builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPaginator, Paginator>();


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
    var visionRepository = scope.ServiceProvider.GetService<IOurVisionRepository>();
    var departmentRepository = scope.ServiceProvider.GetService<IDepartmentRepository>();
    var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
    await DbInitializer.SeedAsync(roloManager, userManager,visionRepository,departmentRepository ,unitOfWork);
}

app.Run();
