using System;
using System.Net.NetworkInformation;

namespace OnlineShop.Models.ViewModel;

public class ProductFilterVM
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
