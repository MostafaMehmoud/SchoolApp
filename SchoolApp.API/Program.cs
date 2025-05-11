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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SchoolApp.DAL.Repositories.IRepository;

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
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



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




// إعداد JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentNullException("JwtSettings:Secret", "Secret key cannot be null");
}

var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
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
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApp API v1");
        options.RoutePrefix = string.Empty; // فتح Swagger مباشرة عند تشغيل التطبيق
    });
}

app.UseCors("AllowAll");


app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
