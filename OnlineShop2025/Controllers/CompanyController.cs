using Asp2025_DataAccess.Data;
using Asp2025_Models;
using Asp2025_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Utility;

using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_DataAccess.Repository;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CompanyController : Controller
    {

        private ICompanyRepository companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }


        // GET: CompanyController
        public ActionResult Index()
        {
            return View(companyRepository.GetAll());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string name)
        {

            Company company1 = new Company()
            {
                Name = name
            };
            companyRepository.Add(company1);
            companyRepository.Save();
            TempData[WC.Success] = "Company created successfully";
            return RedirectToAction("Index", companyRepository.GetAll());
        }

        public IActionResult Edit(int id)
        {
            var comp = companyRepository.Find(id);
            
            return View(comp);
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                companyRepository.Update(company);
                companyRepository.Save();
                TempData[WC.Success] = "Company created successfully";
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public IActionResult Delete(int id)
        {
            var comp = companyRepository.Find(id);
            companyRepository.Remove(comp);
            companyRepository.Save();
            TempData[WC.Success] = "Company deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
