using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Services
{
    public class ToDoItemManager : IToDoItemService
    {
        readonly ToDoListContext db;
        public ToDoItemManager(ToDoListContext db)
            => this.db = db;

        public async Task AddToDoItemAsync(string userId, ToDoItem item)
        {
            try
            {
                item.UserId = userId;
                db.ToDoItems.Add(item);
                db.SaveChanges();
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
                ToDoItem? item = await db.ToDoItems/*.Include(i => i.Category)*/.FirstOrDefaultAsync(i => i.UserId == userId && i.Id == toDoItemId);
                if (item is not null)
                {
                    //item.User = null;
                    //item.Category.ToDoItems = null;
                    //item.Category.User = null;
                    return item;
                }
                throw new ArgumentException($"There isn't such ToDoItem.Id = {toDoItemId} in the db");
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoItemsAsync(string userId)
        {
            try
            {
                return db.ToDoItems./*Include(i => i.Category).*/Where(i => i.UserId == userId).ToList();
                    //.Select(i =>
                    //{
                    //    i.User = null;
                    //    i.Category.ToDoItems = null;
                    //    i.Category.User = null;
                    //    return i;
                    //});
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateToDoItem(string userId, ToDoItem item)
        {
            try
            {
                item.UserId = userId;
                item.Category = db.Categories.First(c => c.Id == item.CategoryId && c.UserId == userId);
                db.ToDoItems.Update(item);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
