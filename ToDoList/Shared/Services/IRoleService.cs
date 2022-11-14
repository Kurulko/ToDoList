using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models.Db;

namespace ToDoList.Shared.Services;

public interface IRoleService
{
    Task<IEnumerable<IdentityRole>> GetRolesAsync();
    Task<IdentityRole> GetRoleAsync(string roleId);
    Task UpdateRoleAsync(IdentityRole role);
    Task CreateRoleAsync(IdentityRole role);
    Task DeleteRoleAsync(string roleId);
}
