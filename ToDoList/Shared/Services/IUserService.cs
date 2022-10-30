using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
