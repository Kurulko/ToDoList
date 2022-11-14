using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;

namespace ToDoList.Server.Services;

public class ToDoItemManager : IToDoItemService
{
    readonly ToDoListContext db;
    public ToDoItemManager(ToDoListContext db)
        => this.db = db;

    public Task AddToDoItemAsync(ModelWithUserId<ToDoItem> model)
    {
        try
        {
            ToDoItem item = model.Model;
            item.UserId = model.UserId;
            db.ToDoItems.Add(item);
            db.SaveChanges();
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteToDoItem(string userId, long toDoItemId)
    {
        try
        {
            ToDoItem item = await db.ToDoItems.FirstAsync(i => i.Id == toDoItemId && i.UserId == userId);
            db.ToDoItems.Remove(item);
            db.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public async Task<ToDoItem> GetToDoItemAsync(string userId, long toDoItemId)
    {
        try
        {
            return await db.ToDoItems.FirstAsync(i => i.UserId == userId && i.Id == toDoItemId);
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<ToDoItem>> GetToDoItemsAsync(string userId)
    {
        try
        {
            return Task.FromResult(db.ToDoItems.Where(i => i.UserId == userId).AsEnumerable());
        }
        catch
        {
            throw;
        }
    }

    public Task UpdateToDoItem(ModelWithUserId<ToDoItem> model)
    {
        try
        {
            string userId = model.UserId;
            ToDoItem item = model.Model;
            item.UserId = userId;
            item.Category = db.Categories.First(c => c.Id == item.CategoryId && c.UserId == userId);
            db.ToDoItems.Update(item);
            db.SaveChanges();
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }
}
