using Asp2025_DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Utility;

//Для репозиториев
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //Регистрация репозитория категорий  
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); //Регистрация репозитория компаний  
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //Регистрация репозитория продуктов


builder.Services.AddScoped<IInquiryHeaderRepository, InquiryHeaderRepository>(); //Регистрация репозитория InquiryHeader
builder.Services.AddScoped<IInquiryDetailRepository, InquiryDetailRepository>(); //Регистрация репозитория InquiryDetail
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>(); //Регистрация репозитория OrderHeader
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>(); //Регистрация репозитория OrderDetail

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // добавляем сервисы сессии
builder.Services.AddHttpContextAccessor(); //для сессий
// builder.Services.AddDefaultIdentity<IdentityUser>().
// AddEntityFrameworkStores<ApplicationDbContext>();//Для identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>(); //Для полного Identity с Ролями


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();             // 1. Включаем маршрутизацию
app.UseAuthentication();      // 2. Аутентификация (куки, JWT и т. д.)
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.MapRazorPages(); // For Razor PAge
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/");

app.Run();
