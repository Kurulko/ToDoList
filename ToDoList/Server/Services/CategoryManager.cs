using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Services
{
    public class CategoryManager : ICategoryService
    {
        readonly ToDoListContext db;
        public CategoryManager(ToDoListContext db)
            => this.db = db;

        public async Task AddCategoryAsync(string userId, Category category)
        {
            try
            {
                category.UserId = userId;
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategoryAsync(string userId, long categoryId)
        {
            try
            {
                Category category = await db.Categories.FirstAsync(c => c.Id == categoryId && c.UserId == userId);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
        {
            try
            {
                return db.Categories.Where(c => c.UserId == userId).ToList();
                    //.Select(c =>
                    //{
                    //    c.User = null;
                    //    return c;
                    //}); ;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Category> GetCategoryAsync(string userId, long categoryId)
        {
            try
            {
                return await db.Categories.FirstAsync(c => c.UserId == userId && c.Id == categoryId);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCategoryAsync(string userId, Category category)
        {
            try
            {
                category.UserId = userId;
                db.Categories.Update(category);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
