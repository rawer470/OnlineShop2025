using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp2025_Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    [DisplayName("Category Type")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public int CompanyId{get;set;}
    [ForeignKey("CompanyId")]
    public Company Company{get;set;}

    [NotMapped]
    [Range(1, 1000, ErrorMessage = "Count must be between 1 and 1000")]
    public int TempCount { get; set; } = 1;
}
