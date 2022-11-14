using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync(string userId);
    Task<Category> GetCategoryAsync(string userId, long categoryId);
    Task DeleteCategoryAsync(string userId, long categoryId);
    Task AddCategoryAsync(ModelWithUserId<Category> model);
    Task UpdateCategoryAsync(ModelWithUserId<Category> model);
}
