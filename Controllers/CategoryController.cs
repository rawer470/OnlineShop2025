using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{
    [Authorize(Roles =WC.AdminRole)]
    public class CategoryController : Controller
    {
        private ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            IEnumerable<Category> categories = context.Category;

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            context.Category.Add(category);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = context.Category.FirstOrDefault(n => n.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            context.Category.Update(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = context.Category.FirstOrDefault(n => n.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = context.Category.FirstOrDefault(n => n.Id == id);
            context.Category.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
