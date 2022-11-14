using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;

namespace ToDoList.Server.Services;

public class CategoryManager : ICategoryService
{
    readonly ToDoListContext db;
    public CategoryManager(ToDoListContext db)
        => this.db = db;

    public Task AddCategoryAsync(ModelWithUserId<Category> model)
    {
        try
        {
            Category category = model.Model;
            category.UserId = model.UserId;
            db.Categories.Add(category);
            db.SaveChanges();
            return Task.CompletedTask;
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

    public Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
    {
        try
        {
            return Task.FromResult(db.Categories.Where(c => c.UserId == userId).AsEnumerable());
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

    public Task UpdateCategoryAsync(ModelWithUserId<Category> model)
    {
        try
        {
            Category category = model.Model;
            category.UserId = model.UserId;
            db.Categories.Update(category);
            db.SaveChanges();
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }
}
