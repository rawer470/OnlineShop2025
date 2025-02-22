using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Models.ViewModel;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {

        private ApplicationDbContext context;
        private IWebHostEnvironment webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public ActionResult Index()
        {
           var a = User.Identity.IsAuthenticated;
            //  var a = context.Products.ToList();
            ProductFilterVM productFilter = new ProductFilterVM()
            {
                Products = context.Products.Include(x => x.Category).Include(x => x.Company),
                Categories = context.Category
            };
            return View(productFilter);
        }

        public IActionResult Add(int id)
        {
            ViewBag.CompId = id;
            var category = context.Category.ToList();
            ViewBag.CategoryChoose = new SelectList(category, "Id", "Name");

            return View(new ProductViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductViewModel product)
        {

            string webRootPath = webHostEnvironment.WebRootPath;
            string upload = Path.Combine(webRootPath, WC.ImagePath);
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(product.Image.FileName);
            string folder = Path.Combine(webRootPath, upload);
            string endPath = Path.Combine(folder, $"{fileName}{extension}");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            using (var fileStream = new FileStream(endPath, FileMode.Create))
            {
                // Запись файла сюда
                product.Image.CopyTo(fileStream);
            }

            Product productnew = new Product()
            {
                Name = product.Name,
                Image = Path.Combine(WC.ImagePath, $"{fileName}{extension}"),
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
            var product = context.Products.Find(id);
            var category = context.Category.ToList();
            var company = context.Company.ToList();
            ViewBag.CategoryChoose = new SelectList(category, "Id", "Name");
            ViewBag.CompanyChoose = new SelectList(company, "Id", "Name");
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {

            var comp = context.Products.Find(product.Id);
            comp.Name = product.Name;
            comp.Image = product.Image;
            comp.CategoryId = product.CategoryId;
            comp.CompanyId = product.CompanyId;
            comp.Description = product.Description;
            comp.Price = product.Price;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var prod = context.Products.Find(id);
            context.Products.Remove(prod);
            context.SaveChangesAsync();
            return RedirectToAction("Index", "Company");
        }

        public IActionResult Details(int id) //ProductId
        {

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
            HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Получаем лист из сессии если он существует
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            DetailsVM details = new DetailsVM()
            {
                Product = context.Products.Include(u => u.Category).Include(u => u.Company).Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };

            //Получаем элементы из корзины
            foreach (var item in shoppingCarts)
            {
                if (item.ProductId == id) // Если товар уже есть в корзине
                {
                    details.ExistsInCart = true;
                }
            }

            return View(details);
        }
        [HttpPost]
        public IActionResult DetailsPost(int id) //ProductId
        {
            //Создаем корзину для просмотра сессии с корзиной
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
            HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Сессия существует
                //Берем из сессии корзину
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            //Добавляем продукт в корзину
            shoppingCarts.Add(new ShoppingCart() { ProductId = id });

            //Устанавливаем сессию
            HttpContext.Session.Set(WC.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
            HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Сессия существует
                //Берем из сессии корзину
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = shoppingCarts.SingleOrDefault(r => r.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCarts.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }

    }
}
