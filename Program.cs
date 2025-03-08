using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Utility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // добавляем сервисы сессии
builder.Services.AddHttpContextAccessor(); //для сессий
builder.Services.AddTransient<IEmailSender, EmailSender>();
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
