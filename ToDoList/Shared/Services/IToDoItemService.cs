using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetToDoItemsAsync(string userId);
        Task<ToDoItem> GetToDoItemAsync(string userId, long toDoItemId);
        Task DeleteToDoItem(string userId, long toDoItemId);
        Task UpdateToDoItem(string userId, ToDoItem toDoItem);
        Task AddToDoItemAsync(string userId, ToDoItem toDoItem);
    }
}
