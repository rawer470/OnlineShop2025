using Asp2025_DataAccess.Repository;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;
using Asp2025_Models.ViewModel;
using Asp2025_Utility;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class InquiryController : Controller
    {
        private IInquiryHeaderRepository headerRepository;
        private IInquiryDetailRepository detailRepository;

        [BindProperty]
        public InquiryVM inquiryVM { get; set; }

        public InquiryController(IInquiryHeaderRepository headerRepository, IInquiryDetailRepository detailRepository)
        {
            this.headerRepository = headerRepository;
            this.detailRepository = detailRepository;
        }
        // GET: InquiryController
        public ActionResult Index()
        {
            
            return View();
        }

        public IActionResult Details(int id)
        {
            InquiryVM inquiryVM = new InquiryVM()
            {
                InquiryHeader = headerRepository.FirstOrDefault(x => x.Id == id),
                InquiryDetails = detailRepository.GetAll(x => x.InquiryHeaderId == id, includeProperties: "Product")
            };
            return View(inquiryVM);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            InquiryHeader inquiryHeader = headerRepository.FirstOrDefault(x => x.Id == inquiryVM.InquiryHeader.Id);
            IEnumerable<InquiryDetail> inquiryDetails = detailRepository.GetAll(x => x.InquiryHeaderId == inquiryVM.InquiryHeader.Id);

            detailRepository.RemoveRange(inquiryDetails);
            headerRepository.Remove(inquiryHeader);
            headerRepository.Save();
            TempData[WC.Success] = "Inquiry deleted successfully";  
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetInquiryList()
        {
            var inquiryList = headerRepository.GetAll();
            return Json(new { data = inquiryList });
        }

    }
}
