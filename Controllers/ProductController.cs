using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {

        private ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        public IActionResult Add(int id)
        {
            ViewBag.CompId = id;
            var category = context.Category.ToList();
            ViewBag.CategoryChoose = new SelectList(category, "Id", "Name");

            return View(new Product());
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {

            Product productnew = new Product()
            {
                Name = product.Name,
                Image = product.Image,
                CategoryId = product.CategoryId,
                CompanyId = product.CompanyId,
                Description = product.Description,
                Price = product.Price
            };
            context.Products.Add(productnew);
            context.SaveChanges();
            return RedirectToAction("Index", context.Products.ToList());
        }

        public IActionResult Edit(int id)
        {
            var comp = context.Products.Find(id);
            return View(comp);
        }
        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            if (ModelState.IsValid)
            {
                var comp = context.Products.Find(prod.Id);
                comp.Name = prod.Name;
                context.SaveChangesAsync();
                return RedirectToAction("Index","");
            }
            return View(prod);
        }

        public IActionResult Delete(int id)
        {
            var prod = context.Products.Find(id);
            context.Products.Remove(prod);
            context.SaveChangesAsync();
            return RedirectToAction("Index", "Company");
        }



    }
}
