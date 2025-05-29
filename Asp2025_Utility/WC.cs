using System;
namespace Asp2025_Utility;

public static class WC
{
    public static string ImagePath = Path.Combine("images", "product");
    public const string SessionCart = "ShoppingCartSesssion";

    public const string AdminRole = "Admin";
    public const string CustomerRole = "Customer";

    public const string Success = "Success";
    public const string Error = "Error";


    public const string StatusApproved = "Approved";// Это статус, когда заказ одобрен администратором
    public const string StatusInProcess = "InProcess";// Это статус, когда заказ находится в процессе обработки
    public const string StatusShipped = "Shipped";// Это статус, когда заказ отправлен на доставку
    public const string StatusCancelled = "Cancelled";//Это статус, когда заказ отменен клиентом или админом
    public const string StatusRefunded = "Refunded";//Это статус, когда деньги возвращены клиенту
    public const string StatusCompleted = "Completed";//Это статус, когда заказ успешно выполнен и доставлен клиенту

    public static string[] StatusList = {
        StatusApproved,
        StatusInProcess,
        StatusShipped,
        StatusRefunded,
         StatusCancelled,
         StatusCompleted
    };
}
