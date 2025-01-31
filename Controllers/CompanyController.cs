using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CompanyController : Controller
    {

        private ApplicationDbContext context;

        public CompanyController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: CompanyController
        public ActionResult Index()
        {
            return View(context.Company.ToList());
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
            context.Company.Add(company1);
            context.SaveChanges();
            return RedirectToAction("Index", context.Company.ToList());
        }

        public IActionResult Edit(int id)
        {
            var comp = context.Company.Find(id);
            return View(comp);
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                var comp = context.Company.Find(company.Id);
                comp.Name = company.Name;
                context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public IActionResult Delete(int id)
        {
            var comp = context.Company.Find(id);
            context.Company.Remove(comp);
            context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
