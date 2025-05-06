using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Models;
using SchoolApp.DAL;
using SchoolApp.BL.Services.IServices;
using SchoolApp.BL.Services;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.Repositories;
using SchoolApp.DAL.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;

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
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SchoolApp API",
        Version = "v1",
        Description = "توثيق API لإدارة بيانات المدرسة"
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "National API v1");
        options.RoutePrefix = string.Empty; // فتح Swagger مباشرة عند تشغيل التطبيق
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApp API v1");
        options.RoutePrefix = string.Empty; // This makes Swagger accessible at '/'
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();