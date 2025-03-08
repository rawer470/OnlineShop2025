using System;
using Microsoft.Build.Framework;

namespace OnlineShop.Utility;

public static class WC
{
    public static string ImagePath = Path.Combine("images", "product");
    public const string SessionCart = "ShoppingCartSesssion";

    public const string AdminRole = "Admin";
    public const string CustomerRole = "Customer";
}
