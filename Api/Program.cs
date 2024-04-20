using Data_MongoDb.Abstract;
using Data_MongoDb.Concrete;
using Data_MongoDb.MongoDbContext;
using Data_SSMS.Identity;
using Data_SSMS;
using Entities_MongoDb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Services_MongoDb.Abstract;
using Services_MongoDb.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Data_SSMS.Abstract;
using Data_SSMS.Concrete;
using Services_SSMS.Concrete;
using Services_SSMS.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
// IUserRepository ve UserRepository'yi kaydetmeye devam edin
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepositorySSMS, UserRepositorySSMS>();
builder.Services.AddScoped<IUserServicesSSMS, UserServicesSSMS>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<ICategoriesRepositoryMD, CategoriesRepositoryMD>();
builder.Services.AddScoped<ICategoriesServicesMD, CategoriesServicesMD>();
builder.Services.AddScoped<IProductRepositoryMD, ProductsRepositoryMC>();
builder.Services.AddScoped<IProductsServicesMD, ProductsServicesMD>();


// MongoDbContext'yi yapýlandýrma bölümü
 builder.Services.AddSingleton<MongoDbContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    var database = client.GetDatabase(settings.DatabaseName);
    return new MongoDbContext(database);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddRoles<AppIdentityRole>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{

    //options.Password.RequireDigit = true; //Þifre Sayýsal karakteri desteklesin mi?
    options.Password.RequiredLength = 6;  //Þifre minumum karakter sayýsý
    options.Password.RequireLowercase = true; //Þifre küçük harf olabilir
    options.Password.RequireLowercase = true; //Þifre büyük harf olabilir
    options.Password.RequireNonAlphanumeric = false; //Sembol bulunabilir

    options.Lockout.MaxFailedAccessAttempts = 5; //Kullanýcý kaç baþarýsýz giriþten sonra sisteme giriþ yapamasýn
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Baþarýsýz giriþ iþlemlerinden sonra ne kadar süre sonra sisteme giriþ hakký tanýnsýn
    options.Lockout.AllowedForNewUsers = true; //Yeni üyeler için kilit sistemi geçerli olsun mu

    options.User.RequireUniqueEmail = true; //Kullanýcý benzersiz e-mail adresine sahip olsun

    options.SignIn.RequireConfirmedEmail = false; //Kayýt iþlemleri için email onaylamasý zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon onayý olsun mu?
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
