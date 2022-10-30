using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;

namespace ToDoList.Server.Services
{
    public class UsersManager : IUserService
    {
        readonly ToDoListContext db;
        public UsersManager(ToDoListContext db)
            => this.db = db;


        public async Task<User> GetUserAsync(string userId)
        {
            try
            {
                return await db.Users.FirstAsync(u => u.Id == userId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await db.Users.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
