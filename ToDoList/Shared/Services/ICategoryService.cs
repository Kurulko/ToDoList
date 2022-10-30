using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(string userId);
        Task<Category> GetCategoryAsync(string userId, long categoryId);
        Task DeleteCategoryAsync(string userId, long categoryId);
        Task AddCategoryAsync(string userId, Category category);
        Task UpdateCategoryAsync(string userId, Category category);
    }
}
