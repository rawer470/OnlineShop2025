using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp2025_Models;

public class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderHeaderId { get; set; }
    [ForeignKey("OrderHeaderId")]
    public OrderHeader OrderHeader { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    public double PricePer { get; set; }
    public int Count { get; set; }

}
