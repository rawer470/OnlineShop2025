using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public List<Product> Products { get; set; }

}
