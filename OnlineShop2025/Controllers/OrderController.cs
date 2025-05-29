using Asp2025_DataAccess.Repository;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;
using Asp2025_Models.ViewModel;
using Asp2025_Utility;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderHeaderRepository orderHeaderRepository;
        private readonly IInquiryDetailRepository detailRepository;
        private readonly IInquiryHeaderRepository inquiryHeaderRepository;
        public OrderController(IOrderDetailRepository orderDetailRepository, IOrderHeaderRepository orderHeaderRepository,
            IInquiryDetailRepository detailRepository, IInquiryHeaderRepository inquiryHeaderRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.orderHeaderRepository = orderHeaderRepository;
            this.detailRepository = detailRepository;
            this.inquiryHeaderRepository = inquiryHeaderRepository;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrderUser()
        {
            if (User.IsInRole(WC.AdminRole))
            {
                return GetOrderList();
            }
            var inquiryList = orderHeaderRepository.GetAll(filter: x => x.CreatedByUserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
).Select(x => new
{
    Id = x.Id,
    OrderDate = x.OrderDate.Date.ToString("dd/MM/yyyy"),
    OrderStatus = x.OrderStatus,
    FinalOrderTotal = x.FinalOrderTotal,
});
            if (inquiryList == null || !inquiryList.Any())
            {
                return NotFound(new { message = "No orders found for this user." });
            }
            return Json(new { data = inquiryList });
        }
        [HttpGet]
        private IActionResult GetOrderList()
        {
            var orderList = orderHeaderRepository.GetAll();
            return Json(new { data = orderList });
        }

        public IActionResult AddOrder(InquiryVM inquiryVM)
        {
            inquiryVM.InquiryDetails = detailRepository.GetAll(x => x.InquiryHeaderId == inquiryVM.InquiryHeader.Id, includeProperties: "Product");
            var productList = inquiryVM.InquiryDetails.Select(x => x.Product).ToList();
            var totalPrice = productList.Sum(x => x.Price * x.TempCount);

            OrderHeader orderHeader = new OrderHeader
            {
                CreatedByUserId = inquiryVM.InquiryHeader.ApplicationUserId,
                OrderDate = DateTime.Now,
                ShippingDate = DateTime.Now.AddDays(7), // Assuming shipping is 7 days after order
                FinalOrderTotal = totalPrice,
                OrderStatus = "Pending",
                PaymentDate = DateTime.Now, // Assuming payment is made now
                PhoneNumber = inquiryVM.InquiryHeader.PhoneNumber,
                FullName = inquiryVM.InquiryHeader.FullName,
                Email = inquiryVM.InquiryHeader.Email
            };
            orderHeaderRepository.Add(orderHeader);
            orderHeaderRepository.Save();
            foreach (var item in inquiryVM.InquiryDetails)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderHeaderId = orderHeader.Id, // Assuming you will save the order header first
                    ProductId = item.ProductId,
                    Count = item.Count,
                };
                orderDetailRepository.Add(orderDetail);
                orderDetailRepository.Save();
            }
            inquiryHeaderRepository.Remove(inquiryVM.InquiryHeader);
            detailRepository.RemoveRange(inquiryVM.InquiryDetails);
            inquiryHeaderRepository.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            OrderHeader header = orderHeaderRepository.FirstOrDefault(x => x.Id == id);
            if (header == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = header,
                OrderDetails = orderDetailRepository.GetAll(x => x.OrderHeaderId == id, includeProperties: "Product")
            };
            return View(orderVM);
        }

        public IActionResult ChangeStatusOrder(int orderId)
        {
            var order = orderHeaderRepository.FirstOrDefault(x => x.Id == orderId);
            if (order.OrderStatus == WC.StatusCompleted)
            {
                DeleteOrder(orderId);
                return RedirectToAction("Index");
            }
            var statusIndex = WC.StatusList.ToList().IndexOf(order.OrderStatus);
            order.OrderStatus = WC.StatusList[statusIndex + 1];
            orderHeaderRepository.Update(order);
            orderHeaderRepository.Save();

            return RedirectToAction("Details", new { id = orderId });

        }

        private void DeleteOrder(int id)
        {
            OrderHeader orderHeader = orderHeaderRepository.FirstOrDefault(x => x.Id == id);
            IEnumerable<OrderDetail> orderDetails = orderDetailRepository.GetAll(x => x.OrderHeaderId == id);

            orderDetailRepository.RemoveRange(orderDetails);
            orderHeaderRepository.Remove(orderHeader);
            orderHeaderRepository.Save();
        }
    }
}
