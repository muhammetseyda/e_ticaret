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


// MongoDbContext'yi yap�land�rma b�l�m�
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

    //options.Password.RequireDigit = true; //�ifre Say�sal karakteri desteklesin mi?
    options.Password.RequiredLength = 6;  //�ifre minumum karakter say�s�
    options.Password.RequireLowercase = true; //�ifre k���k harf olabilir
    options.Password.RequireLowercase = true; //�ifre b�y�k harf olabilir
    options.Password.RequireNonAlphanumeric = false; //Sembol bulunabilir

    options.Lockout.MaxFailedAccessAttempts = 5; //Kullan�c� ka� ba�ar�s�z giri�ten sonra sisteme giri� yapamas�n
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Ba�ar�s�z giri� i�lemlerinden sonra ne kadar s�re sonra sisteme giri� hakk� tan�ns�n
    options.Lockout.AllowedForNewUsers = true; //Yeni �yeler i�in kilit sistemi ge�erli olsun mu

    options.User.RequireUniqueEmail = true; //Kullan�c� benzersiz e-mail adresine sahip olsun

    options.SignIn.RequireConfirmedEmail = false; //Kay�t i�lemleri i�in email onaylamas� zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon onay� olsun mu?
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
