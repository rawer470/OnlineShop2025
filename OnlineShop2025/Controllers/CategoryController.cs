using Asp2025_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp2025_Models;
using OnlineShop.Utility;
using Asp2025_DataAccess.Data;

using Asp2025_DataAccess.Repository.IRepository; //Нужно подключить пространство имен, чтобы использовать интерфейс IRepository

namespace OnlineShop.Controllers
{
    [Authorize(Roles =WC.AdminRole)]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
           this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<Asp2025_Models.Category> categories = categoryRepository.GetAll();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepository.Add(category);
            categoryRepository.Save(); //Сохраняем изменения в базе данных
            TempData[WC.Success] = "Category created successfully"; //Успешное сообщение

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = categoryRepository.Find(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            categoryRepository.Update(category); //Обновляем категорию
            categoryRepository.Save(); //Сохраняем изменения в базе данных
           TempData[WC.Success] = "Category updated successfully"; //Успешное сообщение
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = categoryRepository.FirstOrDefault(n => n.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.FirstOrDefault(n => n.Id == id);
            categoryRepository.Remove(category);
            categoryRepository.Save(); //Сохраняем изменения в базе данных
            TempData[WC.Success] = "Category deleted successfully"; //Успешное сообщение
            return RedirectToAction("Index");
        }
    }
}
