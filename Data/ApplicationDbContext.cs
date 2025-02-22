using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //Using for Identity
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data;

public class ApplicationDbContext : IdentityDbContext //Наследование
{
    //Microsoft.AspNet.Identity.EntityFrameworkCore   Пакет для Identity
    //Microsoft.AspNetCore.Identity.UI   Для Razor pages
    //Microsoft.EntityFrameworkCore.Tools
    // dotnet aspnet-codegenerator identity -dc MyApplication.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout"
    //Команда для генерации кода
    public DbSet<Category> Category { get; set; }
    public DbSet<Company> Company { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }
}
