using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ✅ Register ApplicationDBContext in DI
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConStr")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;

    options.User.AllowedUserNameCharacters = null;
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    // السماح بأي كلمة سر
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 1; // أقل طول ممكن
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequiredUniqueChars = 0;

//    // للسماح بأي اسم مستخدم كمان (اختياري)
//    options.User.AllowedUserNameCharacters = null;
//    options.User.RequireUniqueEmail = false;
//})
//.AddEntityFrameworkStores<ApplicationDBContext>()

//    .AddDefaultTokenProviders();

// ✅ Register Repository and Unit of Work (if used)
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Register your BL service for National
builder.Services.AddScoped<IServiceNational, ServiceNational>();
builder.Services.AddScoped<IServiceFileBus, ServiceFileBus>();
builder.Services.AddScoped<IServiceStage, ServiceStage>();
builder.Services.AddScoped<IServiceClassTypeName, ServiceClassTypeName>();  
builder.Services.AddScoped<IServiceClassType, ServiceClassType>();  
builder.Services.AddScoped<IServiceInstallment, ServiceInstallment>();  
builder.Services.AddScoped<IServiceDepartment, ServiceDepartment>();  
builder.Services.AddScoped<IServiceStudent, ServiceStudent>();  
builder.Services.AddScoped<IServiceReceipt, ServiceReceipt>();  
builder.Services.AddScoped<IServicePayment, ServicePayment>();  
builder.Services.AddScoped<IServiceReport, ServiceReport>();  
builder.Services.AddScoped<IserviceAuth, ServiseAuth>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); // ضروري


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
