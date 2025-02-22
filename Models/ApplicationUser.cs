using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}
