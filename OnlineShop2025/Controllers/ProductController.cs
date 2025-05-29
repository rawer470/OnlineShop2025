using System.Reflection;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;
using Asp2025_Models.ViewModel;
using Asp2025_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private ICompanyRepository companyRepository;
        private IWebHostEnvironment webHostEnvironment;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepository,
         IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.categoryRepository = categoryRepository;

        }
        // GET: ProductController
        public ActionResult Index()
        {


            //  var a = context.Products.ToList();
            ProductFilterVM productFilter = new ProductFilterVM()
            {
                Products = productRepository.GetAll(includeProperties: "Category,Company"),
                Categories = categoryRepository.GetAll(),
            };
            return View(productFilter);
        }

        public IActionResult Add(int id)
        {
            ViewBag.CompId = id;
            var category = categoryRepository.GetAll();
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
            productRepository.Add(productnew);
            productRepository.Save();
            TempData[WC.Success] = "Product created successfully";
            return RedirectToAction("Index", productRepository.GetAll(includeProperties: "Category,Company"));
        }

        public IActionResult Edit(int id)
        {
            var product = productRepository.Find(id);
            var category = categoryRepository.GetAll();
            var company = companyRepository.GetAll();
            ViewBag.CategoryChoose = new SelectList(category, "Id", "Name");
            ViewBag.CompanyChoose = new SelectList(company, "Id", "Name");
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {

            productRepository.Update(product);
            productRepository.Save();
            TempData[WC.Success] = "Product updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var prod = productRepository.Find(id);
            productRepository.Remove(prod);
            productRepository.Save();
            TempData[WC.Success] = "Product deleted successfully";
            return RedirectToAction("Index", "Company");
        }


        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
           HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            DetailsVM detailsVM = new DetailsVM()
            {
                Product = productRepository.FirstOrDefault(u => u.Id == id, includeProperties: "Category,Company"),
                ExistsInCart = false
            };


            foreach (var item in shoppingCarts)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                    detailsVM.Product.TempCount = item.Count;
                }
            }
            return View(detailsVM);
        }

        [HttpPost]
        public IActionResult DetailsPost(int id, int tempCount)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
           HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCarts.Add(new ShoppingCart() { ProductId = id, Count = tempCount });
            HttpContext.Session.Set(WC.SessionCart, shoppingCarts);
            TempData[WC.Success] = "Product added to cart successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
           HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = shoppingCarts.SingleOrDefault(r => r.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCarts.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCarts);
            TempData[WC.Success] = "Product removed from cart successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
