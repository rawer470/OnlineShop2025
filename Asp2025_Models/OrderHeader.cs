using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp2025_Models;

public class OrderHeader
{
    [Key]
    public int Id { get; set; }

    public string CreateByUserId { get; set; }
    [ForeignKey("CreateByUserId")]
    public ApplicationUser CreateByUser { get; set; }

    [Required]
    public DateTime OrdereDate { get; set; }
    [Required]
    public DateTime ShippedDate { get; set; }
    [Required]
    public double FinalOrderTotal { get; set; }
    public string OrderStatus { get; set; }

    public DateTime? PaymentDate { get; set; }


    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string FullName { get; set; }
}
