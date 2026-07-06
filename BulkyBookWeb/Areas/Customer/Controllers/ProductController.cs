using BulkyBook.Business;
using BulkyBook.Business.Service.IService;
using BulkyBook.Data;
using BulkyBook.Models;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(Product product)
        {


            if (ModelState.IsValid)
            {
                await  _productService.CreateProductAsync(product);
               
                TempData["success"] = "Product created successfully";
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
            var category = await  _productService.GetProductByIdAsync(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task <IActionResult> UpdatePOST(Product product)
        {


            if (ModelState.IsValid)
            {
                await  _productService.UpdateProductAsync(product);

                TempData["success"] = "Productupdated successfully";
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

    }
}
