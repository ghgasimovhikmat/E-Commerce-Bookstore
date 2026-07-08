using BulkyBook.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Business.Service.IService
{
    public interface IProductService
    {
        
        Task<Product?> GetProductByIdAsync(int id);
        Task <IEnumerable<Product>> GetAllProductsAsync(bool includeCategory = false);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

  
    }
}
