using System;
using Asp2025_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //Using for Identity
using Microsoft.EntityFrameworkCore;
namespace Asp2025_DataAccess.Data;

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

  public DbSet<InquiryHeader> InquiryHeader { get; set; }
  public DbSet<InquiryDetail> InquiryDetail { get; set; }

  public DbSet<OrderHeader> OrderHeader { get; set; }
  public DbSet<OrderDetail> OrderDetail { get; set; }

  //  dotnet ef migrations add AddOrderModels -s OnlineShop2025 -p ASP2025_DataAccess

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
      base(options)
  { }
}
