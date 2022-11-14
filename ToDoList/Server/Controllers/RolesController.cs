using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Shared;
using ToDoList.Shared.Models.Account;
using ToDoList.Shared.Models.Db;
using ToDoList.Shared.Services;
using ToDoList.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Server.Controllers;

[Authorize(Roles = Roles.Admin)]
public class RolesController : ApiController
{
    readonly IRoleService iRole;
    public RolesController(IRoleService iRole)
        => this.iRole = iRole;

    [HttpGet]
    public async Task<IEnumerable<IdentityRole>> GetRoles()
         => await iRole.GetRolesAsync();

    [HttpGet("{roleId}")]
    public async Task<IdentityRole> GetRole(string roleId)
         => await iRole.GetRoleAsync(roleId);

    [HttpPost]
    public async Task<IActionResult> CreateRole(IdentityRole role)
        => await ReturnOkIfEverithingIsGood(async () => await iRole.CreateRoleAsync(role));

    [HttpPut]
    public async Task<IActionResult> UpdateRole(IdentityRole role)
        => await ReturnOkIfEverithingIsGood(async () => await iRole.UpdateRoleAsync(role));

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteUser(string roleId)
        => await ReturnOkIfEverithingIsGood(async () => await iRole.DeleteRoleAsync(roleId));
}
