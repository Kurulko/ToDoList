using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models;

namespace ToDoList.Server.Services;

public class UsersManager : IUserService
{
    readonly UserManager<User> userManager;
    readonly ToDoListContext db;
    public UsersManager(UserManager<User> userManager, ToDoListContext db)
        => (this.userManager, this.db) = (userManager, db);
    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
    {
        try
        {
            string roleName = model.Model;
            User user = await userManager.FindByIdAsync(model.UserId);
            IdentityResult res = await userManager.AddToRoleAsync(user, roleName);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task AddUserPasswordAsync(ModelWithUserId<ChangePassword> model)
    {
        try
        {
            ChangePassword password = model.Model;
            User user = await GetUserAsync(model.UserId);
            IdentityResult res = await userManager.AddPasswordAsync(user, password.NewPassword);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task ChangeUserPasswordAsync(ModelWithUserId<ChangePassword> model)
    {
        try
        {
            ChangePassword password = model.Model;
            User user = await GetUserAsync(model.UserId);
            IdentityResult res = await userManager.ChangePasswordAsync(user, password.OldPassword, password.NewPassword);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task CreateUserAsync(User user)
    {
        try
        {
            IdentityResult res = await userManager.CreateAsync(user);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));

        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteRoleFromUserAsync(string userId, string roleName)
    {
        try
        {
            User user = await userManager.FindByIdAsync(userId);
            IdentityResult res = await userManager.RemoveFromRoleAsync(user, roleName);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        User user = await userManager.FindByIdAsync(userId);
        IdentityResult res = await userManager.DeleteAsync(user);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string userId)
    {
        try
        {
            User user = await GetUserAsync(userId);
            return await userManager.GetRolesAsync(user);
        }
        catch
        {
            throw;
        }
    }

    public async Task<User> GetUserAsync(string userId)
    {
        try
        {
            return await db.Users.FirstAsync(u => u.Id == userId); ;
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        try
        {
            return Task.FromResult(userManager.Users.AsEnumerable());
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> HasUserPasswordAsync(string userId)
    {
        try
        {
            User user = await GetUserAsync(userId);
            return await userManager.HasPasswordAsync(user);
        }
        catch
        {
            throw;
        }
    }

    public Task UpdateUserAsync(User user)
    {
        try
        {
            db.Users.Update(user);
            db.SaveChanges();
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }
}
