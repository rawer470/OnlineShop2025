using System;
using System.Net.NetworkInformation;

namespace Asp2025_Models.ViewModel;

public class ProductFilterVM
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
