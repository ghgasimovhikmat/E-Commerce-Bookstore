using BulkyBook.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Business.Service.IService
{
    public interface ICatagoryService
    {
        
        Task<Category?> GetCategoryByIdAsync(int id);
        Task <IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task  UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

        Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null);
    }
}
