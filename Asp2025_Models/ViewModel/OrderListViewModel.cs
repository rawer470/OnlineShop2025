using System;

namespace Asp2025_Models.ViewModel;

public class OrderListViewModel
{
    public IEnumerable<OrderHeader> OrderHeaderList { get; set; }
    public IEnumerable<OrderDetail> OrderDetailList { get; set; }

}
