using BulkyBook.Business;
using BulkyBook.Business.Service.IService;
using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)]
    public class CategoryController : Controller
    {

        private readonly ICatagoryService _catagoryService;

        public CategoryController(ICatagoryService catagoryService)
        {
            _catagoryService = catagoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _catagoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && !await _catagoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
            {
                ModelState.AddModelError("", "Category already exists");
            }

            if (ModelState.IsValid)
            {
                await _catagoryService.CreateCategoryAsync(category);
               
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = await _catagoryService.GetCategoryByIdAsync(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task <IActionResult> UpdatePOST(Category category)
        {

            if (!String.IsNullOrEmpty(category.Name) && !await _catagoryService.IsCategoryNameUniqueAsync(category.Name,category.Id))
            {
                ModelState.AddModelError("", "Category already exists");
            }

            if (ModelState.IsValid)
            {
                await _catagoryService.UpdateCategoryAsync(category);

                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = await _catagoryService.GetCategoryByIdAsync(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int Id)
        {
            var category = await _catagoryService.GetCategoryByIdAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            await _catagoryService.DeleteCategoryAsync(Id);

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
