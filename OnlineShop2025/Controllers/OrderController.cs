using Asp2025_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private IInquiryHeaderRepository headerRepository;
        private IInquiryDetailRepository detailRepository;

        public OrderController(IInquiryHeaderRepository headerRepo, IInquiryDetailRepository detailRepo)
        {
            headerRepository = headerRepo;
            detailRepository = detailRepo;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

    }
}
