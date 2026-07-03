using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.Trim() == category.Name.Trim()))
            {
                ModelState.AddModelError("", "Category already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public IActionResult UpdatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.Trim() == category.Name.Trim() && c.Id != category.Id))
            {
                ModelState.AddModelError("", "Category already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
