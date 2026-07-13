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

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService, ICatagoryService catagoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _catagoryService = catagoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
                 return View();
        }
       
        public async Task<IActionResult> Upsert(int? id)
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
            if(id==null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product= await _productService.GetProductByIdAsync(id.Value);
                return View(productVM);
            }
           
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPOST(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {


                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine("images", "products");
                    string finalPath = Path.Combine(wwwRootPath, productPath);


                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    //save the new image
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                   productVM.Product.ImageUrl = Path.Combine(@"\", productPath, fileName).Replace("\\", "/");
                  
                }
                if (productVM.Product.Id == null || productVM.Product.Id == 0)
                {
                    await _productService.CreateProductAsync(productVM.Product);
                }
                else
                {
                    await _productService.UpdateProductAsync(productVM.Product);

                }

                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _catagoryService.GetAllCategoriesAsync();
                productVM = new ProductVM()
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
