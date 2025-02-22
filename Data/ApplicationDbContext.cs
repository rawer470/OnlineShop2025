using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data;

public class ApplicationDbContext : IdentityDbContext // ИЗМЕНИТЬ НАСЛЕДОВАНИЕ
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Company> Company { get; set; }

    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }
}
