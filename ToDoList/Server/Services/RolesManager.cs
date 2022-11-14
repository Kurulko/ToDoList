using Microsoft.AspNetCore.Identity;
using ToDoList.Server.Models;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Server.Services;

public class RolesManager : IRoleService
{
    readonly RoleManager<IdentityRole> roleManager;
    readonly ToDoListContext db;
    public RolesManager(RoleManager<IdentityRole> roleManager, ToDoListContext db)
        => (this.roleManager, this.db) = (roleManager, db);

    public async Task CreateRoleAsync(IdentityRole role)
    {
        try
        {
            IdentityResult res = await roleManager.CreateAsync(role);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task<IdentityRole> GetRoleAsync(string roleId)
    {
        try
        {
            return await roleManager.FindByIdAsync(roleId);
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<IdentityRole>> GetRolesAsync()
    {
        try
        {
            return Task.FromResult(roleManager.Roles.AsEnumerable());
        }
        catch
        {
            throw;
        }
    }


    public async Task UpdateRoleAsync(IdentityRole role)
    {
        try
        {
            IdentityResult res = await roleManager.UpdateAsync(role);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteRoleAsync(string roleId)
    {
        try
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            IdentityResult res = await roleManager.DeleteAsync(role);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
        }
        catch
        {
            throw;
        }

    }

}
