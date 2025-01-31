using System;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Category {get; set;}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options) {}
}
