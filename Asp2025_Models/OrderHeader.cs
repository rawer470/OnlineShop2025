using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp2025_Models;

public class OrderHeader
{

    [Key]
    public int Id { get; set; }

    // кто из админов создал эту запись
    public string CreatedByUserId { get; set; }
    [ForeignKey("CreatedByUserId")]
    public ApplicationUser CreatedBy { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime ShippingDate { get; set; }
    [Required]
    public double FinalOrderTotal { get; set; }

    public string OrderStatus { get; set; }

    // дата оплаты
    public DateTime PaymentDate { get; set; }


    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string FullName { get; set; }
    public string Email { get; set; }
}
