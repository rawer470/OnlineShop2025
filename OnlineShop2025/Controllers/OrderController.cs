using System.Security.Claims;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;
using Asp2025_Models.ViewModel;
using Asp2025_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderHeaderRepository orderHeaderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IInquiryDetailRepository inquiryDetailRepository;
        private IInquiryHeaderRepository inquiryHeaderRepository;
        public OrderController(IOrderHeaderRepository orderHeaderRepository, IOrderDetailRepository orderDetailRepository, IInquiryDetailRepository inquiryDetailRepository, IInquiryHeaderRepository inquiryHeaderRepository)
        {
            this.orderHeaderRepository = orderHeaderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.inquiryDetailRepository = inquiryDetailRepository;
            this.inquiryHeaderRepository = inquiryHeaderRepository;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrderListUser()
        {
            if (User.IsInRole(WC.AdminRole))
            {
                return RedirectToAction("GetOrderList");
            }
            var inquiryList = orderHeaderRepository.GetAll(filter: x => x.CreateByUserId == User.FindFirst(ClaimTypes.NameIdentifier).Value).Select(x => new
            {
                Id = x.Id,
                OrderDate = x.OrdereDate.ToString("dd.MM.yyyy"),
                OrderStatus = x.OrderStatus,
                FinalOrderTotal = x.FinalOrderTotal,
            });

            if (inquiryList == null)
            {
                return NotFound();
            }
            return Json(new { data = inquiryList });
        }
        [HttpGet]
        public IActionResult GetOrderList()
        {
            var orderList = orderHeaderRepository.GetAll().Select(x => new
            {
                Id = x.Id,
                OrderDate = x.OrdereDate.ToString("dd.MM.yyyy"),
                OrderStatus = x.OrderStatus,
                FinalOrderTotal = x.FinalOrderTotal,
            });
            return Json(new { data = orderList });
        }
        public IActionResult AddOrder(InquiryVM inquiryVM)
        {
            inquiryVM.InquiryDetails = inquiryDetailRepository.GetAll(x => x.InquiryHeaderId == inquiryVM.InquiryHeader.Id, includeProperties: "Product");
            var products = inquiryVM.InquiryDetails.Select(x => x.Product).ToList();
            var total = products.Sum(x => x.Price * x.TempCount);

            OrderHeader orderHeader = new OrderHeader()
            {
                CreateByUserId = inquiryVM.InquiryHeader.ApplicationUserId,
                OrdereDate = DateTime.Now,
                ShippedDate = DateTime.Now.AddDays(7),
                FinalOrderTotal = total,
                OrderStatus = WC.StatusApproved,
                PaymentDate = DateTime.Now,
                PhoneNumber = inquiryVM.InquiryHeader.PhoneNumber,
                Email = inquiryVM.InquiryHeader.Email,
                FullName = inquiryVM.InquiryHeader.FullName,
            };

            orderHeaderRepository.Add(orderHeader);
            orderHeaderRepository.Save();
            foreach (var item in inquiryVM.InquiryDetails)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderHeaderId = orderHeader.Id,
                    ProductId = item.ProductId,
                    Count = item.Count,
                    PricePer = item.Product.Price
                };
                orderDetailRepository.Add(orderDetail);
                orderDetailRepository.Save();
            }
            inquiryDetailRepository.RemoveRange(inquiryVM.InquiryDetails);
            inquiryHeaderRepository.Remove(inquiryVM.InquiryHeader);
            inquiryHeaderRepository.Save();
            return RedirectToAction("Index");
        }



        public IActionResult Details(int id)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = orderHeaderRepository.FirstOrDefault(x => x.Id == id),
                OrderDetails = orderDetailRepository.GetAll(x => x.OrderHeaderId == id, includeProperties: "Product")
            };
            return View(orderVM);
        }

        public IActionResult ChangeStatusOrder(int orderId)
        {
            var order = orderHeaderRepository.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            if (order.OrderStatus == WC.StatusCompleted)
            {
                DeleteOrder(order);
                return RedirectToAction("Index");
            }

            var statusIndex = Array.IndexOf(WC.OrderStatusList, order.OrderStatus);
            order.OrderStatus = WC.OrderStatusList[statusIndex + 1];
            orderHeaderRepository.Update(order);
            orderHeaderRepository.Save();

            return RedirectToAction("Details", new { id = orderId });
        }

        private void DeleteOrder(OrderHeader order)
        {
            IEnumerable<OrderDetail> orderDetails = orderDetailRepository.GetAll(x => x.OrderHeaderId == order.Id);
            orderDetailRepository.RemoveRange(orderDetails);
            orderHeaderRepository.Remove(order);
            orderHeaderRepository.Save();
            TempData[WC.Error] = "Order deleted successfully";


        }
    }
}
