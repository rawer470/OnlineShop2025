using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;


var builder = WebApplication.CreateBuilder(args);

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
app.UseSession();
app.UseRouting();
app.MapRazorPages(); // For Razor PAge
app.UseAuthorization();
app.UseAuthentication(); //Для identity
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=Index}/");

app.Run();
