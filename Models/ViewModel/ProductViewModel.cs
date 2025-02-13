using System;

namespace OnlineShop.Models.ViewModel;

public class ProductViewModel
{
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public IFormFile Image{get;set;}
    public int CategoryId { get; set; }
    public int CompanyId{get;set;}
}
