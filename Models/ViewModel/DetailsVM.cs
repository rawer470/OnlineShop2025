using System;

namespace OnlineShop.Models.ViewModel;

public class DetailsVM
{
    public Product Product { get; set; }
    public bool ExistsInCart { get; set; }
}
