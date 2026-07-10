using BulkyBook.Business;
using BulkyBook.Business.Service.IService;
using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICatagoryService _catagoryService;

        public ProductController(IProductService productService, ICatagoryService catagoryService)
        {
            _productService = productService;
            _catagoryService = catagoryService;

        }
        public async Task<IActionResult> Index()
        {
                 return View();
        }
       
        public async Task<IActionResult> Upsert()
        {
            var categories = await _catagoryService.GetAllCategoriesAsync();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
           
                      return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPOST(Product product,IFormFile? file)
        {


            if (ModelState.IsValid)
            {
              //  await  _productService.CreateProductAsync(product);
               
                TempData["success"] = "Product created successfully";
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
            var category = await  _productService.GetProductByIdAsync(Id.Value);
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
            var product = await  _productService.GetProductByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            await  _productService.DeleteProductAsync(Id);

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

      
        #region API Calls
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }
        #endregion
    }
}
