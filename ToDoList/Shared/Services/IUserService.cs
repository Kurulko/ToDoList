using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Shared.Services;

public interface IUserService
{
    Task<User> GetUserAsync(string userId);
    Task<IEnumerable<User>> GetUsersAsync();
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(string userId);
    Task CreateUserAsync(User user);
    Task ChangeUserPasswordAsync(ModelWithUserId<ChangePassword> model);
    Task AddUserPasswordAsync(ModelWithUserId<ChangePassword> model);
    Task<bool> HasUserPasswordAsync(string userId);
    Task<IEnumerable<string>> GetRolesAsync(string userId);
    Task AddRoleToUserAsync(ModelWithUserId<string> model);
    Task DeleteRoleFromUserAsync(string userId, string roleName);
}
